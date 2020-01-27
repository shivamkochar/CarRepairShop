using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRepairShop
{
    public class CustomerList 
    {
        private string firstName;
        private string lastName;
        private int age;
        private string city;
        private int accountNumber;

        
        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public string City
        {
            get => city;
            set => city = value;
        }

        public int AccountNumber
        {
            get => accountNumber;
            set => accountNumber = value;
        }
    }
}
