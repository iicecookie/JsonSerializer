using Navicon.Serializer.DAL.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using OfficeOpenXml;
using System.Threading.Tasks;

namespace Navicon.Serializer.Serializing
{
    public class ExcelSerializer : ISerializer
    {
        /// <summary>
        /// Добавляет все контакты построчно в указаный Excel документ на первую страницу
        /// </summary>
        /// <param name="excelContacts">Список контактов, подготовленных к записи</param>
        /// <param name="excelPackage">Excel файл</param>
        public async Task<byte[]> GetContactsPackaged(IEnumerable<ExportContact> excelContacts)
        {
            ExcelPackage excelPackage = CreateExcelFile();

            AddColumnTitles(excelPackage, excelContacts.First());

            PropertyInfo[] propertiesInfo = excelContacts.First().GetType().GetProperties();

            const int ROW_AFTER_TITLE = 2;

            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            for (int row = 0; row < excelContacts.Count(); row++)
            {
                var contact = excelContacts.ElementAt(row);

                for (int column = 0; column < propertiesInfo.Length; column++)
                {
                    worksheet.Cells[ROW_AFTER_TITLE + row, column + 1].Value = propertiesInfo[column].GetValue(contact);
                }
            }

            return excelPackage.GetAsByteArray();
        }

        /// <summary>
        /// Создает Excel файл и одной страницей
        /// </summary>
        /// <returns></returns>
        private ExcelPackage CreateExcelFile()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage();

            excelPackage.Workbook.Properties.Author = "COOKIE";
            excelPackage.Workbook.Properties.Title = "List of the Contacts";
            excelPackage.Workbook.Properties.Created = DateTime.Now;

            excelPackage.Workbook.Worksheets.Add("Contacts");

            return excelPackage;
        }

        /// <summary>
        /// Добавляет в первую строку первой Excel страницы имена полей ExcelContact
        /// </summary>
        /// <param name="excelPackage">Excel Файл</param>
        /// <param name="contact"></param>
        /// <returns></returns>
        private ExcelPackage AddColumnTitles(ExcelPackage excelPackage, ExportContact contact)
        {
            PropertyInfo[] propertiesInfo = contact.GetType().GetProperties();

            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            for (int i = 0; i < propertiesInfo.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = propertiesInfo[i].Name;
            }
            return excelPackage;
        }

        public string GetFileFormat()
        {
            return "xlsx";
        }
    }
}
