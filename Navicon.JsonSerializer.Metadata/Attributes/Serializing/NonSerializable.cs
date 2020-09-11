using System;
using System.Runtime.Serialization;
using Navicon.JsonSerializer.Metadata.Attributes.Serializing;
using ISerializable = Navicon.JsonSerializer.Metadata.Attributes.Serializing.ISerializable;

namespace Navicon.JsonSerializer.Metadata.Attributes
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
