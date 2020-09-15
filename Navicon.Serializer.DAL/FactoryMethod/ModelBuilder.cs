using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navicon.Serializer.DAL.ModelBuilder
{
    public class ModelBuilder
    {
        public List<ExcelContact> PrepeareContactForExcel(List<Contact> contacts)
        {
            List<ExcelContact> excelContacts = ConvertContactToExcelContact(contacts);

            OrderExcelContactsByName(excelContacts);
            SetId(excelContacts);

            return excelContacts;
        }

        public List<ExcelContact> ConvertContactToExcelContact(List<Contact>contacts)
        {
            var excelContacts = new List<ExcelContact>();

            foreach(var contact in contacts)
            {
                var excelContact = new ExcelContact();

                excelContact.FirstName  = contact.FirstName;
                excelContact.SecondName = contact.SecondName;
                excelContact.LastName   = contact.LastName;
                excelContact.ShortName  = GetShortName(contact.FirstName, contact.SecondName, contact.LastName);
                excelContact.ITN = contact.ITN;
                excelContact.PhoneNumber = contact.PhoneNumber;
                excelContact.DateOfBirth = GetDateFormated(contact.DateOfBirth);
                excelContact.Country = contact.Address.Country;
                excelContact.Sity    = contact.Address.Sity;
                excelContact.Address = contact.Address.LifeLocation;

                excelContacts.Add(excelContact);
            }

            return excelContacts;
        }

        public List<ExcelContact> OrderExcelContactsByName(List<ExcelContact> contacts)
        {
            return contacts.OrderBy(sc => sc.SecondName).ThenBy(fi => fi.FirstName).ToList();
        }

        public List<ExcelContact> SetId(List<ExcelContact> contacts)
        {
            int i = 1;
            foreach(var contact in contacts)
            {
                contact.Id = i++;
            }
            return contacts;
        }

        private string GetDateFormated(DateTime data)
        {
            return data.ToString("dd/M/yyyy");
        }
        private string GetShortName(string firstName, string secondName, string lastName)
        {
            return $"{firstName} {secondName[0]}.{lastName[0]}.";
        }
    }
}
