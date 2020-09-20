using Navicon.Serializer.DAL;
using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Models;
using Navicon.Serializer.DAL.Interfaces;
using System.Collections.Generic;
using Navicon.Serializer.Serializing;
using System.Threading.Tasks;

namespace Navicon.Serializer
{
    public sealed class Director
    {
        private readonly IDataSorce _dataSorce;
              
        private readonly ModelBuilder _modelBuilder;
             
        private readonly ISerializer _serializer;
             
        private readonly FileManager _fileManager;

        public Director(IDataSorce dataSorce, ModelBuilder modelBuilder, ISerializer serializer, FileManager fileManager)
        {
            _dataSorce = dataSorce;
            _modelBuilder = modelBuilder;
            _serializer = serializer;
            _fileManager = fileManager;
        }

        public async void CreateAndFillExcelFile(string fileName = "File", string filePath = @"C:\Navicon\JsonSerializer")
        {
            List<Contact> contacts = (List<Contact>)_dataSorce.GetContacts(10);

            List<ExcelContact> excelContacts = _modelBuilder.PrepeareContactForExcel(contacts);

            byte[] excelFile = await _serializer.GetContactsPackaged(excelContacts);

            _fileManager.WriteInFile(excelFile, fileName, filePath, _serializer.GetFileFormate());
        }
    }
}
