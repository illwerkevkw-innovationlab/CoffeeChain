using CoffeeChain.App.ViewModels;
using CoffeeChain.App.Views;
using CoffeeChain.Connector;
using CommonServiceLocator;
using Nethereum.Web3.Accounts;
using Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;

namespace CoffeeChain.App
{
    public partial class App : Application
	{
        private static string KeyFile = @"{""address"":""54585691af6387f8a23eae6f280d2b6a4c9dc586"",""crypto"":{""cipher"":""aes-128-ctr"",""ciphertext"":""4b6379cd740dd45c8498b605bf0942df088bcce42d2c780fced563925fed5c82"",""cipherparams"":{""iv"":""685fe051614a893cad8cdaa3d128d87c""},""kdf"":""scrypt"",""kdfparams"":{""dklen"":32,""n"":262144,""p"":1,""r"":8,""salt"":""7c98b570a0e5807778d01db039a5a3bc88936511e02f4ab28a8c208b5b7c9278""},""mac"":""8cea0b9b4db934276d7b6aceac96a70d4b3c2c0aa1f2bc9d2dcee119fa295729""},""id"":""16657499-d1ef-4342-bb66-3e6b61ac89ad"",""version"":3}";
        private static string PassPhrase = "testtest";
        private static string ContractAddress = @"0x3F45D4615A21cB534E1b0173EDBCb0305E41da96";

        public App ()
        {
            InitializeComponent();

            var unityContainer = new UnityContainer();

            RegisterCoffeeEconomyService(unityContainer);

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
            var web3 = new Nethereum.Web3.Web3(account, "http://192.168.1.166:30304");
            var account = Account.LoadFromKeyStore(KeyFile, PassPhrase);
            var service = new CoffeeEconomyService(account, web3, ContractAddress);

            container.RegisterInstance(web3);
            container.RegisterInstance<ICoffeeEconomyService>(service);
        }
	}
}
