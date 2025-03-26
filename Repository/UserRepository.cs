using Microsoft.Data.SqlClient;
using System.Data;

namespace RoleBasedAuth.Repository
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbcs");
        }

        // Validate User with Username and Password
        public DataRow ValidateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                    }
                }
            }
        }
    }

}
