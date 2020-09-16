﻿using Navicon.Serializer.DAL.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using OfficeOpenXml;

namespace Navicon.Serializer.Serializing.EXCEL
{
    public class ExcelSerializer
    {
        /// <summary>
        /// Создает Excel файл и одной страницей
        /// </summary>
        /// <returns></returns>
        public ExcelPackage CreateExcelFile()
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
        /// Добавляет все контакты построчно в указаный Excel документ на первую страницу
        /// </summary>
        /// <param name="excelContacts">Список контактов, подготовленных к записи</param>
        /// <param name="excelPackage">Excel файл</param>
        public void AddExcelContactToExcel(List<ExcelContact> excelContacts, ExcelPackage excelPackage)
        {
            AddColumnTitles(excelPackage, excelContacts.First());

            PropertyInfo[] propertyesInfo = excelContacts.First().GetType().GetProperties();

            const int ROW_AFTER_TITLE = 2;

            for (int row= 0; row < excelContacts.Count; row++)
            {
                for (int column= 0; column < propertyesInfo.Length; column++)
                {
                    excelPackage.Workbook.Worksheets[0].Cells[ROW_AFTER_TITLE + row, column + 1].Value = 
                                                                        propertyesInfo[column].GetValue(excelContacts[row]);
                }
            }
        }

        /// <summary>
        /// Добавляет в первую строку первой Excel страницы имена полей ExcelContact
        /// </summary>
        /// <param name="excelPackage">Excel Файл</param>
        /// <param name="contact"></param>
        /// <returns></returns>
        public ExcelPackage AddColumnTitles(ExcelPackage excelPackage, ExcelContact contact)
        {
            PropertyInfo[] propertyesInfo = contact.GetType().GetProperties();

            for (int i = 0; i < propertyesInfo.Length; i++)
            {
                excelPackage.Workbook.Worksheets[0].Cells[1, i + 1].Value = propertyesInfo[i].Name;
            }
            return excelPackage;
        }
    }
}
