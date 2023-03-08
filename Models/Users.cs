using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using LearnTutorial;

namespace LearnTutorial.Models
{
	public class Users : DatabaseManager
    {
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public int UserId { get; set; }
		public DateTime DateOfRegistration { get; set; }

        public void CreateUserID() // Need the latest userID from SQL, for CreateUser()
        {	
			SqlConnection con = DatabaseManager.GetConnection();

			SqlCommand cmd = new SqlCommand("uspReturnLastUserId", con);
			cmd.CommandType = CommandType.StoredProcedure;
			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				try
				{
					UserId = Convert.ToInt32(dr["UserId"]);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			con.Close();
		}

		public void CreateUser()
		{
			CreateUserID();
			DateOfRegistration = DateTime.Now;

            try
            {
                SqlConnection con = DatabaseManager.GetConnection();

                SqlCommand cmd = new SqlCommand("uspAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@userId", UserId + 1));
                cmd.Parameters.Add(new SqlParameter("@userName", UserName));
                cmd.Parameters.Add(new SqlParameter("@password", Password));
                cmd.Parameters.Add(new SqlParameter("@firstName", FirstName));
                cmd.Parameters.Add(new SqlParameter("@lastName", LastName));
                cmd.Parameters.Add(new SqlParameter("@email", Email));
                cmd.Parameters.Add(new SqlParameter("@registerDate", DateOfRegistration));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

			catch (Exception ex)
			{

				throw ex;
			}
		}

		public void CheckUser()
		{
            try
            {
                SqlConnection con = DatabaseManager.GetConnection();

                SqlCommand cmd = new SqlCommand("uspCheckUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@userName", UserName));
                cmd.Parameters.Add(new SqlParameter("@password", Password));

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Access the data using the column name or index
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                    DateOfRegistration = reader.GetDateTime(reader.GetOrdinal("RegisterDate"));
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    Email = reader.GetString(reader.GetOrdinal("Email"));
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

