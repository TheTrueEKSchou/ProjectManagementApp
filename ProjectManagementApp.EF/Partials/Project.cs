using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.EF
{
    public partial class Project
    {
        public decimal CalulculatePay()
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
