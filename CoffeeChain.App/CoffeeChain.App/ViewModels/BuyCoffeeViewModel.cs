using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeChain.App.Services;
using CoffeeChain.Connector.FunctionOutputs;
using Xamarin.Forms;

namespace CoffeeChain.App.ViewModels
{
    public class BuyCoffeeViewModel : BaseViewModel
    {
        private readonly CoffeeMakerStorageService _coffeeMakerStorageService;

        private string _coffeeMaker;
        public string CoffeeMaker
        {
            get { return _coffeeMaker; }
            set { SetProperty(ref _coffeeMaker, value); }
        }

        private CoffeeMaker _coffeeMakerDetails;
        public CoffeeMaker CoffeeMakerDetails
        {
            get { return _coffeeMakerDetails; }
            set { SetProperty(ref _coffeeMakerDetails, value); }
        }

        private IList<CoffeeMakerProgram> _coffeeMakerPrograms = new List<CoffeeMakerProgram>();
        public IList<CoffeeMakerProgram> CoffeePrograms
        {
            get { return _coffeeMakerPrograms; }
            set { SetProperty(ref _coffeeMakerPrograms, value); }
        }

        private int _selectedCoffeeProgram = -1;
        public int SelectedCoffeeProgram
        {
            get { return _selectedCoffeeProgram; }
            set { SetProperty(ref _selectedCoffeeProgram, value); }
        }

        private int _numberOfCoffees = 1;
        public int NumberOfCoffees
        {
            get { return _numberOfCoffees; }
            set { SetProperty(ref _numberOfCoffees, value); }
        }

        public ICommand ExecuteCoffeePurchaseCommand { get; private set; }

        public BuyCoffeeViewModel(CoffeeMakerStorageService coffeeMakerStorageService)
        {
            _coffeeMakerStorageService = coffeeMakerStorageService;

            Title = "Kaffee kaufen";

            ExecuteCoffeePurchaseCommand = new Command(ExecuteCoffeePurchase);

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CoffeeMaker))
                {
                    Task.Run(() => LoadCoffeeMakerPrograms());
                }
            };
        }

        private async void LoadCoffeeMakerPrograms()
        {
            if (CoffeeMaker.IsNullOrEmpty() || !CoffeeMaker.IsValidEthereumAddress())
            {
                return;
            }

            try
            {
                var details = await _coffeeEconomyService.GetCoffeeMakerDataAsync(CoffeeMaker);
                if (details == null) { return; }
                CoffeeMakerDetails = details;

                var programs = await _coffeeEconomyService.GetCoffeeMakerProgramCountAsync(CoffeeMaker);
                Console.WriteLine($"Found {programs} coffee programs!");

                if (programs > 0)
                {
                    var list = new List<CoffeeMakerProgram>();
                    for (int i = 0; i < programs; i++)
                    {
                        var program = await _coffeeEconomyService.GetCoffeeMakerProgramDetailsAsync(CoffeeMaker, i);
                        list.Add(program);
                        Console.WriteLine($"Coffee program: {program.Name}, Price: {program.Price}");
                    }
                    CoffeePrograms = list;
                    SelectedCoffeeProgram = 0;

                    // add the discovered coffee maker to our list of known coffee makers but only if it is a valid one
                    var extendedCoffeeMakerDetails = new Models.CoffeeMaker
                    {
                        Address = CoffeeMaker,
                        OwenerAddress = details.OwenerAddress,
                        Name = details.Name,
                        Department = details.Department,
                        DescriptiveLocation = details.DescriptiveLocation,
                        Latitude = details.Latitude,
                        Longitude = details.Longitude,
                        MachineInfo = details.MachineInfo,
                        MachineType = details.MachineType,
                    };
                    _coffeeMakerStorageService.AddOrUpdateCoffeeMakerDetails(extendedCoffeeMakerDetails);
                }
            }
            catch (Exception e)
            {
                // reset our view data
                ResetLoadedVariables();
            }
        }

        private async void ExecuteCoffeePurchase()
        {
            Console.WriteLine($"CoffeeMaker: {CoffeeMaker}, Program: {SelectedCoffeeProgram}, NumberOfCoffees: {NumberOfCoffees}");
            Console.WriteLine($"Sender: {_web3.TransactionManager.Account.Address}");

            if (CoffeeMaker.IsNullOrEmpty() || SelectedCoffeeProgram < 0 || NumberOfCoffees <= 0)
            {
                Console.WriteLine("Invalid input for transaction. Aborting.");
                return;
            }

            IsBusy = true;
            var transactionId = await _coffeeEconomyService.BuyCoffeeAsync(CoffeeMaker, SelectedCoffeeProgram, NumberOfCoffees);
            Console.WriteLine($"TransactionId: {transactionId}");

            CoffeeMaker = null;
            ResetLoadedVariables();
            IsBusy = false;
        }

        private void ResetLoadedVariables()
        {
            CoffeeMakerDetails = null;
            SelectedCoffeeProgram = -1;
            CoffeePrograms = new List<CoffeeMakerProgram>();
            NumberOfCoffees = 1;
        }
    }
}
