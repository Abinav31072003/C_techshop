using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Models;
using System.Data.SqlClient;
using task1.Utility;

namespace task1.Repository
{
    internal class OrdersRepository
    {
        private readonly SqlConnection sql;
        private readonly SqlCommand cmd;

        public OrdersRepository()
        {
            sql = new SqlConnection(DatabaseUtility.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sql;
        }

        public void CalculateTotalAmount()
        {
            sql.Open();
            cmd.CommandText = @"
                UPDATE Orders
                SET TotalAmount = (
                    SELECT SUM(od.Quantity * p.Price)
                    FROM OrderDetails od
                    INNER JOIN Products p ON od.ProductID = p.ProductID
                    WHERE od.OrderID = Orders.OrderID
                )";
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public List<Orders> GetOrderDetails()
        {
            List<Orders> ordersList = new List<Orders>();
            sql.Open();
            cmd.CommandText = "SELECT * FROM Orders";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Orders order = new Orders
                {
                    OrderID = (int)reader["OrderID"],
                    CustomerID = (int)reader["CustomerID"],
                    OrderDate = (DateTime)reader["OrderDate"],
                    TotalAmount = reader["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalAmount"])
                };
                ordersList.Add(order);
            }
            sql.Close();
            return ordersList;
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            sql.Open();
            cmd.CommandText = "UPDATE Orders SET OrderStatus = @status WHERE OrderID = @orderid";
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@orderid", orderId);
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public void AddOrder(Orders order)
        {
            sql.Open();
            cmd.CommandText = "INSERT INTO Orders (OrderID, CustomerID, OrderDate, TotalAmount) VALUES (@OrderID, @CustomerID, @OrderDate, @TotalAmount)";
            cmd.Parameters.AddWithValue("@OrderID", order.OrderID);
            cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public void CancelOrder(int orderId)
        {
            sql.Open();
            cmd.CommandText = "DELETE FROM Orders WHERE OrderID = @OrderID";
            cmd.Parameters.AddWithValue("@OrderID", orderId);
            cmd.ExecuteNonQuery();

            sql.Close();

        }

    }
}
