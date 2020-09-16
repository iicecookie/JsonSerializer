using Navicon.Serializer.DAL.Interfaces;
using Navicon.Serializer.Models;
using Navicon.Serializer.Models.Enums;
using System;
using System.Collections.Generic;

namespace Navicon.Serializer.DAL.DataSorce
{
    public class ContactFabric: IDataSorce
    {
        private List<string> firstNames = new List<string> {
                                            "Liam", "Olivia", "Noah",
                                            "Emma", "Oliver" , "Ava",
                                            "William", "Sophia" , "Elijah",
                                            "Isabella", "James", "Charlotte",
                                            "Benjamin", "Amelia", "Lucas",
                                            "Mia", "Mason", "Harper",
                                            "Ethan" , "Evelyn"
        };
        private List<string> secondNames = new List<string> {
                                            "Smith", "Johnson", "Williams",
                                            "Jones", "Brown", "Davis",
                                            "Miller","Wilson",
        };
        private List<string> lastNames = new List<string>
            {
                                            "Smith", "Johnson", "Williams",
                                            "Brown", "Jones", "Garcia",
                                            "Miller", "Davis",
        };
        private List<string> countries = new List<string>
            {
                                            "Norway", "Switzerland", "Australia",
                                            "Ireland", "Germany", "Iceland",
                                            "Sweden", "Hong Kong",
        };
        private List<string> sityes = new List<string>
            {
                                            "Helsinki", "Tokyo", "Vancouver",
                                            "Melbourne", "Munich", "Copenhagen",
                                            "Zurich", "Vienna",
        };

        /// <summary>
        /// Создает список случайно сгенерированых контактов
        /// </summary>
        /// <param name="count">количество нужных контактов</param>
        /// <returns>Список контактов</returns>
        public List<Contact> GetContacts(int count = 10)
        {
            List<Contact> contacts = new List<Contact>(count);

            for (int i = 0; i < contacts.Capacity; i++)
            {
                var contact = new Contact();

                Random random = new Random();

                contact.FirstName = firstNames[random.Next(0, 20)];
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
            return contacts;
        }
    }
}