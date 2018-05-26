namespace CoffeeChain.App.ViewModels
{
    public class BalanceViewModel : BaseViewModel
    {
        public string Wallet { get; set; }
        public long CoffeeTokens { get; set; }

        public BalanceViewModel()
        {
            Title = "Balance";
        }
    }
}