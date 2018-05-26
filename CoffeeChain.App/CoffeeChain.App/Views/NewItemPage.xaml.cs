using System;
using CoffeeChain.Connector.FunctionOutputs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeChain.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public CoffeeMaker Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new CoffeeMaker
            {
                Name = "Item name",
                Department = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}