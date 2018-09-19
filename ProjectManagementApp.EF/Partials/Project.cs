using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.EF
{
    public partial class Project
    {
        protected string name;
        protected DateTime startDate;
        protected decimal? budgetLimit;

        public Project(string name, DateTime startDate, decimal budgetLimit)
            : this(name, startDate)
        {
            BudgetLimit = budgetLimit;
        }

        public Project(string name, DateTime startDate)
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

        public decimal? BudgetLimit
        {
            get
            {
                return budgetLimit;
            }
            set
            {
                if(value >= 0.0m || value == null)
                {
                    budgetLimit = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Calculates the total salary of all employees of all the teams in the project.
        /// </summary>
        /// <returns></returns>
        public decimal CalculatePay()
        {
            decimal pay = 0.0m;
            foreach(Team team in Teams)
            {
                pay += team.CalculatePay();
            }
            return pay;
        }
    }
}
