using CoffeeChain.App.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        TransactionsViewModel _viewModel;

        public TransactionsPage()
        {
            InitializeComponent();

            _viewModel = ServiceLocator.Current.GetInstance<TransactionsViewModel>();

            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            await _viewModel.OnAppearing();
        }
    }
}
