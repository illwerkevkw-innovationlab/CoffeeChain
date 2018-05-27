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
                new MainPageMenuItem { Id = 2, Title = "Kaffeemaschinen", TargetType = typeof(DefaultPage) },
                new MainPageMenuItem { Id = 3, Title = "Tokens verschicken", TargetType = typeof(DefaultPage) },
                new MainPageMenuItem { Id = 4, Title = "Transaktionen", TargetType = typeof(DefaultPage) },
                new MainPageMenuItem { Id = 5, Title = "Einstellungen", TargetType = typeof(DefaultPage) },
            });
        }
    }
}
