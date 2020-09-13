using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Navicon.JsonSerializer.Metadata.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneRegexAttribute : Attribute, IValidation
    {
        public string Regex
        {
            get; set;
        }

        public PhoneRegexAttribute(string expression)
        {
            Regex = expression;
        }

        public bool IsValid(object value, out string errorMessage)
        {
            if (value != null)
            {
                Regex regex = new Regex(Regex);
                MatchCollection matches = regex.Matches(value.ToString());

                if (matches.Count > 0)
                {
                    errorMessage = string.Empty;
                    return true;
                }
                else
                {
                    errorMessage = "Номер телефона не соответствует установленному формату";
                    return false;
                }
            }
            errorMessage = "Значение отсутствует";
            return false;
        }
    }
}
