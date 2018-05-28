using System.Threading.Tasks;
using CoffeeChain.App.Models;
using CoffeeChain.Connector;
using Nethereum.Web3;

namespace CoffeeChain.App.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private readonly Web3 _web3;
        private readonly ICoffeeEconomyService _coffeeEconomyService;

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        public AccountViewModel(Web3 web3, ICoffeeEconomyService coffeeEconomyService)
        {
            _web3 = web3;
            _coffeeEconomyService = coffeeEconomyService;

            Title = "Balance";
            IsBusy = true;
        }

        public async Task OnAppearing()
        {
            var customer = new Customer();
            customer.Wallet = _web3.TransactionManager.Account.Address;
            customer.CoffeeTokens = await _coffeeEconomyService.GetTokensAsync(customer.Wallet);

            var customerData = await _coffeeEconomyService.GetCustomerDataAsync(customer.Wallet);
            if (customerData != null)
            {
                customer.Name = customerData.Name;
                customer.Department = customerData.Department;
                customer.Email = customerData.Email;
                customer.Telephone = customerData.Telephone;
            }

            Customer = customer;

            IsBusy = false;
        }
    }
}
