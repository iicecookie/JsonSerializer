using System;
using System.Configuration;
using System.IO;
using System.Text;
using Navicon.Serializer.DAL.Interfaces;

namespace Navicon.Serializer.DAL
{
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Записывает текст в файл
        /// </summary>
        /// <param name="serializedText">Текст для записи</param>
        /// <param name="fileName">Имя файла</param>
        /// <param name="path">Путь к файлу</param>
        public void WriteInFile(string Text, string fileName, string path = @"C:\Navicon\JsonSerializer", string fileFormat = "txt")
        {
            WriteInFile(Encoding.Unicode.GetBytes(Text), fileName, path, fileFormat);
        }

        /// <summary>
        /// Записывает последовательность байт в файл
        /// </summary>
        /// <param name="Text">Текст в байтовом представлении unicode</param>
        /// <param name="fileName">Имя файла</param>
        /// <param name="path">Путь к файлу</param>
        public void WriteInFile(byte[] Text, string fileName, string path = @"C:\Navicon\JsonSerializer", string fileFormat = "txt")
        {
            if (IsValidPath(path) == false)
            {
                Logger.Logger.Log.Error($"Путь {path} некорректен");
                throw new ArgumentException("Путь к файлу не корректен");
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists == false)
            {
                dirInfo.Create();
            }

            string filePath = $@"{path}\{fileName}.{fileFormat}";

            if (File.Exists(filePath) == true)
            {
                bool isOverridable;
                bool.TryParse(ConfigurationManager.AppSettings.Get("OverrideFile"), out isOverridable);

                if (isOverridable == false)
                {
                    Logger.Logger.Log.Error($"Файл {fileName}.{fileFormat} уже существует в {path}");
                    throw new IOException("file already exist, check config file");
                }
            }

            using (FileStream fstream = new FileStream(filePath, FileMode.Create))
            {
                fstream.Write(Text, 0, Text.Length);
            }

            Logger.Logger.Log.Info($"Файл {fileName}.{fileFormat} записан в {path}");
        }

        /// <summary>
        /// Считывает текст из указаного текстового файла
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <param name="path">путь к файлу</param>
        /// <returns></returns>
        public string ReadFromFile(string fileName, string path = @"C:\Navicon\JsonSerializer")
        {
            if (IsValidPath(path) == false)
            {
                throw new ArgumentException("Путь к файлу не корректен");
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            if (dirInfo.Exists == false)
            {
                throw new FileNotFoundException();
            }

            using (FileStream fstream = File.OpenRead($"{path}\\{fileName}.txt"))
            {
                byte[] array = new byte[fstream.Length];

                fstream.Read(array, 0, array.Length);

                return System.Text.Encoding.Default.GetString(array);
            }
        }

        /// <summary>
        /// Проверяет путь к файлу на корректность использования
        /// </summary>
        /// <param name="path">Путь к файлу в файловой системе Windows</param>
        /// <returns></returns>
        private bool IsValidPath(string path)
        {
            bool isValid = true;

            try
            {
                string fullPath = Path.GetFullPath(path);

                string root = Path.GetPathRoot(path);

                isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}