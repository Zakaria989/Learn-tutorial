using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TrimTonic.Models;

namespace TrimTonic.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class Data_ViewerModel : PageModel
    {
        public string SortDate { get; set; }
        public int SortDay { get; set; }
        public int SortYear { get; set; }
        public int SortMonth { get; set; }
        public bool CorrectValue { get; set; } = true;
        public string EditOrSave { get; set; }

        private readonly ILogger<Data_ViewerModel> _logger;
        private readonly IConfiguration _configuration;
        public List<WeightManager.WeightData> weightDataList = new List<WeightManager.WeightData>();
        WeightManager weightManager;

        public Data_ViewerModel(ILogger<Data_ViewerModel> logger, IConfiguration configuration)
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


        public void OnPost()
        {
            string[] splitSort = new string[3];
            SortDate = Request.Form["dateValue"];
            if (Regex.IsMatch(SortDate, @"^\d{2}/\d{2}/\d{4}$"))
            {
                splitSort = SortDate.Split('/');
                SortDay = Convert.ToInt32(splitSort[0]);
                SortMonth = Convert.ToInt32(splitSort[1]);
                SortYear = Convert.ToInt32(splitSort[2]);
            }
            else
            {
                CorrectValue = false;
            }

        }

        public void OnPostEdit(int weightDataId,double weight, double bodyFat, DateTime valueDate)
        {
            weightManager.EditDataValues(weightDataId, weight, bodyFat, valueDate);
        }
    }
}