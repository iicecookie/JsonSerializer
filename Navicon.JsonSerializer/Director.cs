using Navicon.Serializer.DAL;
using Navicon.Serializer.DAL.ModelBuilder;
using Navicon.Serializer.DAL.Models;
using Navicon.Serializer.Models;
using Navicon.Serializer.Serializer.EXCEL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.Serializer
{
    public sealed class Director
    {
        private IDataSorce _dataSorce;

        private ModelBuilder _modelBuilder;

        public Director(IDataSorce dataSorce, ModelBuilder modelBuilder)
        {
            _dataSorce = dataSorce;
            _modelBuilder = modelBuilder;
        }

        public void DoEverything()
        {
            List<Contact> contacts = _dataSorce.GetContacts(10);

            List<ExcelContact> excelContacts = _modelBuilder.PrepeareContactForExcel(contacts);

            int a = 5;

            var c = new ExcelSerializer();
            var pack = c.CreateExcelFile();
            c.AddExcelContactToExcel(excelContacts, pack);

        }
    }
}
