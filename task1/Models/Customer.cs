using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Customer (int CustomerID, string Firstname, string Lastname, string Email, string Phone, string Address)
        {
            this.CustomerID = CustomerID;
            this.FirstName = Firstname;
            this.LastName = Lastname;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;

        }

        public Customer() { }
    }
}
