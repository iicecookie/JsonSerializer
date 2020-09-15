using Navicon.Serializer.Metadata.Attributes;
using Navicon.Serializer.Models.Enums;
using System;
using System.Globalization;
using SerializableAttribute = Navicon.Serializer.Metadata.Attributes.SerializableAttribute;

namespace Navicon.Serializer.Models
{
    [Description("Описание человека")]
    public sealed class Contact : ICloneable
    {
        [Serializable]
        [MaximumLength(10)]
        public string FirstName
        {
            get; set;
        }

        [Serializable]
        [MaximumLength(10)]
        public string SecondName
        {
            get; set;
        }

        [Serializable]
        [MaximumLength(10)]
        public string LastName
        {
            get; set;
        }

        [Serializable]
        public Gender Gender
        {
            get; set;
        }

        [Serializable]
        [MinimumAge(2000, 10, 10)]
        public DateTime DateOfBirth
        {
            get; set;
        }

        /// <summary>
        /// Individual Taxpayer Number
        /// </summary>
        [Serializable]
        public string ITN
        {
            get; set;
        }

        [Serializable]
        public Address Address
        {
            get; set;
        }

        [Serializable]
        [PhoneRegex(@"\+? ?3?[ -]?8?[ -]?\(?(\d[ -]?){3}\)?[ -]?(\d[ -]?){7}")]
        public string PhoneNumber
        {
            get; set;
        }

        /// <summary>
        /// Возраст контакта
        /// </summary>
        [NonSerializable]
        public int Age
        {
            get
            {
                int Years = new DateTime(DateTime.Now.Subtract(DateOfBirth).Ticks).Year - 1;

                return Years;
            }
        }

        public Contact()
        {

        }

        public Contact(string firstName, string secondName, string lastName, 
                       Gender gender, DateTime birth, Address address,
                       string itn = "", string phoneNumber = "") : base()
        {
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = birth;
            ITN = itn;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        #region interfaces

        public object Clone()
        {
            Contact contact = (Contact)this.MemberwiseClone();
            contact.Address = (Address)this.Address.Clone();
            return contact;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            Contact contact = obj as Contact;

            if (contact == null)
            {
                return false;
                // throw new InvalidCastException();
            }

            return contact.FirstName .Equals(FirstName) &&
                   contact.SecondName.Equals(SecondName) &&
                   contact.LastName  .Equals(LastName) &&
                   contact.ITN   .Equals(ITN) &&
                   contact.Gender.Equals(Gender) &&
                   contact.DateOfBirth.Equals(DateOfBirth) &&
                   contact.PhoneNumber.Equals(PhoneNumber);
        }

        public bool Equals(Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException();
                // or return false;
            }

            return contact.FirstName .Equals(FirstName) &&
                   contact.SecondName.Equals(SecondName) &&
                   contact.LastName  .Equals(LastName) &&
                   contact.ITN   .Equals(ITN) &&
                   contact.Gender.Equals(Gender) &&
                   contact.DateOfBirth.Equals(DateOfBirth) &&
                   contact.PhoneNumber.Equals(PhoneNumber);
        }

        public override int GetHashCode()
        {
            int hash = (int)2111136261;

            hash = (hash * 16777619) ^ FirstName.GetHashCode();
            hash = (hash * 16777619) ^ SecondName.GetHashCode();
            hash = (hash * 16777619) ^ LastName.GetHashCode();
            hash = (hash * 16777619) ^ DateOfBirth.GetHashCode();
            if (Address != null)
            {
                hash = (hash * 16777619) ^ Address.GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            return $"{FirstName} {SecondName} {LastName}, " +
                   $"Номер телефона: {PhoneNumber}, " +
                   $"Пол: {Gender}, " +
                   $"Дата рождения: {DateOfBirth} ({Age} год(а)), " +
                   $"Место жительства: {Address}, " +
                   $"ИНН: {ITN}";
        }
    }
}