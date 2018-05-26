namespace CoffeeChain.App.ViewModels
{
    public class BalanceViewModel : BaseViewModel
    {
        public string Wallet { get; set; }
        public long CoffeeTokens { get; set; }

        public BalanceViewModel(TestService testService)
        {
            Title = "Balance";

            Wallet = testService.GetWallet();
            CoffeeTokens = testService.GetTokens();
        }
    }
}