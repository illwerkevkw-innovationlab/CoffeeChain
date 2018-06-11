using System;
using CoffeeChain.App.Models;
using CoffeeChain.Connector;
using Nethereum.Web3;
using Nethereum.Web3.Accounts.Managed;
using Rg.Plugins.Popup.Extensions;

namespace CoffeeChain.App.Views
{
    public partial class NewAccountFormPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public NewAccountFormPage()
        {
            InitializeComponent();
        }

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            //return base.OnBackgroundClicked();
            return false;
        }

        private async void btnConfirm_ClickedAsync(object sender, EventArgs eventArgs)
        {
            var name = inputName.Text;
            var department = inputDepartment.Text ?? string.Empty;
            var telephone = inputTelephone.Text ?? string.Empty;
            var email = inputEmail.Text ?? string.Empty;
            var passphrase = inputPassPhrase.Text;

            Console.WriteLine($"Name: {name}, Department: {department}, Telephone: {telephone}, Email: {email}, Passphrase: {passphrase}");

            if (name.IsNullOrEmpty() || passphrase.IsNullOrEmpty())
            {
                Console.WriteLine($"Invalid input. Aborting.");
                return;
            }

            var web3 = new Web3(Settings.Current.ServerIpAddress);

            string wallet;
            try
            {
                // creating the new account
                wallet = await web3.Personal.NewAccount.SendRequestAsync(passphrase);
                Console.WriteLine($"The new wallet has the address {wallet}.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occured when creating a new account.");
                Console.WriteLine(e);
                return;
            }

            var coffeeEconomyService = new CoffeeEconomyService(new ManagedAccount(wallet, passphrase), web3, Settings.Current.ContractAddress);

            try
            {
                // create customer for wallet
                var transactionId = await coffeeEconomyService.AddCustomerAsync(wallet, name, department, telephone, email);
                Console.WriteLine($"Created customer with transactionId {transactionId}.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occured when creating the customer.");
                Console.WriteLine(e);
                return; // also if we have an error, this cannot be valid or useful, so we abort
            }

            Settings.Current.PublicWalletAddress = wallet;
            Settings.Current.Passphrase = passphrase;

            Console.WriteLine($"Successfully set credentials and returning to dashboard.");

            await Navigation.PopAllPopupAsync();
        }
    }
}
