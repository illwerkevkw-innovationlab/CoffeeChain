using CoffeeChain.App.ViewModels;
using CoffeeChain.App.Views;
using CoffeeChain.Connector;
using CommonServiceLocator;
using Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;

namespace CoffeeChain.App
{
    public class TestService
    {
        public string GetWallet() => "0x54585691af6387f8a23eae6f280d2b6a4c9dc586";
        public long GetTokens() => 1238190;
    }

    public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();

            var unityContainer = new UnityContainer();
            unityContainer.RegisterSingleton<ICoffeeEconomyService, CoffeeEconomyService>();
            unityContainer.RegisterType<TestService>();

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
	}
}
