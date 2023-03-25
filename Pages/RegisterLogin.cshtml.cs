using System.ComponentModel.DataAnnotations;
using LearnTutorial.Controllers;
using LearnTutorial.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LearnTutorial.Pages
{
    public class RegisterLoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        public string? Message { get; private set; }

        private readonly IConfiguration _configuration;

        public RegisterLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            Message = TempData["Message"]?.ToString();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create a UserDTO object to hold the user information
            var userDTO = new UserDTO
            {
                Username = UserName,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };

            // Serialize the UserDTO object to a JSON string
            //var json = JsonConvert.SerializeObject(userDTO);

            // Call the Register method of the AuthController to save the user information
            var authController = new AuthController(_configuration);
            var result = authController.Register(userDTO);

            // If registration fails, show error message
            if (result.Result is BadRequestObjectResult)
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

            ViewData["ErrorMessage"] = "Registration successful! You can now log in.";
            return RedirectToPage("RegisterLogin");
        }
    }
}
