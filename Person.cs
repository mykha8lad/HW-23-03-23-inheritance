using ExceptionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HW_23_03_23_inheritance
{
    public class Person
    {
        private string name;
        private string lastname;
        private string surname;
        private string phoneNumber;
        private DateTime birthday;
        private Address address;

        public string Name
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }
        public string Lastname
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.lastname = value;
            }
            get
            {
                return this.lastname;
            }
        }
        public string Surname
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.surname = value;
            }
            get
            {
                return this.surname;
            }
        }
        public string PhoneNumber
        {
            set
            {
                string phoneRegexp = @"^\(\d{3}\)\d{3}\-\d{4}$";
                if (Regex.IsMatch(value, phoneRegexp)) this.phoneNumber = value;
                else this.phoneNumber = "(000)000-0000";
            }
            get
            {
                return this.phoneNumber;
            }
        }
        public DateTime Birthday
        {
            set
            {
                this.birthday = value;
            }
            get
            {
                return this.birthday;
            }
        }
        public Address Address
        {
            set
            {
                this.address = value;
            }
            get
            {
                return this.address;
            }
        }

        public Person(string name, string lastname, string surname, DateTime birthday, string phoneNumber, string city, string street, string homeNumber)
            : base()
        {
            Name = name;
            Lastname = lastname;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
            Address = new Address(city, street, homeNumber);
        }
        public Person(string name, string lastname, string surname, DateTime birthday, string phoneNumber) :
            this(name, lastname, surname, birthday, phoneNumber, "None", "None", "None")
        { }
        public Person(string name, string lastname, string surname) :
            this(name, lastname, surname, new DateTime(1, 1, 1), "(000)000-0000", "None", "None", "None")
        { }
        public Person() :
            this("None", "None", "None", new DateTime(1, 1, 1), "(000)000-0000", "None", "None", "None")
        { }

        public override string ToString()
        {
            return ($"Person: {Lastname} {Name} {Surname}\n" +
                $"Birthday: {Birthday.Date.ToString("d")}\n" +
                $"Address: {Address}\n" +
                $"Phone number: {PhoneNumber}\n");
        }
    }
}
