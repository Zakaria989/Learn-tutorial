using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace LearnTutorial.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string User { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // Get user data from the cookie
                var username = HttpContext.User.Identity.Name;
                //var userId = HttpContext.User.Identity.;
                User = username;
            }
            else
            {
                User = "";
            }


        }

    }
}