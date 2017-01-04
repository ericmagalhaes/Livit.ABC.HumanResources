using System.Security.Claims;
using System.Threading.Tasks;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.LeaveApi.Models;
using Livit.ABC.LeaveApi.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Livit.ABC.LeaveApi.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmployeeRepository _employeeRepository = null;
     
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IEmployeeRepository employeeRepository)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        [Route("GoogleExternalAuth")]
        public IActionResult GoogleExternalAuthentication()
        {
            var request = HttpContext.Request;
            var redirect = "${request.Host}"+"/swagger/ui";
            
        }

        [HttpGet]
        [Route("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin(string provider,string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info != null)
            {
                return RedirectToAction("ExternalLoginCallback",new {returnUrl});
            }
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        //
        // GET: /Account/ExternalLoginCallback
        [HttpGet]
        [Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var email = info.Principal.FindFirst(c => c.Type == ClaimTypes.Email);
            var user = new ApplicationUser { UserName = email.Value, Email = email.Value };
            var hasLogin = await _userManager.FindByEmailAsync(email.Value);
            if (hasLogin == null)
            {
                var employee = _employeeRepository.RegisterEmployee(email.Value);
                user.EmployeeId = employee.Id;
                await _userManager.CreateAsync(user);
                await _userManager.AddLoginAsync(user, info);
                
            }
            await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
            
            return Redirect(returnUrl);
        }
        





        #region Helpers


        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(this.ExternalLogin), "Home");
            }
        }

        #endregion
    }
}
