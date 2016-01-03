using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class PersonChangedEvent : Event
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdCard { get; set; }
        public string IdNumber { get; set; }
    }
}
