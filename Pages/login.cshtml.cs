using LearnTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using LearnTutorial.DTOs;
using LearnTutorial.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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
            if (result is ActionResult<string> actionResult)
            {
                var value = (BadRequestObjectResult)actionResult.Result;
                if ( value.Value != null)
                {
                    ViewData["ErrorMessage"] = value.Value;
                }
                else
                {
                    ViewData["ErrorMessage"] = "An unknown error occurred.";
                }
                return Page();
            }


            // If authentication succeeds, sign in user with authentication cookie
            var claims = JsonConvert.DeserializeObject<ClaimsIdentity>(result.Value.ToString());
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = true,
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claims),
                authProperties);

            return LocalRedirect("/");
        }

    }



}



