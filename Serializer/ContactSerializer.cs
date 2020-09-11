using Navicon.JsonSerializer.Models;
using Navicon.JsonSerializer.Models.Enums;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Navicon.JsonSerializer.Serializer
{
    public class ContactSerializer
    {
        private string _filename;

        public ContactSerializer(string filename)
        {
            _filename = filename;
        }

        public string Serialize(Contact person)
        {
            StringBuilder stringBuilder = new StringBuilder(200);

            stringBuilder.Append("{");

            foreach (PropertyInfo propertyInfo in person.GetType().GetProperties())
            {
                stringBuilder.Append($"\"{propertyInfo.Name}\":");

                if (propertyInfo.PropertyType == string.Empty.GetType() ||
                    propertyInfo.PropertyType == DateTime.Now.GetType())
                {
                    stringBuilder.Append($"\"{propertyInfo.GetValue(person)}\"");
                }
                else
                {
                    stringBuilder.Append($"{propertyInfo.GetValue(person)}");
                }
                stringBuilder.Append(",");
            }

            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        public Contact Deserialize(string serializedText)
        {
            Contact contact = new Contact();

            Console.WriteLine(serializedText);
            var lines = serializedText.Split(',');

            for (int i = 0; i < lines.Length - 1; i++)
            {
                var (attributeName, attributeValue) = GetNameAndValue(lines[i]);

                foreach (PropertyInfo propertyInfo in contact.GetType().GetProperties())
                {
                    if (propertyInfo.Name == attributeName && attributeName == "Gender")
                    {
                        Gender gender = (Gender)Enum.Parse(typeof(Gender), attributeValue);

                        propertyInfo.SetValue(contact, Convert.ChangeType(gender, propertyInfo.PropertyType), null);
                        break;
                    }
                    else if (propertyInfo.Name == attributeName)
                    {
                        propertyInfo.SetValue(contact, Convert.ChangeType(attributeValue, propertyInfo.PropertyType), null);
                    }
                }
            }

            return contact;
        }

        private (string, string) GetNameAndValue(string line)
        {
            var oneLine = line.Split(new char[] { ':' }, 2);

            int pFrom = oneLine[0].IndexOf("\"") + "\"".Length;
            int pTo = oneLine[0].LastIndexOf("\"");
            string attributeName = oneLine[0].Substring(pFrom, pTo - pFrom);

            pFrom = oneLine[1].IndexOf("\"") + "\"".Length;
            pTo = oneLine[1].LastIndexOf("\"");
            if (pTo < 0) pTo = oneLine[1].Length;

            string attributeValue = oneLine[1].Substring(pFrom, pTo - pFrom);

            return (attributeName, attributeValue);
        }
    }
}
