using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeChain.App.Models;
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
            Transactions = await _coffeeEconomyService.GetCoffeeBoughtEventsForWallet(Settings.Current.PublicWalletAddress);

            Console.WriteLine($"Found {Transactions.Count} old transactions to display for wallet {Settings.Current.PublicWalletAddress}.");

            IsBusy = false;
        }
    }
}
