using Navicon.JsonSerializer.Models;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Linq;
using System;
using System.IO;

namespace Navicon.XSLXSerializer.Serializer
{
    public static class ExcelSerializer
    {
        public static void Serialize(List<Contact> contacts, string fileName)
        {
            contacts = contacts.OrderBy(or => or.SecondName).ThenBy(th => th.FirstName).ToList();

            //Create a new ExcelPackage
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "VDWWD";
                excelPackage.Workbook.Properties.Title = "Title of Document";
                excelPackage.Workbook.Properties.Subject = "EPPlus demo export data";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                worksheet.Cells[0, 1].Value = "Порядковый номер";
                worksheet.Cells[0, 2].Value = "Краткое имя";
                worksheet.Cells[0, 3].Value = "Фамилия";
                worksheet.Cells[0, 4].Value = "Имя";
                worksheet.Cells[0, 5].Value = "Отчество";
                worksheet.Cells[0, 6].Value = "Дата рождения";
                worksheet.Cells[0, 7].Value = "ИНН";
                worksheet.Cells[0, 8].Value = "Телефон";
                worksheet.Cells[0, 9].Value = "Страна";
                worksheet.Cells[0, 10].Value = "Город";
                worksheet.Cells[0, 11].Value = "Адрес";



                //Add some text to cell A1
                worksheet.Cells["A1"].Value = "My first EPPlus spreadsheet!";
                //You could also use [line, column] notation:
                worksheet.Cells[1, 2].Value = "This is cell B1!";

                //Save your file
                FileInfo fi = new FileInfo(@"C:\Users\Cookie\Downloads\File.xlsx");
                excelPackage.SaveAs(fi);
            }
        }
    }
}
