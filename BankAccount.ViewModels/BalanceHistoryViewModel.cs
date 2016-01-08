namespace BankAccount.ViewModels
{
    public class BalanceHistoryViewModel
    {
        public decimal Amount { get; }
        public string Text { get; private set; }

        public BalanceHistoryViewModel(decimal amount)
        {
            this.Amount = amount;
            this.Text = amount > 0 ? "Deposit" : "Withdraw";
        }
    }
}