using Navicon.Serializer.DAL;
using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Models;
using Navicon.Serializer.Serializer;
using Navicon.Serializer.Serializer.EXCEL;
using System.Collections.Generic;
using OfficeOpenXml;

namespace Navicon.Serializer
{
    public sealed class Director
    {
        private IDataSorce _dataSorce;

        private ModelBuilder _modelBuilder;

        private ExcelSerializer _excelSerializer;

        private FileManager _fileManager;

        public Director(IDataSorce dataSorce, ModelBuilder modelBuilder, ExcelSerializer excelSerializer, FileManager fileManager)
        {
            _dataSorce = dataSorce;
            _modelBuilder = modelBuilder;
            _excelSerializer = excelSerializer;
            _fileManager = fileManager;
        }

        public void CreateAndFillExcelFile(string fileName = "", string filePath = "")
        {
            List<Contact> contacts = _dataSorce.GetContacts(10);

            List<ExcelContact> excelContacts = _modelBuilder.PrepeareContactForExcel(contacts);

            ExcelPackage excelFile = _excelSerializer.CreateExcelFile();
            _excelSerializer.AddExcelContactToExcel(excelContacts, excelFile);

            _fileManager.SaveExcelFile(excelFile, fileName, filePath);
        }
    }
}
