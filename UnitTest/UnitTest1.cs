using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagementApp.EF;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        //Tests to see if the ssn is formatted correctly.
        [TestMethod]
        public void TestEmployeeSsnSuccess()
        {
            string firstName = "Lars";
            string lastName = "Larsen";
            DateTime birthDate = Convert.ToDateTime("1999-12-12");
            string ssn = "124578 4578";
            DateTime startDate = Convert.ToDateTime("2018-09-10");
            decimal salary = 300.0m;
            Employee employee = new Employee(firstName,lastName,birthDate,ssn,startDate,salary);

            Assert.AreEqual("124578-4578",employee.Ssn);
        }

        //Tests to see if an exception is thrown when a negative salary is being used.
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEmployeeSsnSalary()
        {
            string firstName = "Lars";
            string lastName = "Larsen";
            DateTime birthDate = Convert.ToDateTime("1999-12-12");
            string ssn = "124578 4578";
            DateTime startDate = Convert.ToDateTime("2018-09-10");
            decimal salary = -300.0m;
            Employee employee = new Employee(firstName, lastName, birthDate, ssn, startDate, salary);
        }
    }
}
