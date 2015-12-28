using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DbModel.Entities
{
    public class EventEntity
    {
        [Key]
        public Int64 EventEntityId { get; set; }

        public Guid AggregateId { get; set; }

        public int Sequence { get; set; }

        public DateTime EventDate { get; set; }

        public string EventName { get; set; }

        public string Body { get; set; }
    }
}
