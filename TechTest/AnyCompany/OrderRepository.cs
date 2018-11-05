using System;
using System.Data.SqlClient;
using AnyCompany;

namespace AnyCompany
{
    internal class OrderRepository
    {
        //private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Utils.GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @CustomerId, @Amount, @VAT)", connection);

                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    command.Parameters.AddWithValue("@Amount", order.Amount);
                    command.Parameters.AddWithValue("@VAT", order.VAT);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                Utils.LogErrorToDB(ex, "OrderRepository.Save");
            }
        }
    }
}
