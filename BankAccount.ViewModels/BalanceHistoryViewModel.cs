namespace BankAccount.ViewModels
{
    public class BalanceHistoryViewModel
    {
        public int Amount { get; }
        public string Text { get; private set; }

        public BalanceHistoryViewModel(int amount)
        {
            this.Amount = amount;
            this.Text = amount > 0 ? "Deposit" : "Withdraw";
        }
    }
}