using ConsoleAppAirLineManagement.Repository;
using Microsoft.Data.SqlClient;

namespace ConsoleAppAirLineManagement.Repository
{
//Implementation of IAdminRepository for Admin login validation
    public class AdminRepositoryImpl : IAdminRepository
    {
    //Connection string to the SQL Server database
        private string connStr = @"Data Source=localhost;Initial Catalog=FlyWithMeDB;Integrated Security=True;TrustServerCertificate=True";

        public bool ValidateLogin(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
            //Validates the admin login using stored procedure sp_ValidateAdminLogin
                SqlCommand cmd = new SqlCommand("sp_ValidateAdminLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                int result = 0;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        result = (int)reader["IsValid"];
                }

                return result > 0;
            }
        }
    }
}

