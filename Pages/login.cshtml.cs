using LearnTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnTutorial.Pages
{
    public class loginModel : PageModel
    {
		[BindProperty]
		public string LoginUserName { get; set; }
        [BindProperty]
		public string LoginPassWord { get; set; }

        public int UserId { get; set; }
        public string Password { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
            newUser.UserName = LoginUserName;
            newUser.Password = LoginPassWord;

            newUser.CheckUser();

            UserId = newUser.UserId;
            Password = newUser.Password;
            DateOfRegistration = newUser.DateOfRegistration;
            FirstName = newUser.FirstName;
            LastName = newUser.LastName;
            Email = newUser.Email;

            return Page();
        }
    }
}



