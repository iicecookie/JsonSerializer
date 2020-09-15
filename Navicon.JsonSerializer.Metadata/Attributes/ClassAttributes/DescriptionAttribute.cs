using System;

namespace Navicon.Serializer.Metadata.Attributes
{

    [AttributeUsage(AttributeTargets.Class)]
    public class DescriptionAttribute : Attribute
    {
        public string Description
        {
            get; set;
        }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
