using System;
using System.Runtime.Serialization;
using Navicon.Serializer.Metadata.Attributes.Serializing;
using ISerializable = Navicon.Serializer.Metadata.Attributes.Serializing.ISerializable;

namespace Navicon.Serializer.Metadata.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class NonSerializableAttribute : Attribute, ISerializable
    {
        public bool isSerializable { get; }

        public NonSerializableAttribute()
        {
            isSerializable = false;
        }
    }
}
