using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class PersonChangedEvent : DomainEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdCard { get; set; }
        public string IdNumber { get; set; }
        public State State { get; set; }
    }
}
