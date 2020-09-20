using Navicon.Serializer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.Serializer.DAL.Interfaces
{
    public interface IDataSorce//<T>
    {
      //  IEnumerable<T> GetAll();

        IEnumerable<Contact>//<T> 
            GetContacts(int count);
    }
}
