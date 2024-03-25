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
    internal class InventoryRepository
    {
        SqlConnection sqlConnection;
        SqlCommand cmd;

        public InventoryRepository()
        {
            sqlConnection = new SqlConnection(DatabaseUtility.GetConnectionString());
            cmd = new SqlCommand();
        }

        public int GetQuanitityInStock(int inventoryID)
        {
            int quantityInStock = 0;

            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "SELECT QuantityInStock FROM Inventory WHERE InventoryID = @InventoryID";
            cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                quantityInStock = Convert.ToInt32(result);
            }

            sqlConnection.Close();

            return quantityInStock;
        }

        public void GetProducts(Products product, int inventoryID)
        {
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "SELECT  p.* FROM Inventory i INNER JOIN Products p ON i.ProductID = p.ProductID WHERE i.InventoryID = @inventoryID";
            cmd.Parameters.AddWithValue("@inventoryID", inventoryID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    product.ProductID = (int)reader["ProductID"];
                    product.ProductName = reader["ProductName"].ToString();
                    product.Price = (decimal)reader["Price"];
                    product.Description = reader["Description"].ToString();

                }
            }
            sqlConnection.Close();
        }
        public void AddToInventory(int productID, int quantityToAdd)
        {

            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate) VALUES (@ProductID, @QuantityToAdd, @LastStockUpdate); SELECT SCOPE_IDENTITY();";

            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@QuantityToAdd", quantityToAdd);
            cmd.Parameters.AddWithValue("@LastStockUpdate", DateTime.Now);
            int newInventoryID = Convert.ToInt32(cmd.ExecuteScalar());
            // newInventoryID now holds the value of the newly inserted InventoryID
            sqlConnection.Close();
        }
        public void RemoveFromInventory(int productID, int quantityToRemove)
        {
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "UPDATE Inventory SET QuantityInStock = QuantityInStock - @QuantityToRemove WHERE ProductID = @ProductID";
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@QuantityToRemove", quantityToRemove);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }


        public void UpdateStockQuantity(int productID, int newQuantity)
        {
            cmd.Connection = sqlConnection;
            sqlConnection.Open();


            cmd.CommandText = "UPDATE Inventory SET QuantityInStock = @NewQuantity WHERE ProductID = @ProductID";

            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);
            sqlConnection.Close();
        }

        public bool IsProductAvailable(int productID, int quantityToCheck)
        {
            bool isAvailable = false;

            cmd.Connection = sqlConnection;
            sqlConnection.Open();

            cmd.CommandText = "SELECT 1 FROM Inventory WHERE ProductID = @ProductID AND QuantityInStock >= @QuantityToCheck";
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@QuantityToCheck", quantityToCheck);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                isAvailable = true;
            }

            sqlConnection.Close();

            return isAvailable;
        }

        public decimal GetInventoryValue()
        {
            decimal totalValue = 0;
            cmd.Connection = sqlConnection;
            sqlConnection.Open();

            cmd.CommandText = "SELECT SUM(p.Price * i.QuantityInStock) AS TotalValue " +
                         "FROM Inventory i " +
                         "INNER JOIN Products p ON i.ProductID = p.ProductID";

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                totalValue = Convert.ToDecimal(result);
            }
            else
            {
                totalValue = 0;
            }

            return totalValue;
        }

        public List<Inventory> ListLowStockProducts(int threshold)
        {
            List<Inventory> lowStockProducts = new List<Inventory>();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();

            cmd.CommandText = "SELECT p.ProductName, i.QuantityInStock, i.LastStockUpdate " +
                             "FROM Inventory i " +
                             "INNER JOIN Products p ON i.ProductID = p.ProductID " +
                             "WHERE i.QuantityInStock < @Threshold";

            cmd.Parameters.AddWithValue("@Threshold", threshold);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Inventory inventory = new Inventory();
                inventory.Product.ProductName = reader["ProductName"].ToString();
                inventory.QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]);
                inventory.LastStockUpdate = Convert.ToDateTime(reader["LastStockUpdate"]);
                lowStockProducts.Add(inventory);
            }

            return lowStockProducts;
        }


        public List<Inventory> ListOutOfStockProducts()
        {
            List<Inventory> outOfStockProducts = new List<Inventory>();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "SELECT p.ProductName, i.LastStockUpdate " +
                             "FROM Inventory i " +
                             "INNER JOIN Products p ON i.ProductID = p.ProductID " +
                             "WHERE i.QuantityInStock = 0";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Inventory inventory = new Inventory();
                inventory.Product.ProductName = reader["ProductName"].ToString();
                inventory.LastStockUpdate = Convert.ToDateTime(reader["LastStockUpdate"]);
                outOfStockProducts.Add(inventory);
            }

            return outOfStockProducts;
        }


    }
}
