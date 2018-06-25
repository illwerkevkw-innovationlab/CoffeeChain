using System;
using CoffeeChain.App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoffeeMakersPage : ContentPage
    {
        public CoffeeMakersPage()
        {
            InitializeComponent();

            BindingContext = Settings.Current;
        }

        private void CoffeeMakerListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var coffeeMaker = e.SelectedItem as CoffeeMaker;
            if (coffeeMaker != null)
            {
                var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
                if (masterDetailPage != null) {
                    var page = (Page)Activator.CreateInstance(typeof(BuyCoffeePage), coffeeMaker.Address);
                    page.Title = "Kaffee kaufen";

                    masterDetailPage.Detail = new NavigationPage(page);
                }
            }
        }
    }
}
