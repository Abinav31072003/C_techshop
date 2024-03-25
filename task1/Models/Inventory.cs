using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Exceptions;

namespace task1.Models
{
    public class Inventory
    {
        int inventoryID;
        Products product;
        int quantityInStock;
        DateTime lastStockUpdate;
        public int InventoryID
        {
            get { return inventoryID; }
            set { inventoryID = value; }
        }
        public Products Product
        {
            get { return product; }
            set { product = value; }
        }
        public int QuantityInStock
        {
            get { return quantityInStock; }
            set
            {
                if (!DataValidationException.ValidateQuantity(value))
                {
                    throw new DataValidationException("give proper quantity in stock.");
                }
                quantityInStock = value;

            }
        }

        public DateTime LastStockUpdate
        {
            get { return lastStockUpdate; }
            set
            {
                if (!DataValidationException.IsValidDateTime(value))
                {
                    throw new DataValidationException("Please provide a valid date-time format for LastStockUpdate.");
                }
                lastStockUpdate = value;
            }
        }
        public Inventory()
        {

        }

        public Inventory(int inventoryID, Products product, int quantityInStock, DateTime lastStockUpdate)
        {
            this.inventoryID = inventoryID;
            this.product = product;
            this.quantityInStock = quantityInStock;
            this.lastStockUpdate = lastStockUpdate;

        }
        public override string ToString()
        {
            return $"{inventoryID} {product} {quantityInStock} {lastStockUpdate}";
        }
    }
}
