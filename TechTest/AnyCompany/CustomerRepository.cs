using System;
using System.Data;
using System.Data.SqlClient;
using AnyCompany;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        //private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();
            try
            {
                using (SqlConnection connection = new SqlConnection(Utils.GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                        connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        customer.CustomerId= int.Parse( reader["CustomerId"].ToString());
                        customer.Name = reader["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        customer.Country = reader["Country"].ToString();
                    }

                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                Utils.LogErrorToDB(ex, "CustomerRepository.Load");
            }
            return customer;
        }
public static DataTable GetAllOrderingCustomers()
        {
            DataTable dtCustomers = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(Utils.GetConnectionString()))
                {
                    connection.Open();
                     SqlCommand command = new SqlCommand("SELECT Customer.*, Orders.Amount, Orders.VAT, (Orders.Amount, Orders.VAT) As [Total] FROM Customer inner join Orders On Customer.CustomerId = Orders.CustomerId group by Customer.Id, OrderId Order by Customer.Name", connection);
                    SqlDataAdapter daTemp = new SqlDataAdapter(command);
                    daTemp.Fill(dtCustomers);
                   //var reader = command.ExecuteReader();



                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Utils.LogErrorToDB(ex, "CustomerRepository.GetAllOrderingCustomers");
            }
            return dtCustomers;
        }
    }
}
