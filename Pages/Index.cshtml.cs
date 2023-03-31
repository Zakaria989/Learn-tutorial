using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TrimTonic.Models;

namespace LearnTutorial.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration _configuration;
        public List<WeightManager.WeightData> weightDataList = new List<WeightManager.WeightData>();
        WeightManager weightManager;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {

            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    // Get user data from the cookie
            //    var username = HttpContext.User.Identity.Name;
            //    //var userId = HttpContext.User.Identity.;
            //    User = username;
            //}
            //else
            //{
            //    User = "";
            //}


        }

    }
}