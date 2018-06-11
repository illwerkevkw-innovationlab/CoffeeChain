using Rg.Plugins.Popup.Extensions;

namespace CoffeeChain.App.Views
{
    public partial class AskForAccountPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AskForAccountPopup()
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

        private async void btnOldAccount_ClickedAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new OldAccountFormPage());
        }

        private async void btnNewAccount_ClickedAsync(object sender, System.EventArgs e)
        {
            await DisplayAlert("Nicht verfügbar", "Diese Funktion ist in der aktuellen Version noch nicht verfügbar.", "Alles klar!");
            //await Navigation.PushPopupAsync(new NewAccountFormPage());
        }
    }
}
