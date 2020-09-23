using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.DAL.Interfaces;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Serializing;
using Navicon.Serializer.Models;
using System.Collections.Generic;
using System;
using Navicon.Serializer.Logging;
using log4net;

namespace Navicon.Serializer
{
    public sealed class Director
    {
        private readonly IDataSorce _dataSorce;
              
        private readonly ModelBuilder _modelBuilder;
             
        private readonly ISerializer _serializer;
             
        private readonly IFileManager _fileManager;

        private readonly ILog _log;

        public Director(IDataSorce dataSorce, ModelBuilder modelBuilder, ISerializer serializer, IFileManager fileManager, ILog log)
        {
            if (log == null)
            {
                throw new ArgumentException($"{nameof(log)} is null");
            }
            if (dataSorce    == null)
            {
                log.Error($"{nameof(dataSorce)} is null");
                throw new ArgumentException($"{nameof(dataSorce)} is null");
            }
            if (modelBuilder == null)
            {
                log.Error($"{nameof(modelBuilder)} is null");
                throw new ArgumentException($"{nameof(modelBuilder)} is null");
            }
            if (serializer   == null)
            {
                log.Error($"{nameof(serializer)} is null");
                throw new ArgumentException($"{nameof(serializer)} is null");
            }
            if (fileManager  == null)
            {
                log.Error($"{nameof(fileManager)} is null");
                throw new ArgumentException($"{nameof(fileManager)} is null");
            }

            _dataSorce    = dataSorce;
            _modelBuilder = modelBuilder;
            _serializer   = serializer;
            _fileManager  = fileManager;
            _log = log;
        }

        public async void CreateAndFillFile(string fileName = "File", string filePath = @"C:\Navicon\JsonSerializer")
        {
            _log.Info("Директор запрашивает список контактов");

            IEnumerable<Contact> contacts = _dataSorce.GetContacts(10);

            _log.Info("Директор запрашивает подготовку списка к формату экспорта");

            IEnumerable<ExportContact> excelContacts = _modelBuilder.PrepeareContactsForExport(contacts);

            _log.Info("Директор запрашивает преобразование данных в конечный файл");

            byte[] excelFile = await _serializer.GetContactsPackaged(excelContacts);

            _log.Info("Директор запрашивает сохранение данных в файл");

            _fileManager.WriteInFile(excelFile, fileName, filePath, _serializer.GetFileFormat());
        }
    }
}