using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Security.Claims;
using TrimTonic.Models;

namespace LearnTutorial.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PrivacyModel : PageModel
    {

        private readonly ILogger<PrivacyModel> _logger;
        private readonly IConfiguration _configuration;
                public List<WeightManager.WeightData> weightDataList = new List<WeightManager.WeightData>();
        WeightManager weightManager;

        public PrivacyModel(ILogger<PrivacyModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            weightManager = new WeightManager(_configuration);
            weightDataList = weightManager.GetWeightData(Convert.ToInt32(userId)); // returns the weight data
            //weightManager.FillInRandomValues(4); //used for fill in purposes


            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Login");
            }
        }
    }
}