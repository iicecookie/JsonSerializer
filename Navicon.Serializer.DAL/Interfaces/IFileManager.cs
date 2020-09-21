using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.Serializer.DAL.Interfaces
{
    public interface IFileManager
    {
        void WriteInFile(byte[] Text, string fileName, string path = @"C:\Navicon\JsonSerializer", string fileFormat = "txt");

        string ReadFromFile(string fileName, string path = @"C:\Navicon\JsonSerializer");
    }
}
