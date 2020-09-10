using Navicon.JsonSerializer.Metadata.Attributes;
using Navicon.JsonSerializer.Models.Enums;
using System;

namespace Navicon.JsonSerializer.Models
{
    [Description("Место жительство человека")]
    public class Address : ICloneable
    {
        public string Country { get; set; }
        public string Sity    { get; set; }
        public string AddressFull      { get; set; }
        public AddressType AddressType { get; set; }

        public object Clone()
        {
            return (Address)this.MemberwiseClone();
        }
    }
}
