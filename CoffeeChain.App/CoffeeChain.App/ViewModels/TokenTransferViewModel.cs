using System.Windows.Input;
using CoffeeChain.Connector;
using Nethereum.Web3;
using Xamarin.Forms;

namespace CoffeeChain.App.ViewModels
{
    public class TokenTransferViewModel : BaseViewModel
    {
        private readonly Web3 _web3;
        private readonly ICoffeeEconomyService _coffeeEconomyService;

        private string _recipient;
        public string Recipient
        {
            get { return _recipient; }
            set { SetProperty(ref _recipient, value); }
        }

        private int _tokens;
        public int Tokens
        {
            get { return _tokens; }
            set { SetProperty(ref _tokens, value); }
        }

        public ICommand ExecuteTransactionCommand { get; private set; }

        public TokenTransferViewModel(Web3 web3, ICoffeeEconomyService coffeeEconomyService)
        {
            _web3 = web3;
            _coffeeEconomyService = coffeeEconomyService;

            Title = "Kaffee Token übertragen";

            ExecuteTransactionCommand = new Command(ExecuteTransaction);
        }

        private async void ExecuteTransaction()
        {
            System.Console.WriteLine($"Recipient: {Recipient}, Tokens: {Tokens}");
            System.Console.WriteLine($"Sender: {_web3.TransactionManager.Account.Address}");

            if (Recipient.IsNullOrEmpty() || Tokens <= 0)
            {
                System.Console.WriteLine("Invalid input for transaction. Aborting.");
                return;
            }

            IsBusy = true;
            var transactionId = await _coffeeEconomyService.TransfareTokensAsync(Recipient, Tokens);
            System.Console.WriteLine($"TransactionId: {transactionId}");

            Recipient = null;
            Tokens = 0;
            IsBusy = false;
        }
    }
}
