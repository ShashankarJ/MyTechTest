using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AnyCompany
{
    public static class  Utils
    {
        private static string ConnectionString = @"Data Source=(local);Database=orderSystem;User Id=admin;Password=password;";

        public static string GetConnectionString()
        {
            
   
            return ConnectionString;
        }
        public static void LogErrorToDB(Exception ex, string strSource)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Utils.GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO ErrorLog VALUES (@Source, @ErrorMsg, GETDATE())", connection);

                    command.Parameters.AddWithValue("@Source", strSource.ToString());
                    command.Parameters.AddWithValue("@ErrorMsg", ex.Message.ToString());


                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception EX)
            {
                Utils.LogErrorToDB(EX, "Utils.LogErrorToDB");
            }
        }
    }
}
