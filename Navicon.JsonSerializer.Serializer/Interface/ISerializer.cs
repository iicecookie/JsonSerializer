using Navicon.Serializer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Navicon.Serializer.Serializing
{
    public interface ISerializer
    {
        Task<byte[]> GetContactsPackaged(IEnumerable<ExportContact> Contacts);

        string GetFileFormat();
    }
}
