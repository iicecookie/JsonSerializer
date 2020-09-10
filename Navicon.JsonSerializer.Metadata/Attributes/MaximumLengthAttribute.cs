using System;
using System.ComponentModel.DataAnnotations;

namespace Navicon.JsonSerializer.Metadata.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaximumLengthAttribute : ValidationAttribute
    {
        public int MaxLength { get; set; }

        public MaximumLengthAttribute()
        { }

        public MaximumLengthAttribute(int length)
        {
            MaxLength = length;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.ToString().Length < MaxLength)
                    return true;
                else
                    this.ErrorMessage = $"Максимальная длина строки не должна привышать {MaxLength}";
            }

            return false;
        }
    }
}
