using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.JsonSerializer.Metadata.Attributes
{
    public interface IValidation
    {
        bool IsValid(object value, out string errorMessage);
    }
}
