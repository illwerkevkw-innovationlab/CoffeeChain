using CoffeeChain.App.ViewModels;
using CoffeeChain.Connector.FunctionOutputs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoffeeMakerDetailPage : ContentPage
	{
        CoffeeMakerDetailViewModel viewModel;

        public CoffeeMakerDetailPage(CoffeeMakerDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public CoffeeMakerDetailPage()
        {
            InitializeComponent();

            var item = new CoffeeMaker
            {
                OwenerAddress = "0x123987asd87asd89712398s7ad98asd",
                Name = "Nespresso @ iLab",
                DescriptiveLocation = "G107, Innovation Lab",
                Department = "E4A / iLab",
                Latitude = "22.123",
                Longitude = "42.123",
                MachineInfo = "Nespresso Speciale",
                MachineType = MachineType.Capsule,
            };

            viewModel = new CoffeeMakerDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}