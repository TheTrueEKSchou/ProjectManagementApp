using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.EF
{
    public partial class Team
    {
        public decimal CalculatePay()
        {
            decimal pay = 0.0m;
            foreach(Employee employee in Employees)
            {
                pay += employee.Salary;
            }
            return pay;
        }
    }
}
