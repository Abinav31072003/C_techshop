using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Models;

namespace task1.Exceptions
{
    internal class OrderNotFoundException:Exception
    {
        public OrderNotFoundException(string message) : base(message)
        {
        }

        public static bool OrderIDNotFound(int value)
        {
            List<Orders> orderslist = new List<Orders>();
            foreach (Orders order in orderslist)
            {
                if (value != order.OrderID)
                    return false;
            }
            return true;
        }
    }
}
