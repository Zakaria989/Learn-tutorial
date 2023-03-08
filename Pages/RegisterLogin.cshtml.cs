using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LearnTutorial.Models;

namespace LearnTutorial.Pages
{
    public class RegisterLoginModel : PageModel
    {
		[BindProperty]
		public string UserName { get; set; }
		[BindProperty]
		public string FirstName { get; set; }
		[BindProperty]
		public string LastName { get; set; }
		[BindProperty]
		public string Password { get; set; }
		[BindProperty]
		public string Email { get; set; }

        public void OnGet()
        {
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			Users newUser = new Users();
			newUser.UserName = UserName;
			newUser.FirstName = FirstName;
			newUser.LastName = LastName;
			newUser.Password = Password;
			newUser.Email = Email;

			newUser.CreateUser();

			return RedirectToPage("/Login");
		}
	}
}
