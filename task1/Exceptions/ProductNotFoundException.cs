using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Models;

namespace task1.Exceptions
{
    internal class ProductNotFoundException:Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {

        }


        public static bool ProductIDNotFound(int value)
        {
            List<Products> productslist = new List<Products>();
            foreach (Products product in productslist)
            {
                if (value != product.ProductID)
                    return false;
            }
            return true;
        }
        public static bool DuplicateProductID(Products products)

        {
            List<Products> productslist = new List<Products>();
            if (productslist.Exists(p => p.ProductID == products.ProductID))
            {
                return false;
            }
            return true;
        }
    }
}
