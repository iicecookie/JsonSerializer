using Navicon.JsonSerializer.Metadata.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Navicon.JsonSerializer.ModelOperations
{
    public static class ModelValidetor
    {
        public static Dictionary<string, string> ValidateModel(object obj)
        {
            var propertyError = new Dictionary<string, string>();

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                object[] customAttributes = propertyInfo.GetCustomAttributes(true);

                var validatingAttributes = customAttributes.Where(attr => attr.GetType()
                                                                   .GetInterfaces()
                                                                   .Where(i => i.Name == "IValidation")
                                                                   .FirstOrDefault() != null)
                                                            .FirstOrDefault();

                if (validatingAttributes == null)
                {
                    continue;
                }

                string errorMessage = string.Empty;

                var isValide = ((IValidation)validatingAttributes).IsValid(propertyInfo.GetValue(obj), out errorMessage);

                if (isValide == false)
                {
                    Console.WriteLine($"Ошибка в поле {propertyInfo.Name}: {errorMessage}");

                    propertyError.Add(propertyInfo.Name, errorMessage);
                }
            }

            return propertyError;
        }
    }
}
