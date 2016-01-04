using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class AddressChangedEvent : DomainEvent
    {
        public string Street { get; set; }
        public string Hausnumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
