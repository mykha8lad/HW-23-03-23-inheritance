using ExceptionLibrary;
using System;

namespace HW_23_03_23_inheritance
{
    public class Address
    {
        private string city;
        private string street;
        private string homeNumber;

        public string City
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.city = value;
            }
            get
            {
                return this.city;
            }
        }
        public string Street
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.street = value;
            }
            get
            {
                return this.street;
            }
        }
        public string HomeNumber
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.homeNumber = value;
            }
            get
            {
                return this.homeNumber;
            }
        }

        public Address(string city, string street, string homeNumber)
        {
            City = city;
            Street = street;
            HomeNumber = homeNumber;
        }        

        public override string ToString()
        {
            return ($"{City}, {Street} {HomeNumber}");
        }
    }
}
