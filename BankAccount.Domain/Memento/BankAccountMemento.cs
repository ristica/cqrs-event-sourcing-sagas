using System;
using BankAccount.Infrastructure.Domain;
using BankAccount.ValueTypes;

namespace BankAccount.Domain.Memento
{
    public class BankAccountMemento : BaseMemento
    {
        public Customer Customer { get; private set; }
        public Contact Contact { get; private set; }
        public Money Money { get; private set; }
        public Address Address { get; private set; }

        public BankAccountMemento(
            Guid id, 
            int version, 
            Customer customer,
            Contact contact,
            Money money,
            Address address)
        {
            this.Customer = customer;
            this.Contact = contact;
            this.Money = money;
            this.Address = address;
        }
    }
}
