using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();
		}

        private async void OnButtonClicked_Account(object sender, EventArgs args)
        {
            await NavigateToNewPage(typeof(AccountPage), "Account");
        }

        private async void OnButtonClicked_CoffeeMakers(object sender, EventArgs args)
        {
            await NavigateToNewPage(typeof(CoffeeMakersPage), "Kaffeemaschinen");
        }

        private async void OnButtonClicked_Transactions(object sender, EventArgs args)
        {
            await NavigateToNewPage(typeof(TransactionsPage), "Transaktionen");
        }

        private async void OnButtonClicked_TransferTokens(object sender, EventArgs args)
        {
            await NavigateToNewPage(typeof(TokenTransferPage), "Kaffee Token verschicken");
        }

        private async void OnButtonClicked_BuyCoffee(object sender, EventArgs args)
        {
            await NavigateToNewPage(typeof(BuyCoffeePage), "Kaffee kaufen");
        }

        private async Task NavigateToNewPage(Type pageType, string title = "")
        {
            var mainPage = Application.Current.MainPage as MasterDetailPage;

            var page = (Page)Activator.CreateInstance(pageType);
            page.Title = title;

            if (mainPage == null)
            {
                await Navigation.PushAsync(page);
            }
            else
            {
                mainPage.Detail = new NavigationPage(page);
            }
        }
    }
}
