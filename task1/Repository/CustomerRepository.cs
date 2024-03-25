using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Models;
using task1.Utility;
using System.Data.SqlClient;

namespace task1.Repository
{
    internal class CustomerRepository
    {
        private readonly string connectionString;


        public CustomerRepository()
        {
            connectionString = DatabaseUtility.GetConnectionString();
        }

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO CUSTOMERS (CustomerID,FirstName,LastName,Email,Phone,Addresss ) VALUES (@CustomerID,@FirstName,@LastName,@Email,@Phone,@Addresss)";
                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Customer GetCustomerDetails(int customerId)
        {
            Customer customer = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {

                connect.Open();
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM CUSTOMERS WHERE CustomerID = @customerId";
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                    }
                }
            }

            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {

                connect.Open();
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = "UPDATE CUSTOMERS SET CustomerName = @Name, Address = @Address WHERE CustomerId = @Customerid";
                    cmd.Parameters.AddWithValue("@Customerid", customer.CustomerID);
                    cmd.Parameters.AddWithValue("@Name", customer.FirstName);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {

                connect.Open();
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM CUSTOMERS WHERE CustomerId = @CustomerId";
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {

                connect.Open();
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM CUSTOMERS";

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"].ToString(),
                            Address = reader["Addresss"].ToString()
                        });
                    }
                }
            }
            return customers;

        }
        public int CalculateTotalOrders(int customerId)
        {
            int totalOrders = 0;

            using (SqlConnection connect = new SqlConnection(connectionString))
            {

                connect.Open();
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Orders WHERE CustomerID = @CustomerId";
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    // ExecuteScalar is used since we expect a single value from the query
                    totalOrders = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }

            return totalOrders;
        }
    }
}
