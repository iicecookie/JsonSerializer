using Navicon.JsonSerializer.Models;
using Navicon.JsonSerializer.Models.Enums;
using Navicon.JsonSerializer.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Navicon.JsonSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Contact contact = new Contact(
                firstName   : "Maxim",
                secondName  : "Bukanov",
                lastName    : "...",
                gender      : Gender.Female,
                address: new Address()
                {
                    Sity = "Moscow",
                    AddressType = AddressType.Actual,
                    Country = "Russia",
                    LifeLocation = ""
                },
                birth : new DateTime(1998, 11, 22),
                itn         : "123124141241",
                phoneNumber : "+7(925)561-84-06"
            );

            Console.WriteLine(contact);

      //      var serializer = new ContactSerializer("firstTry");
      //
      //      var serializedContact = serializer.Serialize(contact);
      //      Console.WriteLine(serializedContact);
      //      Console.WriteLine("\n");
      //
      //      Contact deserializedContact = serializer.Deserialize(serializedContact);
      //      Console.WriteLine(deserializedContact);
      //      Console.WriteLine("\n");
      //
      //      Console.WriteLine(deserializedContact);
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
