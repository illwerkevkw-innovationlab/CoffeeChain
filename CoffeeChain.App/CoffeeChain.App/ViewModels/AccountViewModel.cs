using System.Threading.Tasks;
using CoffeeChain.App.Models;

namespace CoffeeChain.App.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        public AccountViewModel()
        {
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
