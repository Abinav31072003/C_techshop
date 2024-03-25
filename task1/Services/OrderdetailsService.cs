using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Models;
using task1.Repository;
using task1.Exceptions;
using static task1.Repository.OrderdetailsRepository;

namespace task1.Services
{
    internal class OrderdetailsService
    {
        private readonly OrderdetailsRepository orderdetailsRepository;

        public OrderdetailsService()
        {
            orderdetailsRepository = new OrderdetailsRepository();
        }

        public void Orderdetailsmenu()
        {
            int inner1 = 0;
            Orderdetails orderdetail = new Orderdetails();
            do
            {
                Console.WriteLine("Order details Management");
                Console.WriteLine(".....................");
                Console.WriteLine($"1.Insert Order Details\n2.Update Order details\n3.Display Order details\n4.Calculate sub Total Amount\n5.Apply Discount\n6.Exit");
                Console.WriteLine("Enter your choice: ");
                inner1 = int.Parse(Console.ReadLine());
                switch (inner1)
                {

                    case 1:
                        try
                        {
                            Console.WriteLine("Enter Order Details id: ");
                            int odid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Order id: ");
                            int oid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Product id: ");
                            int pid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Quantity: ");
                            int quan = int.Parse(Console.ReadLine());
                            orderdetail = new Orderdetails(odid, oid, pid, quan);
                            InsertOrderdetailRecord(orderdetail);
                            Console.WriteLine("Record inserted successfully");
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;

                    case 2:
                        try
                        {
                            Console.WriteLine("Enter Order Details id: ");
                            int odid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Order id: ");
                            int oid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Product id: ");
                            int pid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Quantity: ");
                            int quan = int.Parse(Console.ReadLine());
                            Orderdetails orderdetails1 = new Orderdetails(odid, oid, pid, quan);
                            UpdateOrderdetailRecord(orderdetails1);
                            Console.WriteLine("Record updated successfully");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;

                    case 3:
                        Console.WriteLine("List of orders: ");
                        int odid1= int.Parse(Console.ReadLine());
                        ShowOrderdetailRecord(odid1);
                        break;

                    case 4:
                        Console.WriteLine("Enter Order Detail id: ");
                        int id1 = int.Parse(Console.ReadLine());
                        CalculateTotalAmount(id1);
                        break;

                    case 5:
                        Console.WriteLine("Enter Order Detail id:");
                        int odid2 = int.Parse(Console.ReadLine());
                        break;

                    case 6:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Try again!!!");
                        break;
                }
            } while (inner1 != 6);
        }

        public void InsertOrderdetailRecord(Orderdetails order) { }

        public void UpdateOrderdetailRecord(Orderdetails order) { }

        public void ShowOrderdetailRecord(int orderDetailID) {
            try
            {
                List<Orderdetails> orderdetails = orderdetailsRepository.GetOrderDetailInfo(orderDetailID);
                if (orderDetailID == 0)
                {
                    Console.WriteLine("Orderdetail id not found");
                }
                foreach (var orderdetail in orderdetails)
                {
                    Console.WriteLine($"OrderDetailid:{orderdetail.OrderDetailID}\n OrderID: {orderdetail.OrderID}\n ProductID: {orderdetail.ProductID}\n Quantity: {orderdetail.Quantity} ");
                }
            }
            catch (OrderDetailsNotFound ex)
            {
                Console.WriteLine($"Error getting order detail: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

            }
        }

        public void CalculateTotalAmount(int id1) { }
    }
}
