using Navicon.JsonSerializer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.JsonSerializer.Serializer
{
    public class JsonSerializer
    {
        private FileManager _fileManager;

        private ContactSerializer _contactSerializer;

        public JsonSerializer()
        {
        }

        public JsonSerializer(FileManager fileManager, ContactSerializer contactSerializer) : base()
        {
            _fileManager = fileManager;

            _contactSerializer = contactSerializer;


        }

        public void Serialize(Contact person)
        {
            string serializedContact = _contactSerializer.Serialize(person);

            _fileManager.WriteInFile(serializedContact, $"{person.FirstName}_{person.SecondName}");
        }

        public Contact Deserialize(string fileName, string filePath = @"C:\Navicon\JsonSerializer")
        {
            string serializedContact = _fileManager.ReadFromFile(fileName, filePath);

            Contact contact = _contactSerializer.Deserialize(serializedContact);

            return contact;
        }
    }
}
