using Navicon.JsonSerializer.Metadata.Attributes.Serializing;
using System;

namespace Navicon.JsonSerializer.Metadata.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class SerializableAttribute : Attribute, ISerializable
    {
        public bool isSerializable { get; }

        public SerializableAttribute()
        {
            isSerializable = true;
        }
    }
}
