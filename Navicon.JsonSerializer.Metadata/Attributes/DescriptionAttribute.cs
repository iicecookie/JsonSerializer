using System;

namespace Navicon.JsonSerializer.Metadata.Attributes
{

    [AttributeUsage(AttributeTargets.Class)]
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
