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

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Nicht verfügbar", "Diese Funktion ist in der aktuellen Version noch nicht verfügbar.", "Alles klar!");
            });

            //await _viewModel.OnAppearing();
        }
    }
}
