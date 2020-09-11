using Navicon.JsonSerializer.Metadata.Attributes;
using Navicon.JsonSerializer.Models.Enums;
using System;
using SerializableAttribute = Navicon.JsonSerializer.Metadata.Attributes.SerializableAttribute;

namespace Navicon.JsonSerializer.Models
{
    [Description("Место жительство человека")]
    public sealed class Address : ICloneable
    {
        [Serializable]
        public string Country
        {
            get; set;
        }

        [Serializable]
        public string Sity
        {   
            get; set;
        }

        [Serializable]
        public string LifeLocation
        {
            get; set;
        }

        [Serializable]
        public AddressType AddressType
        {
            get; set;
        }

        public Address() { }

        public Address(string country, string sity, string lifeLocation, AddressType addressType) : base()
        {
            Country = country;
            Sity = sity;
            LifeLocation = lifeLocation;
            AddressType = addressType;
        }

        #region interfaces

        public object Clone()
        {
            return (Address)this.MemberwiseClone();
        }

        public override int GetHashCode()
        {
            int hash = (int)2111136261;

            hash = (hash * 16777619) ^ Country.GetHashCode();
            hash = (hash * 16777619) ^ Sity.GetHashCode();
            hash = (hash * 16777619) ^ LifeLocation.GetHashCode();

            return hash;
        }

        #endregion

        public override string ToString()
        {
            return $"Страна: {Country}, город {Sity}, {AddressType} адрес проживания: {LifeLocation}";
        }
    }
}