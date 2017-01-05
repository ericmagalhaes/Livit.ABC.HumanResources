using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Livit.ABC.CommandStack.Commands;
using Livit.ABC.CommandStack.Events;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Domain.Query;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Domain.Shared;
using Livit.ABC.Infraestructure;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Mapper;
using Livit.ABC.LeaveApi.Data;
using Livit.ABC.LeaveApi.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;
using SimpleInjector.Integration.AspNetCore.Mvc;

namespace Livit.ABC.LeaveApi
{
    public class Startup
    {
        private readonly Container _container = new Container();
        private readonly MapperConfigurationExpression _config = new MapperConfigurationExpression();
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if(env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
           
            services.AddTransient<AccessTokenService>();
           // services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();
            // Add framework services.
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
            //});
            services.AddSwaggerGen();

            services.AddSingleton<IControllerActivator>(
          new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSimpleInjectorAspNetRequestScoping(_container);

            _container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();
            #region init
            InitializeContainer(app);
            InitMapper(_config);

            MapUtil.DefaultMapper = new AutoMapperImpl(_config);
            //_container.Verify();
            var repository = _container.GetInstance<Repository>();
            InitDb(repository);
            #endregion

            var appDb = _container.GetInstance<ApplicationDbContext>();
            //appDb.Database.EnsureDeleted();
            if (appDb.Database.EnsureCreated())
            {
                var employeeRepository = _container.GetInstance<IEmployeeRepository>();
                var employee = employeeRepository.RegisterEmployee("manager@livit.com");

                var userManager = _container.GetInstance<UserManager<ApplicationUser>>();

                var managerUser = new ApplicationUser();
                managerUser.UserName = employee.Id;
                managerUser.Email = employee.Id;
                userManager.CreateAsync(managerUser, "@Manag3r").Wait();
                userManager.AddClaimAsync(managerUser, new Claim(ClaimTypes.Role, "Manager")).Wait();
            }

           
            

            app.UseIdentity();

            var googleOptions = new GoogleOptions();
            googleOptions.ClientId = "630612391887-p9s3d64g0l9sq0bmu532k48r2l9mtc70.apps.googleusercontent.com";
            googleOptions.ClientSecret = "Q91V0iFWfuxSRoX9x6FOoGDL";
            googleOptions.Scope.Add("https://www.googleapis.com/auth/calendar");

            googleOptions.Events = new OAuthEvents()
            {
                OnCreatingTicket = context =>
                {   
                    var tokenJson = JsonConvert.SerializeObject(context.TokenResponse);
                    context.HttpContext.Session.SetString("access_token",tokenJson);
                    return Task.FromResult(0);
                }
            };

            app.UseSession();


            app.UseGoogleAuthentication(googleOptions);
           
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
        private void InitDb(Repository repository)
        {
            //repository.Database.EnsureDeleted();
            repository.Database.EnsureCreated();



        }
        private void InitMapper(MapperConfigurationExpression config)
        {
            config.CreateMap<RequestAbsenceCommand, SchedulingRequest>().ConstructUsing((src, dst) => SchedulingRequest.Factory.Create(src.RequestedBy, src.StartDate, src.EndDate, src.Description));
            config.CreateMap<SchedulingRequest, ScheduleInfo>().ConstructUsing((src, dst) => src.ToScheduleInfo());
            config.CreateMap<AbsenceRequest, ScheduleCreatedEvent>().ConstructUsing(ScheduleCreatedEvent.FromAbsenceRequest);

            config.CreateMissingTypeMaps = true;
            

        }
        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            var handlerAssemblyName = typeof(RequestAbsenceCommand).AssemblyQualifiedName;

            var commandStackLibrary = new AssemblyName("Livit.ABC.CommandStack");
            var repositoryLibrary = new AssemblyName("Livit.ABC.Domain");
            var handlersAssembly = Assembly.Load(commandStackLibrary);
            var repositoryAssembly = Assembly.Load(repositoryLibrary);

            var handlerTypes =
                handlersAssembly.GetExportedTypes()
                .Where(xt => xt.GetInterfaces().Count(i => i.Name == typeof(IHandleMessage<>).Name) > 0);
            _container.RegisterCollection(typeof(IHandleMessage<>), handlerTypes);

            var handlerStartTypes =
                handlersAssembly.GetExportedTypes()
                .Where(xt => xt.GetInterfaces().Count(i => i.Name == typeof(IStartWithMessage<>).Name) > 0);
            _container.RegisterCollection(typeof(IStartWithMessage<>), handlerStartTypes);
            var repositoryTypes =
                repositoryAssembly.GetExportedTypes()
                .Where(xt => xt.Namespace == "Livit.ABC.Domain.Persistence")
                    .Where(xt => xt.GetInterfaces().Any())
                    .Select((t) => new { Service = t.GetInterfaces().FirstOrDefault(), Implementation = t });
            foreach (var rep in repositoryTypes)
            {
                _container.Register(rep.Service, rep.Implementation, Lifestyle.Transient);
            }
            _container.Register<IQueryDispatcher,QueryDispatcher>();
            _container.Register(typeof(IQueryHandler<,>), typeof(HumanResourcesQueryHandler));
            _container.Register(typeof(IQueryHandler<,>), typeof(TaskApprovmentQueryHandler));
            //container.RegisterCollection(typeof(IRepository), repositoryTypes);
            _container.Register<IBus, SimpleInjectorBus>();
            _container.Register(() => _container);
            
            // Cross-wire ASP.NET services (if any). For instance:
            _container.RegisterSingleton(app.ApplicationServices.GetService<ILoggerFactory>());
            _container.Register(() => app.ApplicationServices.GetService<UserManager<ApplicationUser>>());
            _container.Register(() => app.ApplicationServices.GetService<SignInManager<ApplicationUser>>());
            _container.Register(() => app.ApplicationServices.GetService<ApplicationDbContext>());
            _container.Register(() => app.ApplicationServices.GetService<AccessTokenService>());
            // _container.Register<AccessTokenService>(Lifestyle.Transient);





            // NOTE: Prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/
        }
    }
    
}
