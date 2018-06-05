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

        private async void btnConfirm_ClickedAsync(object sender, System.EventArgs e)
        {
            var wallet = inputWalletAddress.Text;
            var passphrase = inputPassPhrase.Text;

            if (wallet.IsNullOrEmpty() || !wallet.IsValidEthereumAddress() || passphrase.IsNullOrEmpty())
            {
                return;
            }

            var web3 = new Web3(Settings.Current.ServerIpAddress);
            var coffeeEconomyService = new CoffeeEconomyService(new ManagedAccount(wallet, passphrase), web3, Settings.Current.ContractAddress);

            try
            {
                // check if wallet is a customer
                if (!(await coffeeEconomyService.IsCustomerAsync(wallet)))
                {
                    return; // it is no registered customer on our coffee chain
                }

                // check credentials via web3
                if (!(await web3.Personal.UnlockAccount.SendRequestAsync(wallet, passphrase, 0)))
                {
                    return; // these credentials seem invalid
                }
            }
            catch
            {
                return; // also if we have an error, this cannot be valid or useful, so we abort
            }

            Settings.Current.PublicWalletAddress = wallet;
            Settings.Current.Passphrase = passphrase;

            await Navigation.PopAllPopupAsync();
        }
    }
}
