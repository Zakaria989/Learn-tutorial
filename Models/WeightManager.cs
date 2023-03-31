using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using LearnTutorial;
using Azure.Core;
using static TrimTonic.Models.WeightManager;

namespace TrimTonic.Models
{

    public class WeightManager 
    {
        private readonly IConfiguration _configuration;
        public double Weight { get; set; }
        public double BMI { get; set; }
        public DateTime Date { get; set; }
        public WeightManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    
        public class WeightData
        {
            public double Weight { get; set; }
            public double BodyFat { get; set; }
            public DateTime ValueDate { get; set; }
        }
   
    public List<WeightData> GetWeightData(int userId)
        {
            List<WeightData> weightDataList = new List<WeightData>();

            try
            {
                using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DeafultConnection"]))
                {
                    SqlCommand cmd = new SqlCommand("uspShowWeightAndBodyFat", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@userId", userId));
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            WeightData weightData = new WeightData();
                            weightData.Weight = reader.GetDouble("Weight");
                            weightData.BodyFat = reader.GetDouble("BodyFat");
                            weightData.ValueDate = reader.GetDateTime("ValueDate");

                            weightDataList.Add(weightData);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return weightDataList;
        }

        public int GetFirstEmptyWeightDataId()
        {
            int firstEmptyId = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DeafultConnection"]))
                {
                    SqlCommand cmd = new SqlCommand("uspGetFirstEmptyWeightDataId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        firstEmptyId = reader.GetInt32(0);
                    }
                    reader.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return firstEmptyId;
        }


        public void FillInRandomValues(int userId)  //For look purposes
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DeafultConnection"]))
                {
                    int weightDataId;
                    SqlCommand cmd = new SqlCommand("uspFillInRandomValues", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    Random random = new Random();
                    con.Open();
                    for (int i = 0; i < 50; i++) // loop to generate 50 random records
                    {
                        weightDataId = GetFirstEmptyWeightDataId();
                        double weight = random.Next(70, 91);
                        double bodyFat = random.Next(15, 26);
                        DateTime date = DateTime.Now.AddDays(-random.Next(1, 180)); // random date between the last 6 months and today

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@WeightDataId", weightDataId));
                        cmd.Parameters.Add(new SqlParameter("@userId", userId));
                        cmd.Parameters.Add(new SqlParameter("@weight", weight));
                        cmd.Parameters.Add(new SqlParameter("@bodyFat", bodyFat));
                        cmd.Parameters.Add(new SqlParameter("@valueDate", date));

                        cmd.ExecuteNonQuery();

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
