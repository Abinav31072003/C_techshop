using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Services;

namespace task1.techshopapp
{
    internal class Techshopmanage
    {
        CustomerService customerService;
        ProductService productService;
        OrderService orderService;
        OrderdetailsService orderdetailsService;
        InventoryService inventoryService;

        public Techshopmanage()
        {
            customerService = new CustomerService();
            productService = new ProductService();
            orderService = new OrderService();
            orderdetailsService = new OrderdetailsService();
            inventoryService = new InventoryService();
        }

        public void displaymenu()
        {
            int option = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine(".................");
                Console.WriteLine($"1:: Customer\n2:: Product\n3:: Orders\n4:: Order Details\n5:: Inventory\n6:: Exit\n");
                Console.WriteLine("Enter your choice: ");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        customerService.Customermenu();
                        break;

                    case 2:
                        productService.Productmenu();
                        break;

                    case 3:
                        orderService.Ordermenu();
                        break;

                    case 4:
                        orderdetailsService.Orderdetailsmenu();
                        break;

                    case 5:
                        inventoryService.InventoryMenu();
                        break;

                    case 6:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Try again...");
                        break;
                }
            } while (option != 6);
        }
    }
}
