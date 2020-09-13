using System;
using System.ComponentModel.DataAnnotations;

namespace Navicon.JsonSerializer.Metadata.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaximumLengthAttribute : Attribute, IValidation
    {
        public int MaxLength
        {
            get; set;
        }

        public MaximumLengthAttribute(int length)
        {
            MaxLength = length;
        }

        public bool IsValid(object value, out string errorMessage)
        {
            if (value != null)
            {
                if (value.ToString().Length < MaxLength)
                {
                    errorMessage = string.Empty;
                    return true;
                }
                else
                {
                    errorMessage = $"Максимальная длина строки не должна привышать {MaxLength}";
                    return false;
                }
            }
            errorMessage = "Значение отсутствует";
            return false;
        }
    }
}
