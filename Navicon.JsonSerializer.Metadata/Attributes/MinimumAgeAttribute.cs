using System;
using System.ComponentModel.DataAnnotations;

namespace Navicon.JsonSerializer.Metadata.Attributes
{
  //  [AttributeUsage(AttributeTargets.Field)]
    public class MinimumAgeAttribute : ValidationAttribute
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

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (MinDateofBirth.CompareTo((DateTime)value) >= 0)
                    return true;
                else
                    this.ErrorMessage = "Тебе бы подрасти, малой :c";
            }

            return false;
        }
    }
}