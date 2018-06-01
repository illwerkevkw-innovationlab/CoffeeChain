using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeChain.App.Models;
using CoffeeChain.App.Views;
using CoffeeChain.Connector;
using CommonServiceLocator;
using Nethereum.Web3.Accounts;
using Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;

namespace CoffeeChain.App
{
    public partial class App : Application, INotifyPropertyChanged
    {
        private bool _isLoaded = false;
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                OnPropertyChanged(nameof(IsLoaded));
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public bool IsLoading
        {
            get { return !_isLoaded; }
        }

        public App ()
        {
            InitializeComponent();

            var unityContainer = new UnityContainer();

            Task.Run(() => RegisterCoffeeEconomyService(unityContainer));

            var unityServiceLocator = new UnityServiceLocator(unityContainer);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);

            MainPage = new MainPage();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }

        private void RegisterCoffeeEconomyService(IUnityContainer container)
        {
            //var account = Account.LoadFromKeyStore(KeyFile, PassPhrase);
            //var account = new ManagedAccount(AccountAddress, PassPhrase);
            var account = new Account(Settings.Current.PrivateWalletKey, Settings.Current.ChainId);
            var web3 = new Nethereum.Web3.Web3(account, Settings.Current.ServerIpAddress);
            var service = new CoffeeEconomyService(account, web3, Settings.Current.ContractAddress);

            container.RegisterInstance(web3);
            container.RegisterInstance<ICoffeeEconomyService>(service);

            IsLoaded = true;
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
