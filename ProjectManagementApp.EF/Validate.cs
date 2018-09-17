using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectManagementApp.EF
{
    static class Validate
    {
        public static bool IsPersonNameValid(string s)
        {
            Regex regex = new Regex("^[A-Z][a-z][A-Za-z]*$");
            if (regex.IsMatch(s))
                return true;
            else
                return false;
        }

        public static bool IsEmailValid(string s)
        {
            Regex regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (regex.IsMatch(s))
                return true;
            else
                return false;
        }

        public static bool IsPhoneValid(string s)
        {
            Regex regex = new Regex(@"^(\+45 )?(\d ?){7}\d$");//For Danish phone numbers
            //Regex regex = new Regex(@"^(\+\d{1,3} )?(\d ?)+\d$");//For international phone numbers
            if (regex.IsMatch(s))
                return true;
            else
                return false;
        }

        public static bool IsBirthDateValid(DateTime date)
        {
            if(date < DateTime.Today)
                return true;
            else
                return false;
        }

        public static bool IsSsnValid(string s)
        {
            Regex regex = new Regex(@"^\d{6}[- ]?\d{4}");
            if (regex.IsMatch(s))
                return true;
            else
                return false;
        }

        public static bool IsEntityNameValid(string s)
        {
            Regex regex = new Regex("^[A-Z][A-Za-z0-9]{0,49}$");
            if (regex.IsMatch(s))
                return true;
            else
                return false;
        }
    }
}