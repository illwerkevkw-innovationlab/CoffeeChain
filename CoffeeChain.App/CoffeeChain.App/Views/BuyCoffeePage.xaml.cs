using System;
using CoffeeChain.App.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyCoffeePage : ContentPage
    {
        private BuyCoffeeViewModel _viewModel;

        public BuyCoffeePage ()
        {
            InitializeComponent ();

            _viewModel = ServiceLocator.Current.GetInstance<BuyCoffeeViewModel>();

            BindingContext = _viewModel;
        }

        public BuyCoffeePage(string wallet) : this()
        {
            _viewModel.CoffeeMaker = wallet;
        }

        private async void btnScan_ClickedAsync(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);

            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    scan.IsScanning = false;
                    await Navigation.PopAsync();
                    _viewModel.CoffeeMaker = result.Text;
                });
            };
            scan.IsScanning = true;
        }
    }
}
