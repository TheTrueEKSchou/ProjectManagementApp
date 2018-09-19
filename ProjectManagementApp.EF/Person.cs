using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.EF
{
    public class Person
    {
        protected string firstName;
        protected string lastName;
        protected DateTime birthDate;
        protected string ssn;

        public Person(string firstName, string lastName, DateTime birthDate, string ssn)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Ssn = ssn;
        }

        public Person() { }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (Validate.IsPersonNameValid(value))
                {
                    firstName = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (Validate.IsPersonNameValid(value))
                {
                    lastName = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if(value < DateTime.Today)
                {
                    birthDate = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public string Ssn
        {
            get
            {
                return ssn;
            }
            set
            {
                if (Validate.IsSsnValid(value, out string ssn))
                {
                    this.ssn = ssn;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}
