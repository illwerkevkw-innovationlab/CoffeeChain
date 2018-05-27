using System;
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
            var mainPage = App.Current.MainPage as MasterDetailPage;

            var page = (Page)Activator.CreateInstance<AccountPage>();
            page.Title = "Account";

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
