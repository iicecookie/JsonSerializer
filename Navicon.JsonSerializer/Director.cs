using Navicon.Serializer.DAL;
using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Models;
using Navicon.Serializer.DAL.Interfaces;
using System.Collections.Generic;
using Navicon.Serializer.Serializing;
using System.Threading.Tasks;
using System;

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
            if (dataSorce    == null)
            {
                Logger.Logger.Log.Error($"{nameof(dataSorce)} is null");
                throw new ArgumentException($"{nameof(dataSorce)} is null");
            }
            if (modelBuilder == null)
            {
                Logger.Logger.Log.Error($"{nameof(modelBuilder)} is null");
                throw new ArgumentException($"{nameof(modelBuilder)} is null");
            }
            if (serializer   == null)
            {
                Logger.Logger.Log.Error($"{nameof(serializer)} is null");
                throw new ArgumentException($"{nameof(serializer)} is null");
            }
            if (fileManager  == null)
            {
                Logger.Logger.Log.Error($"{nameof(fileManager)} is null");
                throw new ArgumentException($"{nameof(fileManager)} is null");
            }

            _dataSorce    = dataSorce;
            _modelBuilder = modelBuilder;
            _serializer   = serializer;
            _fileManager  = fileManager;
        }

        public async void CreateAndFillExcelFile(string fileName = "File", string filePath = @"C:\Navicon\JsonSerializer")
        {
            Logger.Logger.Log.Info("Директор запрашивает список контактов");
            List<Contact> contacts = (List<Contact>)_dataSorce.GetContacts(10);

            Logger.Logger.Log.Info("Директор запрашивает подготовку списка к формату экспорта");
            List<ExcelContact> excelContacts = _modelBuilder.PrepeareContactForExcel(contacts);

            Logger.Logger.Log.Info("Директор запрашивает преобразование данных в конечный файл");
            byte[] excelFile = await _serializer.GetContactsPackaged(excelContacts);

            Logger.Logger.Log.Info("Директор запрашивает сохранение данных в файл");
            _fileManager.WriteInFile(excelFile, fileName, filePath, _serializer.GetFileFormate());
        }
    }
}
