using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.DAL.Interfaces;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Serializing;
using Navicon.Serializer.Models;
using System.Collections.Generic;
using System;

namespace Navicon.Serializer
{
    public sealed class Director
    {
        private readonly IDataSorce _dataSorce;
              
        private readonly ModelBuilder _modelBuilder;
             
        private readonly ISerializer _serializer;
             
        private readonly IFileManager _fileManager;

        public Director(IDataSorce dataSorce, ModelBuilder modelBuilder, ISerializer serializer, IFileManager fileManager)
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

            IEnumerable<Contact> contacts = _dataSorce.GetContacts(10);

            Logger.Logger.Log.Info("Директор запрашивает подготовку списка к формату экспорта");

            IEnumerable<ExcelContact> excelContacts = _modelBuilder.PrepeareContactsForExport(contacts);

            Logger.Logger.Log.Info("Директор запрашивает преобразование данных в конечный файл");

            byte[] excelFile = await _serializer.GetContactsPackaged(excelContacts);

            Logger.Logger.Log.Info("Директор запрашивает сохранение данных в файл");

            _fileManager.WriteInFile(excelFile, fileName, filePath, _serializer.GetFileFormate());
        }
    }
}