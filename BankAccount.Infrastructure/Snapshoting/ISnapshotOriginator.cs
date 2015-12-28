namespace BankAccount.Infrastructure.Snapshoting
{
    public interface ISnapshotOriginator 
    {
        Snapshot GetCurrentSnapshot(int? lastEventVersion);
        void LoadSnapshot(Snapshot snapshot);
        bool ShouldTakeSnapshot();
    }
}