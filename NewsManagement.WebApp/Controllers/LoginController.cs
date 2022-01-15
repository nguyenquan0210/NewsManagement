using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NewsManagement.ApiIntegration;
using NewsManagement.Utilities.Constants;
using NewsManagement.ViewModels.System.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public LoginController(IUserApiClient userApiClient,
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            request.Check = false;
            var result = await _userApiClient.Authenticate(request);
            if (result.ResultObj == null)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
            var userPrincipal = this.ValidateToken(result.ResultObj);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };

            HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);

            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(PublicRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            request.NameRole = "user";
            var result = await _userApiClient.PublicRegisterUser(request);

            if (result.IsSuccessed)
            {
                var login = new LoginRequest();
                login.Check = false;
                login.UserName = request.UserName;
                login.Password = request.Password;
                var loginrs = await _userApiClient.Authenticate(login);
                if (loginrs.ResultObj == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                var userPrincipal = this.ValidateToken(loginrs.ResultObj);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    IsPersistent = false
                };

                HttpContext.Session.SetString(SystemConstants.AppSettings.Token, loginrs.ResultObj);

                await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);
                return RedirectToAction("Index", "Home");
            }
            if (result == null)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
            return View(request);
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }
    }
}
