using Navicon.JsonSerializer.Metadata.Attributes;
using Navicon.JsonSerializer.Models.Enums;
using System;

namespace Navicon.JsonSerializer.Models
{
    [Description("Описание человека")]
    public class Contact : ICloneable
    {
        [MaximumLength(10)]
        public string FirstName
        {
            get; set;
        }

        [MaximumLength(10)]
        public string SecondName
        {
            get; set;
        }

        [MaximumLength(10)]
        public string LastName
        {
            get; set;
        }

        public Gender Gender
        {
            get; set;
        }

        [MinimumAge(2000, 10, 10)]
        public DateTime DateOfBirth
        {
            get; set;
        }

        public string ITN
        {
            get; set;
        } 
        // Individual Taxpayer Number

        [PhoneRegex(@"\+? ?3?[ -]?8?[ -]?\(?(\d[ -]?){3}\)?[ -]?(\d[ -]?){7}")]
        public string PhoneNumber
        {
            get; set;
        }

        public readonly int Age;

        public Contact()
        {

        }

        public Contact(string firstName, string secondName, string lastName, 
                       Gender gender, DateTime birth, 
                       string itn = "", string phoneNumber = "") : base()
        {
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = birth;
            ITN = itn;
            PhoneNumber = phoneNumber;

            Age = DateTime.Today.Year - DateOfBirth.Year;
            Age--;
        }

        #region interfaces

        public object Clone()
        {
            return (Contact)this.MemberwiseClone();
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
                throw new InvalidCastException();
            }

            return contact.FirstName.Equals(FirstName) &&
                   contact.SecondName.Equals(SecondName) &&
                   contact.LastName.Equals(LastName) &&
                   contact.Gender.Equals(Gender) &&
                   contact.DateOfBirth.Equals(DateOfBirth) &&
                   contact.ITN.Equals(ITN) &&
                   contact.PhoneNumber.Equals(PhoneNumber);
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() ^ SecondName.GetHashCode() ^ LastName.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FirstName} {SecondName} {LastName}, Номер телефона: {PhoneNumber}, Пол: {Gender}, Дата рождения: {DateOfBirth} ({Age} год(а)), ИНН: {ITN}";
        }
    }
}