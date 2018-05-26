
using CoffeeChain.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoffeeMakersPage : ContentPage
	{
        CoffeeMakerViewModel viewModel;

        public CoffeeMakersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CoffeeMakerViewModel();
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    var coffeeMaker = args.SelectedItem as CoffeeMaker;
        //    if (coffeeMaker == null)
        //        return;

        //    await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(coffeeMaker)));

        //    // Manually deselect item.
        //    ItemsListView.SelectedItem = null;
        //}

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (viewModel.CoffeeMakers.Count == 0)
        //        viewModel.LoadItemsCommand.Execute(null);
        //}
    }
}