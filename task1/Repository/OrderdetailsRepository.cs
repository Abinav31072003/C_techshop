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
    internal class OrderdetailsRepository
    {
        internal List<Orderdetails> GetOrderDetailInfo(int orderDetailID)
        {
            throw new NotImplementedException();
        }

        internal class OrderDetailsRepository
        {
            SqlConnection sql = null;
            SqlCommand cmd = null;

            public OrderDetailsRepository()
            {
                sql = new SqlConnection(DatabaseUtility.GetConnectionString());
                cmd = new SqlCommand();
            }

            public void AddOrderDetail(Orderdetails orderDetail)
            {
                sql.Open();
                cmd.Connection = sql;
                cmd.CommandText = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES (@OrderID, @ProductID, @Quantity)";
                cmd.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                cmd.Parameters.AddWithValue("@ProductID", orderDetail.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Order detail added successfully.");
                }
                else
                {
                    Console.WriteLine("No rows were affected. Failed to add order detail.");
                }
                sql.Close();
            }

            public List<Orderdetails> GetOrderDetailInfo(int orderDetailID)
            {
                List<Orderdetails> orderdetaillist = new List<Orderdetails>();


                sql.Open();
                cmd.Connection = sql;
                cmd.CommandText = "SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Orderdetails orderDetail = new Orderdetails
                    {
                        OrderDetailID = (int)reader["OrderDetailID"],
                        OrderID = (int)reader["OrderID"],
                        ProductID = (int)reader["ProductID"],
                        Quantity = (int)reader["Quantity"]
                    };
                    orderdetaillist.Add(orderDetail);
                }

                reader.Close();
                sql.Close();
                return orderdetaillist;


            }


            public void UpdateQuantity(int orderDetailID, int newQuantity)
            {
                sql.Open();
                cmd.Connection = sql;
                cmd.CommandText = "UPDATE OrderDetails SET Quantity = @Quantity WHERE OrderDetailID = @OrderDetailID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
                cmd.ExecuteNonQuery();
                sql.Close();
            }

            public decimal CalculateSubtotal(int orderDetailID)
            {
                decimal subtotal = 0;
                sql.Open();
                cmd.Connection = sql;
                cmd.CommandText = "SELECT (od.Quantity * p.Price) FROM OrderDetails od INNER JOIN Products p ON od.ProductID = p.ProductID WHERE od.OrderDetailID = @OrderDetailID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    subtotal = Convert.ToDecimal(result);
                }
                sql.Close();
                return subtotal;
            }

            public void AddDiscount(int orderDetailID, decimal discountAmount)
            {
                sql.Open();
                cmd.Connection = sql;
                cmd.CommandText = @"UPDATE Products 
                                SET Price = (
                                    SELECT (od.Quantity * p.Price) - @DiscountAmount
                                    FROM OrderDetails od
                                    INNER JOIN Products p ON od.ProductID = p.ProductID
                                    WHERE od.OrderDetailID = @OrderDetailID
                                )";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
                cmd.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                cmd.ExecuteNonQuery();
                sql.Close();
            }
    }   }
}
