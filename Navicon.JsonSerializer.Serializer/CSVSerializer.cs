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
        public async Task<byte[]> GetContactsPackaged(IEnumerable<ExportContact> contacts)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            AddColumnTitles(streamWriter, contacts.First());

            PropertyInfo[] propertiesInfo = contacts.First().GetType().GetProperties();

            for (int row = 0; row < contacts.Count(); row++)
            {
                var contact = contacts.ElementAt(row);

                for (int column = 0; column < propertiesInfo.Length; column++)
                {
                    streamWriter.Write(propertiesInfo[column].GetValue(contact));

                    if (column + 1 != propertiesInfo.Length)
                        streamWriter.Write(";");
                }

                streamWriter.Write('\n');
            }

            streamWriter.Flush();

            return memoryStream.GetBuffer();
        }

        private StreamWriter AddColumnTitles(StreamWriter streamWriter, ExportContact contact)
        {
            PropertyInfo[] propertiesInfo = contact.GetType().GetProperties();

            for (int i = 0; i < propertiesInfo.Length; i++)
            {
                streamWriter.Write(propertiesInfo[i].Name);
            
                if (i + 1 != propertiesInfo.Length)
                    streamWriter.Write(";");
            }

            streamWriter.Write('\n');

            return streamWriter;
        }
        public string GetFileFormat()
        {
            return "txt";
        }
    }
}
