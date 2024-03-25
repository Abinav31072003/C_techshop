using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task1.Exceptions
{
    internal class DataValidationException:Exception
    {
        public DataValidationException(string message) : base(message)
        {
        }
        //email validation
        public static bool IsValidEmail(string email)
        {

            return email.Contains("@");
        }
        //phone number validation
        public static bool IsValidPhoneNumber(string phoneNumber)
        {

            string pattern = @"^\d{10}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
        }
        //Quantity in stock validation
        public static bool ValidateQuantity(int value)
        {

            return value > 0;
        }
        // date time format validation
        public static bool IsValidDateTime(DateTime value)
        {

            return value.Year >= 1900 && value.Year <= DateTime.Now.Year;
        }
    }
}
