using Navicon.Serializer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.Serializer.DAL
{
    public interface IDataSorce
    {
        List<Contact> GetContacts(int count);
    }
}
