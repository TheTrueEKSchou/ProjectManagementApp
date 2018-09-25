using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.EF
{
    public partial class Employee : Person
    {
        protected DateTime startDate;
        protected decimal salary;

        public Employee(string firstName, string lastName, DateTime birthDate, string ssn, string privatePhone, string privateEmail, DateTime startDate, decimal salary)
            : base(firstName, lastName, birthDate, ssn, privatePhone, privateEmail)
        {
            StartDate = startDate;
            Salary = salary;
        }

        public Employee() : base() { }

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

        public decimal Salary
        {
            get
            {
                return salary;
            }
            set
            {
                if(value >= 0.0m)
                {
                    salary = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
