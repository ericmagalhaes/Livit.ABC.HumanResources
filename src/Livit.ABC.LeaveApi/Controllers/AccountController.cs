using System.Security.Claims;
using System.Threading.Tasks;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.LeaveApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Livit.ABC.LeaveApi.Controllers
{

    /// <summary>
    /// responsable for external and internal authentication
    /// </summary>
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmployeeRepository _employeeRepository = null;     
        private readonly ILogger _logger;

        /// <summary>
        /// Account Controller
        /// </summary>
        /// <param name="userManager">User management</param>
        /// <param name="signInManager">Signin manager</param>
        /// <param name="loggerFactory">Logger</param>
        /// <param name="employeeRepository">Employee repository</param>

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

        /// <summary>
        /// provides a url for web api authentication
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GoogleExternalAuth")]
        public string GoogleExternalAuthentication()
        {
            var request = HttpContext.Request;
            var redirect = $"{request.Scheme}://{request.Host}/Account/ExternalLogin?provider=Google&returnUrl={request.Scheme}://{request.Host}/Swagger/ui";
            return redirect;
        }
        /// <summary>
        /// external login method
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin([FromQuery]ExternalLoginModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest(login);

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info != null)
            {
                return RedirectToAction("ExternalLoginCallback",new {login.ReturnUrl});
            }
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { login.ReturnUrl });
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
                var employee = _employeeRepository.RegisterEmployee(email.Value,"manager@livit.com");
                user.EmployeeId = employee.Id;
                await _userManager.CreateAsync(user);
                await _userManager.AddLoginAsync(user, info);
                
            }
            await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
            
            return Redirect(returnUrl);
        }
        /// <summary>
        /// provider an url for manager login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("LoginUrl")]
        public string  LoginUrl()
        {
            var request = HttpContext.Request;
            var redirect = $"{request.Scheme}://{request.Host}/Account/Login?UserName=manager@livit.com&password=@Manag3r&returnUrl={request.Scheme}://{request.Host}/Swagger/ui";
            return redirect;
        }
        /// <summary>
        /// login method
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromQuery]LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest(login);

            var info = await _signInManager.GetExternalLoginInfoAsync();
            var signedId = _signInManager.IsSignedIn(User);
            if (info != null || signedId)
            {
                await _signInManager.SignOutAsync();
            }
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return Redirect(login.ReturnUrl);
            }
            return BadRequest("User not found");
        }
        

    }
}
