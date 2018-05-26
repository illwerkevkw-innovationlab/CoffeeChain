using CoffeeChain.App.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BalancePage : ContentPage
    {
        BalanceViewModel _viewModel;

        public BalancePage()
        {
            InitializeComponent();

            _viewModel = ServiceLocator.Current.GetInstance<BalanceViewModel>();

            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            await _viewModel.OnAppearing();
        }
    }
}
