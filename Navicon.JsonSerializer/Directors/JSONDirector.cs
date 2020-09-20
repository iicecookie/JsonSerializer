using Navicon.Serializer.DAL;
using Navicon.Serializer.Models;
using Navicon.Serializer.Serializing;

namespace Navicon.Serializer
{
    public class JSONDirector
    {
        private readonly FileManager _fileManager;

        private readonly JsonSerializer _jsonSerializer;

        public JSONDirector()
        {
        }

        public JSONDirector(FileManager fileManager, JsonSerializer jsonSerializer) : base()
        {
            _fileManager = fileManager;

            _jsonSerializer = jsonSerializer;
        }

        public void Serialize(Contact person, string filePath = @"C:\Navicon\JsonSerializer")
        {
            string serializedContact = _jsonSerializer.Serialize(person);

            _fileManager.WriteInFile(serializedContact, $"{person.FirstName}_{person.SecondName}", filePath);
        }

        public Contact Deserialize(string fileName, string filePath = @"C:\Navicon\JsonSerializer")
        {
            string serializedContact = _fileManager.ReadFromFile(fileName, filePath);

            Contact contact = _jsonSerializer.Deserialize(serializedContact);

            return contact;
        }
    }
}
