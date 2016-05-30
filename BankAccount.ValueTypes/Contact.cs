using System.Runtime.ConstrainedExecution;

namespace BankAccount.ValueTypes
{
    public class Contact
    {
        #region  Fields

        private readonly string _key = "this is the key";

        #endregion

        #region Properties

        public string Email { get; }
        public string PhoneNumber { get; }

        #endregion

        #region C-Tor

        public Contact(string email, string phoneNumber)
        {
            this.Email = email;
            this.PhoneNumber = phoneNumber;
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

            var other = (Contact)obj;
            return this.Email == other.Email &&
                   this.PhoneNumber == other.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return _key.GetHashCode();
        }

        #endregion

    }
}
