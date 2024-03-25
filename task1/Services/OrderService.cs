using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task1.Exceptions;
using task1.Models;
using task1.Repository;

namespace task1.Services
{
    internal class OrderService
    {
        private readonly OrdersRepository ordersRepository;

        public OrderService()
        {
            ordersRepository = new OrdersRepository();
        }

        public void Ordermenu()
        {
            int inner1 = 0;
            Orders order = new Orders();
            do
            {
                Console.WriteLine("Order Management");
                Console.WriteLine(".....................");
                Console.WriteLine($"1.Insert Order\n2.Update Order\n3.Display Orders\n4.Calculate Total Amount\n5.exit\n");
                Console.WriteLine("Enter your choice: ");
                inner1 = int.Parse(Console.ReadLine());
                switch (inner1)
                {

                    case 1:
                        try
                        {
                            Console.WriteLine("Enter Order id: ");
                            int oid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Customer id: ");
                            int cid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Order Date: ");
                            string odate = Console.ReadLine();
                            Console.WriteLine("Enter Total Amount: ");
                            decimal oprice = decimal.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Order Status");
                            string ostatus = Console.ReadLine();
                            order = new Orders(oid, cid, DateTime.Parse(odate), oprice, ostatus);
                            InsertOrderRecord(order);
                            Console.WriteLine("Record inserted successfully");
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;

                    case 2:
                        try
                        {
                            Console.WriteLine("Enter Order id: ");
                            int oid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Order status:");
                            string ostatus = Console.ReadLine();
                            UpdateOrderRecord(oid, ostatus);
                            Console.WriteLine("Record updated successfully");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;

                    case 3:
                        Console.WriteLine("List of orders: ");
                        ShowOrderRecord();
                        break;

                    case 4:
                        Console.WriteLine("Enter order id: ");
                        int id1 = int.Parse(Console.ReadLine());
                        CalculateTotalAmount(id1);
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

        public void InsertOrderRecord(Orders order) 
        {
            try
            {
                ordersRepository.AddOrder(order);
                Console.WriteLine("Order added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding order: " + ex.Message);
            }
        }

        public void UpdateOrderRecord(int orderId, string status) 
        {
            try
            {
                if (!OrderNotFoundException.OrderIDNotFound(orderId))
                {

                    ordersRepository.UpdateOrderStatus(orderId, status);
                    Console.WriteLine($"Order status updated successfully for Order ID {orderId}.");
                }
                else
                {
                    throw new OrderNotFoundException($"Order with ID {orderId} not found.");
                }
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating order status: " + ex.Message);
            }
        }

        public void ShowOrderRecord() 
        {
            try
            {
                List<Orders> orderlist = ordersRepository.GetOrderDetails();

                Console.WriteLine("Retrieved Order Details:");
                foreach (var orderDetail in orderlist)
                {
                    Console.WriteLine($"OrderID: {orderDetail.OrderID}, CustomerID: {orderDetail.CustomerID}, OrderDate: {orderDetail.OrderDate}, TotalAmount: {orderDetail.TotalAmount}");

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving order details: " + ex.Message);
            }
        }

        public void CalculateTotalAmount(int id1) { }

        public void CancelOrder(int orderId)
        {
            try
            {
                if (!OrderNotFoundException.OrderIDNotFound(orderId))
                {
                    ordersRepository.CancelOrder(orderId);
                    Console.WriteLine($"Order with ID {orderId} canceled successfully.");
                }
                else
                {
                    throw new OrderNotFoundException($"Order with ID {orderId} not found.");
                }
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while canceling order: " + ex.Message);
            }
        }
    }
}
