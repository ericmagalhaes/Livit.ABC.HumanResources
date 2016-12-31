using System.Security.Claims;
using System.Threading.Tasks;
using Livit.ABC.Infraestructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Livit.ABC.LeaveApi.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string EmployeeId { get; set; }
        public string ProviderAccessToken { get; set; }
    }
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly AccessTokenService _accessTokenService = null;
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor,
            AccessTokenService accessTokenService) : base(userManager, roleManager, optionsAccessor)
        {
            _accessTokenService = accessTokenService;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            
            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            new Claim(ClaimTypes.System +"_employee", user.EmployeeId),
            new Claim(ClaimTypes.Email, user.Email)
            //new Claim(ClaimTypes.System+ "_acessToken", _accessTokenService.GetValue()
            
        });
            

            return principal;
        }
    }
}
