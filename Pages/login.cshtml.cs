using LearnTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using LearnTutorial.DTOs;
using LearnTutorial.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LearnTutorial.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string LoginUsername { get; set; }
        [BindProperty]
        public string LoginPassword { get; set; }


        public string ErrorMessage { get; set; }
        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _configuration;


        public LoginModel(ILogger<LoginModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            // Redirect to home page if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UserDTO userDTO = new UserDTO 
            {
                Username = LoginUsername,
                Password = LoginPassword
            };


            // Authenticate user using AuthController Login method
            var authController = new AuthController(_configuration);
            var result = authController.Login(userDTO);
            // If authentication fails, show error message
            if (result.Result is BadRequestObjectResult )
            {
                var errorValue = (BadRequestObjectResult)result.Result;
                if (errorValue.Value != null)
                {
                    ViewData["ErrorMessage"] = errorValue.Value;
                }
                else
                {
                    ViewData["ErrorMessage"] = "An unknown error occurred.";
                }
                return Page();
            }

            try
            {
                // If authentication succeeds, sign in user with authentication cookie
                var value = (OkObjectResult)result.Result;
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(value.Value.ToString());
                var claims = token.Claims;

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    IsPersistent = true,
                };


                if (HttpContext != null)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                }
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex, "Error occurred while signing in.");
                // handle the exception
                // return an appropriate response to the client or redirect to an error page
                // for example:
                return LocalRedirect("/");
            }
            return LocalRedirect("/index");
        }


    }


}



