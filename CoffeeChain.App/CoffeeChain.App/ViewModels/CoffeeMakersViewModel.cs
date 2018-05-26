using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace CoffeeChain.App.ViewModels
{
    public class CoffeeMakerViewModel : BaseViewModel
    {
        public ObservableCollection<Connector.FunctionOutputs.CoffeeMaker> CoffeeMakers { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CoffeeMakerViewModel()
        {
            Title = "Browse";
            CoffeeMakers = new ObservableCollection<Connector.FunctionOutputs.CoffeeMaker>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var _item = item as Item;
            //    CoffeeMakers.Add(_item);
            //    await DataStore.AddItemAsync(_item);
            //});
        }

        //async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        CoffeeMakers.Clear();
        //        var coffeeMakers = await CoffeeEconomyService.GetCoffeeMakerDataAsync(null); // TODO: change parameter
        //        foreach (var item in coffeeMakers)
        //        {
        //            CoffeeMakers.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}