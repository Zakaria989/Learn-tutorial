using LearnTutorial.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrimTonic.Models;
using static TrimTonic.Models.WeightManager;

namespace TrimTonic.Pages
{
    public class Data_DeleteModel : PageModel
    {
        WeightData values;
        readonly IConfiguration _configuration;
        private readonly ILogger<Data_DeleteModel> _logger;
        public int measurementId;


        public Data_DeleteModel(ILogger<Data_DeleteModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            values = new WeightData(_configuration);
            measurementId = Convert.ToInt32(Request.Query["measurementId"]);
            values.DeleteValue(measurementId);
            Response.Redirect("/Data_Viewer");
        }


    }
}
