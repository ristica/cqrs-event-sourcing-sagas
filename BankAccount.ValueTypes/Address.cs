namespace BankAccount.ValueTypes
{
    public class Address
    {
        #region Fields

        private readonly string _key = "this is the key";

        #endregion

        #region Properties

        public string Street { get; }
        public string Zip { get; }
        public string Hausnumber { get; }
        public string City { get; }
        public string State { get; }

        #endregion

        #region C-Tor

        public Address(string street, string zip, string hausnumber, string city, string state)
        {
            this.Street = street;
            this.Zip = zip;
            this.Hausnumber = hausnumber;
            this.City = city;
            this.State = state;
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

            var other = (Address) obj;
            return this.Street == other.Street && 
                    this.Zip == other.Zip && 
                    this.Hausnumber == other.Hausnumber && 
                    this.City == other.City &&
                    this.State == other.State;
        }

        public override int GetHashCode()
        {
            return _key.GetHashCode();
        }

        #endregion
    }
}
