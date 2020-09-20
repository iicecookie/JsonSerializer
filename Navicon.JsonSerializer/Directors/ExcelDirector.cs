using Navicon.Serializer.DAL;
using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Models;
using Navicon.Serializer.Serializing.EXCEL;
using Navicon.Serializer.DAL.Interfaces;
using System.Collections.Generic;
using OfficeOpenXml;
using Navicon.Serializer.Serializing.CSV;

namespace Navicon.Serializer
{
    public sealed class ExcelDirector
    {
        private readonly IDataSorce _dataSorce;
              
        private readonly ModelBuilder _modelBuilder;
             
        private readonly ExcelSerializer _excelSerializer;
             
        private readonly FileManager _fileManager;

        public ExcelDirector(IDataSorce dataSorce, ModelBuilder modelBuilder, ExcelSerializer excelSerializer, FileManager fileManager)
        {
            _dataSorce = dataSorce;
            _modelBuilder = modelBuilder;
            _excelSerializer = excelSerializer;
            _fileManager = fileManager;
        }

        public void CreateAndFillExcelFile(string fileName = "File", string filePath = @"C:\Navicon\JsonSerializer")
        {
            List<Contact> contacts = (List<Contact>)_dataSorce.GetContacts(10);

            List<ExcelContact> excelContacts = _modelBuilder.PrepeareContactForExcel(contacts);

            byte[] excelFile = _excelSerializer.GetContactsAsExcel(excelContacts);

            _fileManager.WriteInFile(excelFile, fileName, filePath, "xlsx");
        }

        public void CreateAndFillCSVFile(string fileName = "File", string filePath = @"C:\Navicon\JsonSerializer")
        {
            List<Contact> contacts = (List<Contact>)_dataSorce.GetContacts(10);

            List<ExcelContact> excelContacts = _modelBuilder.PrepeareContactForExcel(contacts);

            var text = new CSVSerializer().GetContactsAsCsv(excelContacts);

            _fileManager.WriteInFile(text, fileName, filePath);
        }
    }
}
