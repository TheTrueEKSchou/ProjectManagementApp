using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.EF
{
    public partial class Team
    {
        protected string name;
        protected DateTime startDate;

        public Team(string name, DateTime startDate)
        {
            Name = name;
            StartDate = startDate;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Validate.IsEntityNameValid(value))
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if(value != null)
                {
                    startDate = value;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

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
