using CoffeeChain.App.Models;

namespace CoffeeChain.App.Services
{
    public class CoffeeMakerStorageService
    {
        public void AddOrUpdateCoffeeMakerDetails(CoffeeMaker coffeeMaker)
        {
            var knownCoffeeMakers = Settings.Current.KnownCoffeeMakers;

            knownCoffeeMakers.RemoveAll(c => c.Address == coffeeMaker.Address);
            knownCoffeeMakers.Add(coffeeMaker);

            Settings.Current.KnownCoffeeMakers = knownCoffeeMakers;
        }
    }
}
