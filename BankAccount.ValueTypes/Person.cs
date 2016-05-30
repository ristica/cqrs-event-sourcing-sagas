using System;

namespace BankAccount.ValueTypes
{
    public class Person
    {
        #region Fields

        private readonly string _key = "this is the key";

        #endregion

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string IdCard { get; set; }
        public string IdNumber { get; set; }

        #endregion

        #region C-Tor

        public Person(string firstname, string lastname, DateTime dob, string idCard, string idNumber)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Dob = dob;
            this.IdCard = idCard;
            this.IdNumber = idNumber;
        }

        #endregion

        #region Identity Management

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Person)obj;
            return this.FirstName == other.FirstName &&
                   this.LastName == other.LastName &&
                   this.Dob == other.Dob &&
                   this.IdCard == other.IdCard &&
                   this.IdNumber == other.IdNumber;
        }

        public override int GetHashCode()
        {
            return _key.GetHashCode();
        }

        #endregion
    }
}
