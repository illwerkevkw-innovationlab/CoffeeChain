using System;
using CoffeeChain.App.Models;
using CoffeeChain.Connector;
using Nethereum.Web3;
using Nethereum.Web3.Accounts.Managed;
using Rg.Plugins.Popup.Extensions;

namespace CoffeeChain.App.Views
{
    public partial class OldAccountFormPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public OldAccountFormPage()
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
            var wallet = inputWalletAddress.Text;
            var passphrase = inputPassPhrase.Text;

            Console.WriteLine($"Wallet: {wallet}, Passphrase: {passphrase}");

            if (wallet.IsNullOrEmpty() || !wallet.IsValidEthereumAddress() || passphrase.IsNullOrEmpty())
            {
                Console.WriteLine($"Invalid input. Aborting.");
                return;
            }

            var web3 = new Web3(Settings.Current.ServerIpAddress);
            var coffeeEconomyService = new CoffeeEconomyService(new ManagedAccount(wallet, passphrase), web3, Settings.Current.ContractAddress);

            try
            {
                // check if wallet is a customer
                //if (!(await coffeeEconomyService.IsCustomerAsync(wallet)))
                //{
                //    System.Console.WriteLine($"Given wallet ({wallet}) is no customer.");
                //    return; // it is no registered customer on our coffee chain
                //}

                // check credentials via web3
                if (!(await web3.Personal.UnlockAccount.SendRequestAsync(wallet, passphrase, 0)))
                {
                    Console.WriteLine($"Unlocking account with given credentials failed.");
                    return; // these credentials seem invalid
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occured.");
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
