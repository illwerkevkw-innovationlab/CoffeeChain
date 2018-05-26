using CoffeeChain.Connector.FunctionOutputs;

namespace CoffeeChain.App.ViewModels
{
    public class CoffeeMakerDetailViewModel : BaseViewModel
    {
        public CoffeeMaker CoffeeMaker { get; set; }
        public CoffeeMakerDetailViewModel(CoffeeMaker coffeeMaker = null)
        {
            Title = coffeeMaker?.Name;
            CoffeeMaker = coffeeMaker;
        }
    }
}
