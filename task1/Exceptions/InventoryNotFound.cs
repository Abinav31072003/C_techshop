using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Models;

namespace task1.Exceptions
{
    internal class InventoryNotFound:Exception
    {
        public InventoryNotFound(string message) : base(message)
        {
        }
        public static bool InventoryIDNotFound(int value)
        {
            List<Inventory> inventory = new List<Inventory>();

            foreach (Inventory item in inventory)
            {
                if (item.InventoryID == value)
                {
                    return true;
                }
            }
            return false;
        }
        public static void CheckInsufficientStock(int requestedQuantity, int availableQuantity)
        {
            if (requestedQuantity > availableQuantity)
            {
                Console.WriteLine("Insufficient quantity in stock.");
            }

        }
    }
}
