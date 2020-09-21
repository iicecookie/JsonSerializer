using Navicon.Serializer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.Serializer.DAL.Interfaces
{
    public interface IDataSorce
    {

        IEnumerable<Contact> GetContacts(int count);
    }
}
