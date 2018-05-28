using System.Collections.ObjectModel;
using CoffeeChain.App.Models;
using CoffeeChain.App.Views;

namespace CoffeeChain.App.ViewModels
{
    class MainPageMasterViewModel : BaseViewModel
    {
        public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

        public MainPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
            {
                new MainPageMenuItem { Id = 0, Title = "Dashboard", TargetType = typeof(DashboardPage) },
                new MainPageMenuItem { Id = 1, Title = "Account", TargetType = typeof(AccountPage) },
                new MainPageMenuItem { Id = 2, Title = "Kaffeemaschinen", TargetType = typeof(CoffeeMakersPage) },
                new MainPageMenuItem { Id = 3, Title = "Kaffee kaufen", TargetType = typeof(BuyCoffeePage)},
                new MainPageMenuItem { Id = 4, Title = "Tokens verschicken", TargetType = typeof(TokenTransferPage) },
                new MainPageMenuItem { Id = 5, Title = "Transaktionen", TargetType = typeof(TransactionsPage) },
                new MainPageMenuItem { Id = 6, Title = "Einstellungen", TargetType = typeof(SettingsPage) },
            });
        }
    }
}
