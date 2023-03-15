using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using LearnTutorial;
using Azure.Core;

namespace LearnTutorial.Models
{
	public class User : DatabaseManager
	{
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PasswordHash { get; set; }
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
				cmd.Parameters.Add(new SqlParameter("@password", PasswordHash));
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
				using (SqlConnection con = DatabaseManager.GetConnection())
				{
					SqlCommand cmd = new SqlCommand("uspCheckUser", con);
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.Add(new SqlParameter("@userName", UserName));

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();

					if (reader.Read())
					{
                        UserId = reader.GetInt32("UserId");
                        PasswordHash = reader.GetString("PassWord");
                    }

                    reader.Close();
				} 
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
    }
}

