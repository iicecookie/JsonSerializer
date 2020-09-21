using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Metadata.Attributes.Serializing;
using Navicon.Serializer.Models;
using Navicon.Serializer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Navicon.Serializer.Serializing
{
    public class JsonSerializer :ISerializer
    {
        public async Task<byte[]> GetContactsPackaged(IEnumerable<ExportContact> Contacts)
        {
            return Encoding.Unicode.GetBytes(Serialize(Contacts));
        }

        public string GetFileFormat()
        {
            return "json";
        }

        #region Serializing
        /// <summary>
        /// Преобразование списка объектов в JSON формат
        /// </summary>
        /// <param name="obj">Список объектов сериализации</param>
        /// <returns>представление объекта в JSON формате</returns>
        public string Serialize(List<Contact> contacts)
        {
            StringBuilder stringBuilder = new StringBuilder(200);

            foreach (var contact in contacts)
            {
                stringBuilder.Append(Serialize(contact));
                stringBuilder.Append(",");
            }

            // remove the last ','
            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Преобразование объекта в JSON формат
        /// </summary>
        /// <param name="obj">Объект сериализации</param>
        /// <returns>представление объекта в JSON формате</returns>
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
        /// Проверяет, сериализуемо ли свойство
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
        private string SerializePropertyValue(PropertyInfo propertyInfo, object obj)
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
        #endregion

        #region Deserializing
        /// <summary>
        /// Преобразование текста в объект
        /// </summary>
        /// <param name="serializedText">Текст, хранящий объект класса Json</param>
        /// <returns>
        /// Объект класса Contact, заполненый данными
        /// </returns>
        public Contact Deserialize(string serializedText)
        {
            var nameValuePairs = (Dictionary<string, object>)DeserializeJsonInDictionary(serializedText);

            Contact contact = new Contact();

            foreach (PropertyInfo propertyInfo in contact.GetType().GetProperties())
            {
                if (isSerializable(propertyInfo) == true)
                {
                    if      (propertyInfo.PropertyType.IsEnum)
                    {
                        Gender gender = (Gender)Enum.Parse(typeof(Gender), (string)nameValuePairs[propertyInfo.Name]);

                        propertyInfo.SetValue(contact, Convert.ChangeType(gender, propertyInfo.PropertyType), null);
                    }
                    else if (propertyInfo.PropertyType.Equals(typeof(DateTime)))
                    {
                        DateTime birthday = DateTime.Parse((string)nameValuePairs[propertyInfo.Name]);

                        propertyInfo.SetValue(contact, birthday, null);
                    }
                    else if (propertyInfo.PropertyType.Equals(typeof(Address)))
                    {
                        Address address = new Address();

                        Dictionary<string, object> addressNameValue = (Dictionary<string, object>)nameValuePairs[propertyInfo.Name];

                        foreach (PropertyInfo propertyAddressInfo in address.GetType().GetProperties())
                        {
                            if (propertyAddressInfo.PropertyType.IsEnum)
                            {
                                AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), (string)addressNameValue[propertyAddressInfo.Name]);

                                propertyAddressInfo.SetValue(address, Convert.ChangeType(addressType, propertyAddressInfo.PropertyType), null);
                            }
                            else
                            {
                                propertyAddressInfo.SetValue(address, addressNameValue[propertyAddressInfo.Name], null);
                            }
                        }

                        propertyInfo.SetValue(contact, address, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(contact, nameValuePairs[propertyInfo.Name], null);
                    }
                }
            }

            return contact;
        }

        /// <summary>
        /// Преобразует текст Json формата в Dictionary<name,value> 
        /// </summary>
        /// <param name="serializedText">Текст Json формата</param>
        /// <returns>Dictionary<name,value></returns>
        private object DeserializeJsonInDictionary(string serializedText)
        {
            var nameValues = new Dictionary<string, object>();

            var stringBuilder = new StringBuilder(serializedText);

            for (int i = 0; i < stringBuilder.Length; i++)
            {
                if (stringBuilder[i] == ':')
                {
                    string name = GetName(stringBuilder.ToString(), i);

                    if (stringBuilder.Length == i + 1) i--;
                    stringBuilder = stringBuilder.Remove(0, i + 1);

                    int shift = 0;
                    object value = GetValue(stringBuilder.ToString(), ref shift);

                    if (stringBuilder.Length >= shift + 2)
                        stringBuilder = stringBuilder.Remove(0, shift + 2);

                    i = 0;

                    nameValues.Add(name, value);
                }
            }

            return nameValues;
        }

        /// <summary>
        /// Извлекает имя переменной из текста
        /// </summary>
        /// <param name="serializedText">Текст в Json формате</param>
        /// <param name="nameLength">Длина имени переменной</param>
        /// <returns></returns>
        private string GetName(string serializedText, int nameLength)
        {
            return serializedText.Substring(0, nameLength - 1).Trim(new char[] { ',', '"', '{' });
        }

        /// <summary>
        /// Получение текстового значения переменой
        /// </summary>
        /// <param name="serializedText">Описние значения атрибута</param>
        /// <param name="shiftValue">Количество символов значения</param>
        /// <returns>
        /// Возвращает строковое значение переменной,
        /// Или Dictonary, если значение - объект класса
        /// </returns>
        private object GetValue(string serializedText, ref int shiftValue)
        {
            if (serializedText[0] != '{')
            {
                string serializedValue = string.Join("", serializedText.TakeWhile(ch => ch != ',')).Trim('"');
                
                shiftValue = serializedValue.Length;
                
                return serializedValue;
            }
            else
            {
                int endOfAddress = serializedText.IndexOf('}');

                var serializedValue = serializedText.Substring(1, endOfAddress - 1);

                shiftValue = serializedValue.Length;

                return DeserializeJsonInDictionary(serializedValue);
            }
        }
        #endregion
    }
}