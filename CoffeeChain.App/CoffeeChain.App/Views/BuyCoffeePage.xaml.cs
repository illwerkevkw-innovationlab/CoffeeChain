using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyCoffeePage : ContentPage
    {
        public BuyCoffeePage ()
        {
            InitializeComponent ();
        }

        private async void btnScan_ClickedAsync(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);

            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    txtBarcode.Text = result.Text;
                });
            };
        }
    }
}
