using Navicon.Serializer.DAL.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using System.IO;
using System.Text;
using Navicon.Serializer.Models;
using System.Threading.Tasks;

namespace Navicon.Serializer.Serializing
{
    public class CSVSerializer : ISerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public async Task<byte[]> GetContactsPackaged(IEnumerable<ExcelContact> contacts)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            AddColumnTitles(streamWriter, contacts.First());

            PropertyInfo[] propertyesInfo = contacts.First().GetType().GetProperties();
          
            for (int row= 0; row < contacts.Count(); row++)
            {
                for (int column= 0; column < propertyesInfo.Length; column++)
                {
                    streamWriter.Write(propertyesInfo[column].GetValue(contacts.ElementAt(row)));

                    if (column + 1 != propertyesInfo.Length)
                        streamWriter.Write(";");
                }

                streamWriter.Write('\n');
            }

            streamWriter.Flush();

            return memoryStream.GetBuffer();
        }

        private StreamWriter AddColumnTitles(StreamWriter streamWriter, ExcelContact contact)
        {
            PropertyInfo[] propertyesInfo = contact.GetType().GetProperties();

            for (int i = 0; i < propertyesInfo.Length; i++)
            {
                streamWriter.Write(propertyesInfo[i].Name);

                if (i + 1 != propertyesInfo.Length)
                    streamWriter.Write(";");
            }

            streamWriter.Write('\n');

            return streamWriter;
        }
        public string GetFileFormate()
        {
            return "txt";
        }
    }
}
