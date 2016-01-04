using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class ContactChangedEvent : DomainEvent
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
