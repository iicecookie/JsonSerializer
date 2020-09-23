using log4net;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Navicon.Serializer.DAL.ModelBuilder
{
    public class ModelBuilder
    {
        private readonly ILog _log;

        public ModelBuilder(ILog log)
        {
            _log = log;
        }

        /// <summary>
        /// Выполняет преобразование списка Contact в пригодный для записи в Excel формат
        /// сортирует и устанавливает идентификаторы
        /// </summary>
        /// <param name="contacts">Список контактов</param>
        /// <returns>Готовый к записи в Excel список контактов</returns>
        public IEnumerable<ExportContact> PrepeareContactsForExport(IEnumerable<Contact> contacts)
        {
            IEnumerable<ExportContact> excelContacts = ConvertContactToExcelContact(contacts);

            excelContacts = OrderExcelContactsByName(excelContacts);
            excelContacts = SetId(excelContacts);

            _log.Info($"ModelBuilder преобразовал контакты для экспорта");

            return excelContacts;
        }

        /// <summary>
        /// Преобразует тип Contact в пригодный для записи в Excel файл ExcelContact
        /// </summary>
        /// <param name="contacts">список контактов</param>
        /// <returns>список ExcelContact</returns>
        public IEnumerable<ExportContact> ConvertContactToExcelContact(IEnumerable<Contact> contacts)
        {
            var excelContacts = new List<ExportContact>();

            foreach(var contact in contacts)
            {
                var excelContact = new ExportContact();

                excelContact.FirstName  = contact.FirstName;
                excelContact.SecondName = contact.SecondName;
                excelContact.LastName   = contact.LastName;
                excelContact.ShortName  = GetShortName(contact.FirstName, contact.SecondName, contact.LastName);
                excelContact.ITN = contact.ITN;
                excelContact.PhoneNumber = GetPhoneNumberInMask(contact.PhoneNumber);
                excelContact.DateOfBirth = GetDateFormated(contact.DateOfBirth);
                excelContact.Country = contact.Address.Country;
                excelContact.Sity    = contact.Address.Sity;
                excelContact.Address = contact.Address.LifeLocation;

                excelContacts.Add(excelContact);
            }

            return excelContacts;
        }

        /// <summary>
        /// Выполняет сортировку списка контактов по фамилии, а затем по имяни
        /// </summary>
        /// <param name="contacts">Список контактов</param>
        /// <returns>Отсортированый список ExcelContact</returns>
        public IEnumerable<ExportContact> OrderExcelContactsByName(IEnumerable<ExportContact> contacts)
        {
            return contacts.OrderBy(sc => sc.SecondName).ThenBy(fi => fi.FirstName).ToList();
        }

        /// <summary>
        /// Последовательно устанавливает идентификаторы для каждого объекта в списке
        /// </summary>
        /// <param name="contacts">Список контактов</param>
        /// <returns>Список ExcelContact с установлеными идентификаторами</returns>
        public IEnumerable<ExportContact> SetId(IEnumerable<ExportContact> contacts)
        {
            int i = 1;
            foreach(var contact in contacts)
            {
                contact.Id = i++;
            }
            return contacts;
        }

        /// <summary>
        /// Приводит дату к строковому формату dd/M/yyyy
        /// </summary>
        /// <param name="data">Форматируемая дата</param>
        /// <returns>строка формата dd/M/yyyy</returns>
        private string GetDateFormated(DateTime data)
        {
            return data.ToString("dd/M/yyyy");
        }
        private string GetShortName(string firstName, string secondName, string lastName)
        {
            return $"{firstName} {secondName[0]}.{lastName[0]}.";
        }
        private string GetPhoneNumberInMask(string phoneNumber)
        {
            List<int> numbers = new List<int>();

            foreach(var num in phoneNumber)
            {
                if (char.IsNumber(num))
                {
                    numbers.Add(int.Parse(num.ToString()));
                }
            }

            return string.Format("+{0}({1}){2}-{3}-{4}",
                                        numbers[0],
                                        string.Join("", numbers.Skip(1).Take(3)),
                                        string.Join("", numbers.Skip(4).Take(3)),
                                        string.Join("", numbers.Skip(6).Take(2)),
                                        string.Join("", numbers.Skip(9).Take(2)));
        }
    }
}
