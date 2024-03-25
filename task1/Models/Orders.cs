using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string status { get; set; }

        public Orders(int OrderID, int customerid, DateTime OrderDate, decimal TotalAmount, string status)  
        {
            this.OrderID = OrderID;
            this.CustomerID = customerid;
            this.OrderDate = OrderDate;
            this.TotalAmount = TotalAmount;
            this.status = status;   
        }
        
        public Orders() { }

    }
}
