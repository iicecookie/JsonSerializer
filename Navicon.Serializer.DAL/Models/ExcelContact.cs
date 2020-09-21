using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.Serializer.DAL.Models
{
    public sealed class ExportContact
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string ITN { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Sity { get; set; }
        public string Address { get; set; }

        public ExportContact() { }

        public ExportContact(int id, string shortName, string firstName, 
                              string secondName, string lastName, string dateOfBirth, 
                              string iTN, string phoneNumber, string country, 
                              string sity, string address)
        {
            Id = id;
            ShortName = shortName;
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            ITN = iTN;
            PhoneNumber = phoneNumber;
            Country = country;
            Sity = sity;
            Address = address;
        }
    }
}
