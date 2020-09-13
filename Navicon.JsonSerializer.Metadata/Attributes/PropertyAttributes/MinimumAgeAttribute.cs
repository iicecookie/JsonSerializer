using System;
using System.ComponentModel.DataAnnotations;

namespace Navicon.JsonSerializer.Metadata.Attributes
{
  //  [AttributeUsage(AttributeTargets.Field)]
    public class MinimumAgeAttribute : Attribute, IValidation
    {
        public DateTime MinDateofBirth
        {
            get; set;
        }

        public MinimumAgeAttribute(int year, int month, int day)
        {
            MinDateofBirth = new DateTime(year, month, day);
        }

        public MinimumAgeAttribute(string date)
        {
            MinDateofBirth = DateTime.Parse(date);
        }

        public bool IsValid(object value, out string errorMessage)
        {
            if (value != null)
            {
                if (MinDateofBirth.CompareTo((DateTime)value) >= 0)
                {
                    errorMessage = string.Empty;
                    return true;
                }
                else
                {
                    errorMessage = "Тебе бы подрасти, малой :c";
                    return false;
                }
            }
            errorMessage = "Значение отсутствует";
            return false;
        }
    }
}