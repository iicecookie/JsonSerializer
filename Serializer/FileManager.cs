using System;
using System.IO;

namespace Navicon.JsonSerializer.Serializer
{
    public class FileManager
    {
        private void WriteInFile(string serializedText)
        {
            /*
            // создаем каталог для файла
            string startupPath = Environment.CurrentDirectory;

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();

            // запись в файл
            using (FileStream fstream = new FileStream($"{path}\note.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
            }
            */
        }

        private void ReadFromFile(string serializedText)
        {

        }
    }
}
