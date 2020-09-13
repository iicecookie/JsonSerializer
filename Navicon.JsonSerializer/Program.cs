using Navicon.JsonSerializer.Models;
using Navicon.JsonSerializer.Models.Enums;
using Navicon.JsonSerializer.Serializer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Navicon.JsonSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Contact contact1 = new Contact(
                firstName: "Maxim",
                secondName: "Bukanov",
                lastName: "...",
                gender: Gender.Female,
                address: new Address()
                {
                    Sity = "Moscow",
                    AddressType = AddressType.Actual,
                    Country = "Russia",
                    LifeLocation = "213"
                },
                birth: new DateTime(1998, 11, 22),
                itn: "123124141241",
                phoneNumber: "+7(925)561-84-06"
            );

            Contact contact2 = new Contact(
                firstName: "Maxim",
                secondName: "Bukanov",
                lastName: "...",
                gender: Gender.Female,
                address: new Address()
                {
                    Sity = "Moscow",
                    AddressType = AddressType.Actual,
                    Country = "Russia",
                    LifeLocation = "..."
                },
                birth: new DateTime(1998, 11, 22),
                itn: "123124141241",
                phoneNumber: "+7(925)561-84-06"
                );

            List<Contact> contacts = new List<Contact>();
            contacts.Add(contact1);
            contacts.Add(contact2);

            Console.WriteLine(contacts[0]);

            var serializer = new ContactSerializer(new FileManager(), "firstTry");
            
            var serializedContact = serializer.Serialize(contacts[0]);
            Console.WriteLine(serializedContact);
            Console.WriteLine("\n");


            Contact deserializedContact = serializer.Deserialize(serializedContact);
            Console.WriteLine(deserializedContact);
            Console.WriteLine("\n");

            // FileManager fileManager = new FileManager();
            // fileManager.WriteInFile("jojo", "joker");
        }

        public static bool ValidateModel(object obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);

            bool isValide = true;

            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                    isValide = !isValide;
                }
            }
            return isValide;
        }
    }
}
