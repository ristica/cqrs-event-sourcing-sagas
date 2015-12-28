namespace BankAccount.ApplicationLayer.Models
{
    public class BalanceHistoryViewModel
    {
        public int Amount { get; }
        public string Text { get; private set; }

        public BalanceHistoryViewModel(int amount)
        {
            if (amount > 0)
            {
                this.Amount = amount;
            }
            else
            {
                this.Amount = amount*-1;
            }

            this.Text = amount > 0 ? "Deposit" : "Withdraw";
        }
    }
}