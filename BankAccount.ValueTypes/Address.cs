namespace BankAccount.ValueTypes
{
    public class Address
    {
        private readonly string _key = "this is the key";

        public string Street { get; }
        public string Zip { get; }
        public string Hausnumber { get; }
        public string City { get; }
        public string State { get; }

        public Address(string street, string zip, string hausnumber, string city, string state)
        {
            this.Street = street;
            this.Zip = zip;
            this.Hausnumber = hausnumber;
            this.City = city;
            this.State = state;
        }

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
            return Street == other.Street && Zip == other.Zip && Hausnumber == other.Hausnumber && City == other.City &&
                   State == other.State;
        }

        public override int GetHashCode()
        {
            return _key.GetHashCode();
        }
    }
}
