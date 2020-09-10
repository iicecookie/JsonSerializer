using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Navicon.JsonSerializer.Metadata.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneRegexAttribute : ValidationAttribute
    {
        public string Regex { get; set; }

        public PhoneRegexAttribute(string expression)
        {
            Regex = expression;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                Regex regex = new Regex(Regex);
                MatchCollection matches = regex.Matches(value.ToString());

                if (matches.Count > 0)
                {
                    return true;                    
                }
                else
                    this.ErrorMessage = "Номер телефона не соответствует установленному формату";
            }
            return false;
        }
    }
}
