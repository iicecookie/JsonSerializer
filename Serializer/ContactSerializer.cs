using Navicon.JsonSerializer.Metadata.Attributes.Serializing;
using Navicon.JsonSerializer.Models;
using Navicon.JsonSerializer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Navicon.JsonSerializer.Serializer
{
    public class ContactSerializer
    {
        private string _filename; // удалить

        private readonly PropertyInfo[] contactPropertyes;

        public ContactSerializer(string filename)
        {
            _filename = filename;

            contactPropertyes = typeof(Contact).GetProperties();
        }

        public string Serialize(object obj)
        {
            StringBuilder stringBuilder = new StringBuilder(200);

            stringBuilder.Append("{");

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (isSerializable(propertyInfo) == false) continue;

                stringBuilder.Append($"\"{propertyInfo.Name}\":");

                stringBuilder.Append(SerializePropertyValue(propertyInfo, obj));

                stringBuilder.Append(",");
            }

            // remove the last ','
            stringBuilder.Remove(stringBuilder.Length - 1, 1); 

            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Проверяет атрибуты свойства
        /// </summary>
        /// <param name="propertyInfo">Свойство класса</param>
        /// <returns>
        /// true - если у свойства есть атрибут Serializable
        /// falce - если у свойства есть атрибут NonSerializable или отсутствует атрибут, реализующий ISerializable
        /// </returns>
        private bool isSerializable(PropertyInfo propertyInfo)
        {
            object[] customAttributes = propertyInfo.GetCustomAttributes(true);

            var serializableInterface = customAttributes.Where(attr => attr.GetType()
                                                               .GetInterfaces()
                                                               .Where(i => i.Name == "ISerializable")
                                                               .FirstOrDefault() != null)
                                                        .FirstOrDefault();

            if (serializableInterface == null) return false;

            return ((ISerializable)serializableInterface).isSerializable;
        }

        /// <summary>
        /// Сериализует значение свойства 
        /// </summary>
        /// <param name="propertyInfo">информация о свойстве</param>
        /// <param name="obj">класс-владелец свойства</param>
        /// <returns>сериализованое представление значения</returns>
        public string SerializePropertyValue(PropertyInfo propertyInfo, object obj)
        {
            if (propertyInfo.PropertyType == typeof(Address))
            {
                return Serialize((obj as Contact).Address);
            }
            else if (propertyInfo.PropertyType == typeof(string))
            {
                return $"\"{propertyInfo.GetValue(obj)}\"";
            }
            else if (propertyInfo.PropertyType.IsEnum)
            {
                return $"{(int)propertyInfo.GetValue(obj)}";
            }
            else if (propertyInfo.PropertyType == typeof(DateTime))
            {
                return $"\"{((DateTime)propertyInfo.GetValue(obj)).ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}\"";
            }
            else
            {
                // числовые типы без кавычек
                return $"{propertyInfo.GetValue(obj)}";
            }
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
