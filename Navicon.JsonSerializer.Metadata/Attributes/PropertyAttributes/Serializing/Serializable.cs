using Navicon.Serializer.Metadata.Attributes.Serializing;
using System;

namespace Navicon.Serializer.Metadata.Attributes
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
