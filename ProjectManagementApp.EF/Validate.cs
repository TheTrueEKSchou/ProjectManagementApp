using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectManagementApp.EF
{
    public static class Validate
    {
        public static bool IsPersonNameValid(string s)
        {
            Regex regex = new Regex("^[A-Z][a-z][A-Z]?[a-z]{0,47}$");
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

        public static bool IsPhoneValid(string s, out string phone)
        {
            phone = s;
            int index = phone.IndexOf(" ");
            if(index != -1)
            {
                phone = phone.Remove(index, 1).Insert(index, "_");
            }
            //Regex regex = new Regex(@"^(\+45 ?)?(\d ?){7}\d$");//For Danish phone numbers
            Regex regex = new Regex(@"^(\+\d{1,3} ?)?(\d ?)+\d$");//For international phone numbers
            if (regex.IsMatch(s))
            {
                if (phone.Contains("+45"))
                {
                    phone = phone.Remove(0,3);
                    phone = phone.Replace("_", "");
                }
                else if (!phone.Contains("+"))
                {
                    phone = phone.Replace("_", "");
                }
                phone = phone.Replace(" ", "");
                phone = phone.Replace("_", " ");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDateValid(string s, out DateTime date)
        {
            bool dateBool = DateTime.TryParse(s,out date);
            if (dateBool)
            {
                if (date < DateTime.Today)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public static bool IsSsnValid(string s, out string ssn)
        {
            ssn = s;
            Regex regex = new Regex(@"^\d{6}[- ]?\d{4}");
            if (regex.IsMatch(s))
            {
                if (s.Contains(" "))
                {
                    ssn = s.Replace(" ","-");
                }
                else if (!s.Contains("-"))
                {
                    ssn = s.Insert(6,"-");
                }
                return true;
            }
            else
            {
                return false;
            }
                
        }

        public static bool IsSalaryValid(string s, out decimal salary)
        {
            Regex regex = new Regex(@"^\d+([\.,]\d+)?");
            if (regex.IsMatch(s))
            {
                salary = Convert.ToDecimal(s);
                return true;
            }
            else
            {
                salary = default;
                return false;
            }
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