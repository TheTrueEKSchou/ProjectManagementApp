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
            Regex regex = new Regex("^[A-Z][a-z][A-Za-z]+$");
            if (regex.IsMatch(s))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEntityNameValid(string s)
        {
            Regex regex = new Regex("^[A-Z][A-Za-z0-9]{0-49}$");
            if (regex.IsMatch(s))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
