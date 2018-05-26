using CoffeeChain.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BalancePage : ContentPage
    {
        BalanceViewModel viewModel;

        public BalancePage(BalanceViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public BalancePage() : this(new BalanceViewModel
        {
            Wallet = "0x54585691af6387f8a23eae6f280d2b6a4c9dc586",
            CoffeeTokens = 18000,
        })
        { }
    }
}