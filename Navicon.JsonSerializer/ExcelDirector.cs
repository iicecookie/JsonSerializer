using Navicon.Serializer.DAL;
using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Models;
using Navicon.Serializer.Serializing.EXCEL;
using Navicon.Serializer.DAL.Interfaces;
using System.Collections.Generic;
using OfficeOpenXml;

namespace Navicon.Serializer
{
    public sealed class ExcelDirector
    {
        private IDataSorce _dataSorce;

        private ModelBuilder _modelBuilder;

        private ExcelSerializer _excelSerializer;

        private FileManager _fileManager;

        public ExcelDirector(IDataSorce dataSorce, ModelBuilder modelBuilder, ExcelSerializer excelSerializer, FileManager fileManager)
        {
            _dataSorce = dataSorce;
            _modelBuilder = modelBuilder;
            _excelSerializer = excelSerializer;
            _fileManager = fileManager;
        }

        public void CreateAndFillExcelFile(string fileName = "File", string filePath = "")
        {
            List<Contact> contacts = _dataSorce.GetContacts(10);

            List<ExcelContact> excelContacts = _modelBuilder.PrepeareContactForExcel(contacts);

            ExcelPackage excelFile = _excelSerializer.CreateExcelFile();
            _excelSerializer.AddExcelContactToExcel(excelContacts, excelFile);

            _fileManager.SaveExcelFile(excelFile, fileName, filePath);

        }
    }
}
