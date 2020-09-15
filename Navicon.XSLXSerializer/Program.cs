using Navicon.Serializer.Models;
using Navicon.Serializer.Models.Enums;
using Navicon.XSLXSerializer.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Navicon.XSLXSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> firstNames = new List<string> {
                                            "Liam", "Olivia", "Noah", 
                                            "Emma", "Oliver" , "Ava", 
                                            "William", "Sophia" , "Elijah", 
                                            "Isabella", "James", "Charlotte",
                                            "Benjamin", "Amelia", "Lucas",
                                            "Mia", "Mason", "Harper", 
                                            "Ethan" , "Evelyn" 
            };
            List<string> secondNames = new List<string> {
                                            "Smith", "Johnson", "Williams",
                                            "Jones", "Brown", "Davis", 
                                            "Miller","Wilson",
            };
            List<string> lastNames = new List<string>
            {
                                            "Smith", "Johnson", "Williams",
                                            "Brown", "Jones", "Garcia",
                                            "Miller", "Davis",
            };
            List<string> countries = new List<string>
            {
                                            "Norway", "Switzerland", "Australia",
                                            "Ireland", "Germany", "Iceland",
                                            "Sweden", "Hong Kong",
            };
            List<string> sityes = new List<string>
            {
                                            "Helsinki", "Tokyo", "Vancouver",
                                            "Melbourne", "Munich", "Copenhagen",
                                            "Zurich", "Vienna",
            };

            List<Contact> contacts = new List<Contact>(10);

            for (int i = 0; i < contacts.Capacity; i++)
            {
                var contact = new Contact();

                Random random = new Random();

                contact.FirstName = firstNames[random.Next(0,20)];
                contact.SecondName = secondNames[random.Next(0, 7)];
                contact.LastName = lastNames[random.Next(0, 7)];
                contact.ITN = random.Next(0, 123121212).ToString();
                contact.DateOfBirth = new DateTime(1980, 1, 1).AddDays(random.Next(0, 10000));

                Array values = Enum.GetValues(typeof(Gender));
                contact.Gender = (Gender)values.GetValue(random.Next(values.Length));
                contact.PhoneNumber = "+7(906)634-23-" + random.Next(11, 99).ToString();

                Address address = new Address();
                address.Country = countries[random.Next(0, 7)];
                address.Sity = sityes[random.Next(0, 7)];

                values = Enum.GetValues(typeof(Gender));
                address.AddressType = (AddressType)values.GetValue(random.Next(values.Length));
                address.LifeLocation = "...";

                contact.Address = address;

                contacts.Add(contact);
            }

            foreach(var contact in contacts)
            {
                Console.WriteLine(contact);
            }

            Console.WriteLine("Enter a Filename");

            var filename = Console.ReadLine();

            ExcelSerializer.Serialize(contacts, filename);
        }
    }
}
