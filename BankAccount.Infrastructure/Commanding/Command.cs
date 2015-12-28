using System;

namespace BankAccount.Infrastructure.Commanding
{
    [Serializable]
    public class Command : ICommand
    {
        public Guid Id { get; }
        public int Version { get; private set; }

        protected Command(Guid id, int version)
        {
            Id = id;
            Version = version;
        }
    }
}
