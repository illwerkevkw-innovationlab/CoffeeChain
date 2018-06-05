using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeChain.Connector.FunctionOutputs;
using Nethereum.Contracts;

namespace CoffeeChain.App.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private IList<EventLog<CoffeeBoughtEvent>> _transactions = new List<EventLog<CoffeeBoughtEvent>>();
        public IList<EventLog<CoffeeBoughtEvent>> Transactions
        {
            get { return _transactions; }
            set { SetProperty(ref _transactions, value); }
        }

        public TransactionsViewModel()
        {
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
