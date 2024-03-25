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
    internal class ProductsRepository
    {
        SqlConnection sql = null;
        SqlCommand cmd = null;
        public ProductsRepository()
        {
            sql = new SqlConnection(DatabaseUtility.GetConnectionString());
            cmd = new SqlCommand();
        }
        List<Products> productlist = new List<Products>();


        public List<Products> DisplayAllProducts()
        {
            cmd.CommandText = "SELECT * FROM PRODUCTS ";
            cmd.Connection = sql;
            sql.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Products product = new Products();
                product.ProductID = (int)reader["ProductID"];
                product.ProductName = (string)reader["ProductName"];
                product.Price = (decimal)reader["Price"];
                product.Description = (string)reader["Description"];
                productlist.Add(product);
            }
            sql.Close();
            return productlist;

        }
        public void UpdateProductInfo(Products product)

        {
            using (SqlConnection sql = new SqlConnection(DatabaseUtility.GetConnectionString()))
            {
                sql.Open();

                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO PRODUCTS (ProductID, ProductName, Descriptionn, Price) VALUES  (@ID,@NAME,@DESCRIPTION,@PRICE)";
                    cmd.Parameters.AddWithValue("@ID", product.ProductID);
                    cmd.Parameters.AddWithValue("@NAME", product.ProductName);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", product.Description);
                    cmd.Parameters.AddWithValue("@PRICE", product.Price);
                    cmd.Connection = sql;


                    cmd.ExecuteNonQuery();

                }
            }

        }

        public void AddProduct(Products product)
        {
            sql.Open();
            cmd.CommandText = "INSERT INTO Products (ProductID, ProductName, Description, Price) VALUES (@ProductID, @ProductName, @Description, @Price)";
            cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.ExecuteNonQuery();
            sql.Close();
        }
        public Products GetProductDetails(int productId)
        {
            Products product = new Products();

            using (sql)
            {
                cmd.CommandText = "SELECT * FROM PRODUCTS WHERE ProductID = @ID";
                cmd.Parameters.AddWithValue("@ID", productId);
                cmd.Connection = sql;
                sql.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product.ProductID = (int)reader["ProductID"];
                    product.ProductName = (string)reader["ProductName"];
                    product.Price = (decimal)reader["Price"];
                    product.Description = (string)reader["Description"];
                }
            }

            return product;
        }
        public bool IsProductInStock(int productId)
        {
            int stockCount = 0;

            using (sql)
            {
                cmd.CommandText = "SELECT COUNT(*) FROM INVENTORY WHERE ProductID = @ID";
                cmd.Parameters.AddWithValue("@ID", productId);
                cmd.Connection = sql;
                sql.Open();
                stockCount = (int)cmd.ExecuteScalar();
            }

            return stockCount > 0;
        }
        public void RemoveProduct(int productId)
        {
            using (sql)
            {
                cmd.CommandText = "DELETE FROM PRODUCTS WHERE ProductID = @ID";
                cmd.Parameters.AddWithValue("@ID", productId);
                cmd.Connection = sql;
                sql.Open();
                cmd.ExecuteNonQuery();
            }



        }

    }
}
