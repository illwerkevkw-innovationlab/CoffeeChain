using CoffeeChain.App.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        AccountViewModel _viewModel;

        public AccountPage()
        {
            InitializeComponent();

            _viewModel = ServiceLocator.Current.GetInstance<AccountViewModel>();

            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            await _viewModel.OnAppearing();
        }
    }
}
