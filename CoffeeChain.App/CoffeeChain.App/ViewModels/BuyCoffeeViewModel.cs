﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeChain.Connector;
using CoffeeChain.Connector.FunctionOutputs;
using Nethereum.Web3;
using Xamarin.Forms;

namespace CoffeeChain.App.ViewModels
{
    public class BuyCoffeeViewModel : BaseViewModel
    {
        private readonly Web3 _web3;
        private readonly ICoffeeEconomyService _coffeeEconomyService;

        private string _coffeeMaker;
        public string CoffeeMaker
        {
            get { return _coffeeMaker; }
            set { SetProperty(ref _coffeeMaker, value); }
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

        public BuyCoffeeViewModel(Web3 web3, ICoffeeEconomyService coffeeEconomyService)
        {
            _web3 = web3;
            _coffeeEconomyService = coffeeEconomyService;

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
            if (CoffeeMaker.IsNullOrEmpty())
            {
                return;
            }

            try
            {
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
                }
            }
            catch (Exception e)
            {
                throw;
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
            SelectedCoffeeProgram = -1;
            CoffeePrograms = new List<CoffeeMakerProgram>();
            NumberOfCoffees = 1;
            IsBusy = false;
        }
    }
}
