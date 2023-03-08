using System.Data.SqlClient;

namespace LearnTutorial.Models
{
	public class DatabaseManager
	{

		public static SqlConnection GetConnection()
		{
			try
			{
				string connectionString = "Data Source=localhost\\SQLEXPRESS ;Initial Catalog = NetCoreTutorial; Integrated Security =True; TrustServerCertificate=True";
				SqlConnection connection = new SqlConnection(connectionString);
				return connection;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
