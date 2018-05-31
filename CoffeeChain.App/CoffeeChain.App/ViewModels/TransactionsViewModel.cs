using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeChain.Connector;
using CoffeeChain.Connector.FunctionOutputs;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace CoffeeChain.App.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private readonly Web3 _web3;
        private readonly ICoffeeEconomyService _coffeeEconomyService;

        private IList<EventLog<CoffeeBoughtEvent>> _transactions = new List<EventLog<CoffeeBoughtEvent>>();
        public IList<EventLog<CoffeeBoughtEvent>> Transactions
        {
            get { return _transactions; }
            set { SetProperty(ref _transactions, value); }
        }

        public TransactionsViewModel(Web3 web3, ICoffeeEconomyService coffeeEconomyService)
        {
            _web3 = web3;
            _coffeeEconomyService = coffeeEconomyService;

            Title = "Transaktionen";
            IsBusy = true;
        }

        public async Task OnAppearing()
        {
            Transactions = await _coffeeEconomyService.GetCoffeeBoughtEventsForWallet(_web3.TransactionManager.Account.Address);

            IsBusy = false;
        }
    }
}
