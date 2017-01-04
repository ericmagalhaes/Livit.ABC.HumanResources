using System;
using System.Linq;
using System.Reflection;
using AutoMapper.Configuration;
using Livit.ABC.CommandStack.Commands;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Domain.Shared;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Mapper;
using SimpleInjector;
using Xunit;

namespace Livit.ABC.HumanResources.Tests
{
    /// <summary>
    /// Integration Tests
    /// </summary>
    public class AbsenceTests
    {
        private void InitDb(Repository repository)
        {
            //repository.Database.EnsureDeleted();
            repository.Database.EnsureCreated();
            
            if (!repository.Employees.Any())
            {
                var manager = new Employee();
                manager.Id = "manager@leave.com";
                var user = new Employee();
                user.Id = "user@leave.com";
                user.Manager = manager;
                repository.Employees.Add(user);
                repository.SaveChanges();
            }
            
        }

        private void InitContainer(Container container)

        {
            var handlerAssemblyName = typeof(RequestAbsenceCommand).AssemblyQualifiedName;
            
            
            var commandStackLibrary = new AssemblyName("Livit.ABC.CommandStack");
            var repositoryLibrary = new AssemblyName("Livit.ABC.Domain");
            var handlersAssembly = Assembly.Load(commandStackLibrary);
            var repositoryAssembly = Assembly.Load(repositoryLibrary);

            var handlerTypes = 
                handlersAssembly.GetExportedTypes()
                .Where(xt => xt.GetInterfaces().Count(i => i.Name == typeof(IHandleMessage<>).Name) > 0);
            container.RegisterCollection(typeof(IHandleMessage<>), handlerTypes);

            var handlerStartTypes =
                handlersAssembly.GetExportedTypes()
                .Where(xt => xt.GetInterfaces().Count(i => i.Name == typeof(IStartWithMessage<>).Name) > 0);
            container.RegisterCollection(typeof(IStartWithMessage<>), handlerStartTypes);
            var repositoryTypes =
                repositoryAssembly.GetExportedTypes()
                .Where(xt => xt.Namespace == "Livit.ABC.Domain.Persistence")
                    .Where(xt => xt.GetInterfaces().Any())
                    .Select((t) => new {Service = t.GetInterfaces().FirstOrDefault(), Implementation = t});
            foreach (var rep in repositoryTypes)
            {
                container.Register(rep.Service,rep.Implementation,Lifestyle.Transient);
            }
                

            //container.RegisterCollection(typeof(IRepository), repositoryTypes);
            container.Register<IBus,SimpleInjectorBus>();
            container.Register(()=> container);
            
            //container.Register<IEventStore>(()=> new SQLiteEventStore());


        }

        private void InitMapper(MapperConfigurationExpression config)
        {
            config.CreateMap<RequestAbsenceCommand, SchedulingRequest>().ConstructUsing((src, dst) => SchedulingRequest.Factory.Create(src.RequestedBy, src.StartDate, src.EndDate, src.Description));
            config.CreateMap<SchedulingRequest, ScheduleInfo>().ConstructUsing((src, dst) => src.ToScheduleInfo());
                
        }

        [Fact]
        public void AddAbsenceRequestTest()
        {
            var config = new MapperConfigurationExpression();
            InitMapper(config);
            
            var container = new Container();
            InitContainer(container);
            MapUtil.DefaultMapper = new AutoMapperImpl(config);
            var repository = new Repository();
            InitDb(repository);
            var startDate = DateTime.Now.AddDays(5);
            var endDate = startDate.AddDays(8);
            var absenceRequest = new RequestAbsenceCommand("user@leave.com", startDate, endDate);
            var bus = container.GetInstance<IBus>();
            bus.Send(absenceRequest);
            
            
        }

    }
    
}
