using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace task1.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Products(int ProductID, string ProductName, string Description, decimal Price) 
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.Description = Description;
            this.Price = Price;
        }

        public Products() { }
        
    }
}
