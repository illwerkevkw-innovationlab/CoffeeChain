using CoffeeChain.App.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokenTransferPage : ContentPage
    {
        TokenTransferViewModel _viewModel;

        public TokenTransferPage()
        {
            InitializeComponent();

            _viewModel = ServiceLocator.Current.GetInstance<TokenTransferViewModel>();

            BindingContext = _viewModel;
        }
    }
}
