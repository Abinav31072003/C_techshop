using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using task1.Models;
using task1.Repository;
using task1.Exceptions;

namespace task1.Services
{
    internal class CustomerService
    {
        private readonly CustomerRepository customerRepository;

        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }
        Customer customer = new Customer();

        public void Customermenu()
        {
            int inner1 = 0;
            Customer customer = new Customer();
            do
            {
                Console.WriteLine("Customer Management");
                Console.WriteLine(".....................");
                Console.WriteLine($"1.Insert Customer\n2.Update Customer\n3.Display Customer details\n4.Total number of Orders\n5.exit\n");
                Console.WriteLine("Enter your choice: ");
                inner1 = int.Parse(Console.ReadLine());
                switch (inner1)
                {

                    case 1:
                        try
                        {
                            Console.WriteLine("Enter Customer id: ");
                            int id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter first name: ");
                            string fname = Console.ReadLine();
                            Console.WriteLine("Enter last name: ");
                            string lname = Console.ReadLine();
                            Console.WriteLine("Enter email: ");
                            string email = Console.ReadLine();
                            Console.WriteLine("Enter phone number: ");
                            string no = Console.ReadLine();
                            Console.WriteLine("Enter Address: ");
                            string address = Console.ReadLine();
                            customer = new Customer(id, fname, lname, email, no, address);
                            AddCustomer(customer);
                            Console.WriteLine("Record inserted successfully");
                        }
                        catch (Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;

                    case 2:
                        try
                        {
                            Console.WriteLine("Enter Customer id to be updated: ");
                            int u_id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter first name: ");
                            string u_fname = Console.ReadLine();
                            Console.WriteLine("Enter last name: ");
                            string u_lname = Console.ReadLine();
                            Console.WriteLine("Enter email: ");
                            string u_email = Console.ReadLine();
                            Console.WriteLine("Enter phone number: ");
                            string u_phno = Console.ReadLine();
                            Console.WriteLine("Enter Address:");
                            string u_address = Console.ReadLine();
                            Customer customer1 = new Customer(u_id, u_fname, u_lname, u_email, u_phno, u_address);
                            ChangeCustomerRecord(customer1);
                            Console.WriteLine("Record updated successfully");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;

                    case 3:
                        int cid1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Detail of Customer: ");
                        ShowCustomerRecord(cid1);
                        break;

                    case 4:
                        Console.WriteLine("Enter student id: ");
                        int id1 = int.Parse(Console.ReadLine());
                        TotalOrdersRecord(id1);
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

        public void AddCustomer(Customer customer) { }

        public void ChangeCustomerRecord(Customer customer) 
        {
            try
            {
                if (!DataValidationException.IsValidEmail(customer.Email))
                {
                    throw new DataValidationException("Invalid email format.");
                }

                if (!DataValidationException.IsValidPhoneNumber(customer.Phone))
                {
                    throw new DataValidationException("Invalid phone number format.");
                }

                customerRepository.UpdateCustomer(customer);
            }
            catch (DataValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void ShowCustomerRecord(int customerID) 
        {
            try
            {
                Customer customer = customerRepository.GetCustomerDetails(customerID);
                if (customer != null)
                {
                    Console.WriteLine($"CustomerID: {customer.CustomerID}");
                    Console.WriteLine($"FirstName: {customer.FirstName}");
                    Console.WriteLine($"Address: {customer.Address}");
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void TotalOrdersRecord(int id1) 
        {
            try
            {

                int totalOrders = customerRepository.CalculateTotalOrders(id1);
                Console.WriteLine($"The Total Orders placed by customer ID {id1} is {totalOrders}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
