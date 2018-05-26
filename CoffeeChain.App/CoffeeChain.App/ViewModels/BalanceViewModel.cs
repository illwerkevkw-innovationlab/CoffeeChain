using System.ComponentModel;
using System.Threading.Tasks;
using CoffeeChain.Connector;
using Nethereum.Web3;

namespace CoffeeChain.App.ViewModels
{
    public class BalanceViewModel : BaseViewModel
    {
        private readonly Web3 _web3;
        private readonly ICoffeeEconomyService _coffeeEconomyService;

        private string _wallet;
        public string Wallet
        {
            get { return _wallet; }
            set { SetProperty(ref _wallet, value); }
        }

        private long _coffeeTokens;
        public long CoffeeTokens
        {
            get { return _coffeeTokens; }
            set { SetProperty(ref _coffeeTokens, value); }
        }

        public BalanceViewModel(Web3 web3, ICoffeeEconomyService coffeeEconomyService)
        {
            _web3 = web3;
            _coffeeEconomyService = coffeeEconomyService;

            Title = "Balance";
            IsBusy = true;
        }

        public async Task OnAppearing()
        {
            Wallet = _web3.TransactionManager.Account.Address;
            CoffeeTokens = await _coffeeEconomyService.GetTokensAsync(Wallet);
            IsBusy = false;
        }
    }
}
