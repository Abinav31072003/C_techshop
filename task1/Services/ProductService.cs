using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Models;
using task1.Repository;
using task1.Exceptions;

namespace task1.Services
{
    internal class ProductService
    {
        private readonly ProductsRepository productsRepository;

        public ProductService()
        {
            productsRepository = new ProductsRepository();
        }

        public void Productmenu()
        {
            int inner1 = 0;
            Products product = new Products();
            do
            {
                Console.WriteLine("Product Management");
                Console.WriteLine(".....................");
                Console.WriteLine($"1.Insert Product\n2.Update Product\n3.Display Product details\n4.Product Stock\n5.exit\n");
                Console.WriteLine("Enter your choice: ");
                inner1 = int.Parse(Console.ReadLine());
                switch (inner1)
                {

                    case 1:
                        try
                        {
                            Console.WriteLine("Enter Product id: ");
                            int pid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Product name: ");
                            string pname = Console.ReadLine();
                            Console.WriteLine("Enter Product Description: ");
                            string pdesc = Console.ReadLine();
                            Console.WriteLine("Enter Product Price: ");
                            decimal pprice = decimal.Parse(Console.ReadLine());
                            product = new Products(pid, pname, pdesc, pprice);
                            InsertProductRecord(product);
                            Console.WriteLine("Record inserted successfully");
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;

                    case 2:
                        try
                        {
                            Console.WriteLine("Enter Product id: ");
                            int pid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Product name: ");
                            string pname = Console.ReadLine();
                            Console.WriteLine("Enter Product Description: ");
                            string pdesc = Console.ReadLine();
                            Console.WriteLine("Enter Product Price: ");
                            decimal pprice = decimal.Parse(Console.ReadLine());
                            Products product1 = new Products(pid, pname, pdesc, pprice);
                            UpdateProductRecord(product);
                            Console.WriteLine("Record updated successfully");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;

                    case 3:
                        Console.WriteLine("List of products: ");
                        ShowProductRecord();
                        break;

                    case 4:
                        Console.WriteLine("Enter product id: ");
                        int id1 = int.Parse(Console.ReadLine());
                        break;

                    case 5:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Try again!!!");
                        break;
                }
            } while (inner1 != 5);
        }

        public void InsertProductRecord(Products product) 
        {
            try
            {
                productsRepository.AddProduct(product);
                Console.WriteLine("Order added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding order: " + ex.Message);
            }
        }

        public void UpdateProductRecord(Products product) 
        {
            try
            {
                productsRepository.UpdateProductInfo(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void ShowProductRecord() 
        {
            try
            {
                List<Products> products = productsRepository.DisplayAllProducts();
                Console.WriteLine("All Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}, Description: {product.Description}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}
