using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class ContactChangedEvent : Event
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
