using System;
using System.ComponentModel.DataAnnotations;
using BankAccount.ValueTypes;

namespace BankAccount.DbModel.Entities
{
    public class CustomerEntity
    {
        [Key]
        public Guid AggregateId { get; set; }

        public int Version { get; set; }
        public State CustomerState { get; set; }
    }
}
