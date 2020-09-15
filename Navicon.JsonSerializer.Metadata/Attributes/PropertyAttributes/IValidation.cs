using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.Serializer.Metadata.Attributes
{
    public interface IValidation
    {
        bool IsValid(object value, out string errorMessage);
    }
}
