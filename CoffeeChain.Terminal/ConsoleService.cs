using System;
using System.Threading.Tasks;
using CoffeeChain.Connector;
using Nethereum.Util;
using Nethereum.Web3;

namespace CoffeeChain.Terminal
{
    public class ConsoleService
    {
        private Web3 _web3;
        private CoffeeEconomyService _coffeeEconomyService;

        public ConsoleService(Web3 web3, CoffeeEconomyService coffeeEconomyService)
        {
            _web3 = web3;
            _coffeeEconomyService = coffeeEconomyService;
        }

        public async Task Handle()
        {
            PrintMenu();

            string option = Console.ReadLine();
            switch (option)
            {
                case "1": // CALL Display Wallet Tokens
                    await CallDisplayWalletTokens();
                    break;

                case "2": // TRANS Add Authorized Exchange Wallet
                    await SendAddAuthorizedExchangeWallet();
                    break;

                case "3": // TRANS Add Customer
                    await SendAddCustomerTransaction();
                    break;

                case "4": // TRANS Add Coffeemaker
                    await SendAddCoffeeMaker();
                    break;

                case "5": // TRANS Add Coffeemaker Program
                    await AddCoffeeMakerProgram();
                    break;

                case "6": // TRANS Buy Tokens
                    await SendBuyTokensTransaction();
                    break;

                case "7": // TRANS Sell Tokens
                    await SendSellTokens();
                    break;

                case "8": // TRANS Transfer Tokens
                    await SendTransferTokens();
                    break;

                case "9": // TRANS Buy Coffee
                    await SendBuyCoffee();
                    break;

                case "10": // CALL Display Customer Data
                    await CallDisplayCustomerData();
                    break;

                case "11": // CALL Display Coffeemaker Data
                    await CallDisplayCoffeeMakerData();
                    break;

                case "12": // CALL Count Coffeemaker Programs
                    await CallCountPrograms();
                    break;

                case "13": // CALL Get Programm Details
                    await CallGetProgramDetails();
                    break;

                case "14": // CALL Get CoffeeBought EventLogs
                    await CallGetCoffeeBoughtEventLog();
                    break;

                default:
                    Console.WriteLine("Invalid input. Please select a valid option from the list of options.");
                    break;
            }

            Console.ReadKey();
        }

        private async Task CallDisplayWalletTokens()
        {
            var target = AskForTargetWallet();

            var tokens = await _coffeeEconomyService.GetTokensAsync(target);
            Console.WriteLine($"There are {tokens} tokens in the wallet.");
        }

        private async Task SendAddAuthorizedExchangeWallet()
        {
            var target = AskForTargetWallet();
            var result = await _coffeeEconomyService.AddAuthorizedExchangeWalletAsync(target);
            Console.WriteLine($"Exchangewallet successfully created with transactionId {result}.");
        }

        private async Task SendAddCustomerTransaction()
        {
            var target = AskForTargetWallet();
            var name = AskFor("name");
            var department = AskFor("department");
            var telephone = AskFor("telephone");
            var email = AskFor("email");

            var result = await _coffeeEconomyService.AddCustomerAsync(target, name, department, telephone, email);
            Console.WriteLine($"Customer successfully created with transactionId {result}.");
        }

        private async Task SendAddCoffeeMaker()
        {
            var target = AskForTargetWallet();
            var name = AskFor("name");
            var locDescriptive = AskFor("locDescriptive");
            var locDepartment = AskFor("department");
            var locLatitude = AskFor("latitude");
            var locLongitude = AskFor("longitude");

            PrintMachineTypeOptions();
            int infoMachineType = Convert.ToInt32(Console.ReadLine());
            var infoDescription = AskFor("description");

            var result = await _coffeeEconomyService.AddCoffeemakerAsync(target, name, locDescriptive, locDepartment, locLatitude, locLongitude, infoMachineType, infoDescription);
            Console.WriteLine($"Coffeemaker successfully created with transactionId {result}.");
        }

        private async Task AddCoffeeMakerProgram()
        {
            var target = AskForTargetWallet();
            var name = AskFor("coffee name");
            var cost = AskForCoffeePrice();

            var result = await _coffeeEconomyService.AddCoffeemakerPogramAsync(target, name, cost);
            Console.WriteLine($"Coffeemaker program successfully created with transactionId {result}.");
        }

        private async Task SendBuyTokensTransaction()
        {
            PrintAuthorizedWalletsOnlyWarning();

            var target = AskForTargetWallet();

            var amount = UnitConversion.Convert.ToWei(AskForEthereum());
            Console.WriteLine($"Transfering wei: {amount}");

            var result = await _coffeeEconomyService.BuyTokensAsync(target, amount);
            Console.WriteLine($"Tokens successfully bought with transactionId {result}.");
        }

        private async Task SendSellTokens()
        {
            var seller = AskFor("seller wallet");
            var amount = AskForAmountOfTokensToSell();

            var result = await _coffeeEconomyService.SellTokensAsync(seller, amount);
            Console.WriteLine($"Tokens successfully sold with transactionId {result}.");
        }

        private async Task SendTransferTokens()
        {
            var receiver = AskFor("receiver wallet");
            var amount = AskForAmountOfTokensToTransfer();

            var result = await _coffeeEconomyService.TransfareTokensAsync(receiver, amount);
            Console.WriteLine($"Tokens successfully transfered with transactionId {result}.");
        }

        private async Task SendBuyCoffee()
        {
            var coffeeMaker = AskForTargetWallet();
            var program = AskForCoffeeProgram();
            var amount = AskForAmountOfCoffees();

            var result = await _coffeeEconomyService.BuyCoffeeAsync(coffeeMaker, program, amount);
            Console.WriteLine($"Coffee successfully bought with transactionId {result}.");
        }

        private async Task CallDisplayCustomerData()
        {
            var target = AskForTargetWallet();

            var data = await _coffeeEconomyService.GetCustomerDataAsync(target);
            Console.WriteLine($"Name: {data.Name}");
            Console.WriteLine($"Email: {data.Email}");
            Console.WriteLine($"Telephone: {data.Telephone}");
            Console.WriteLine($"Department: {data.Department}");
        }

        private async Task CallDisplayCoffeeMakerData()
        {
            var target = AskForTargetWallet();

            var data = await _coffeeEconomyService.GetCoffeeMakerDataAsync(target);
            Console.WriteLine($"Name: {data.Name}");
            Console.WriteLine($"Owner Address: {data.OwenerAddress}");
            Console.WriteLine($"Location Description: {data.DescriptiveLocation}");
            Console.WriteLine($"Department: {data.Department}");
            Console.WriteLine($"Latitude: {data.Latitude}");
            Console.WriteLine($"Longitude: {data.Longitude}");
            Console.WriteLine($"Machine Type: {data.MachineType}");
            Console.WriteLine($"Machine Info: {data.MachineInfo}");
        }

        private async Task CallGetProgramDetails()
        {
            var target = AskForTargetWallet();
            var program = AskForCoffeeProgram();

            var data = await _coffeeEconomyService.GetCoffeeMakerProgramDetailsAsync(target, program);
            Console.WriteLine($"Name: {data.Name}");
            Console.WriteLine($"Price: {data.Price}");
        }

        private async Task CallCountPrograms()
        {
            var target = AskForTargetWallet();

            int count = await _coffeeEconomyService.GetCoffeeMakerProgramCountAsync(target);
            Console.WriteLine($"There are {count} programs available.");
        }

        private async Task CallGetCoffeeBoughtEventLog()
        {
            var target = AskForTargetWallet();

            var data = await _coffeeEconomyService.GetCoffeeBoughtEventsForWallet(target);
            if (data.Count == 0)
            {
                Console.WriteLine("No event data found for wallet.");
            }
            foreach (var elem in data)
            {
                Console.WriteLine($"Wallet: {elem.Event.CoffeeMaker}, Program: {elem.Event.Program}, Amount: {elem.Event.Amount}");
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine(@"What would you like to do? Please enter one of the following numbers:
    1    ---     Get Tokens from an Address
    2    ---     Add  Authorized Exchange Wallet 
    3    ---     Add Customer
    4    ---     Add Coffeemaker 
    5    ---     Add Coffeemaker Program 
    6    ---     Buy Tokens
    7    ---     Sell Tokens 
    8    ---     Transfere Tokens
    9    ---     Buy Coffee 
    10   ---     Display Customer Data 
    11   ---     Display Coffeemaker Data
    12   ---     Count Coffeemaker Programs
    13   ---     Get Program Details
    14   ---     Get EventLog for CoffeeBought
    0    ---     Close");
        }

        private void PrintMachineTypeOptions()
        {
            Console.WriteLine(@"Which machine type do you have? Please enter one of the following numbers:
    0    ---     Capsule Machine
    1    ---     Pad Machine (e.g. Nespresso)
    2    ---     Filter Machine
    3    ---     Pulver Machine
    4    ---     Fully Automatic Machine
    5    ---     Vending Machine");
        }

        private void PrintAuthorizedWalletsOnlyWarning()
        {
            Console.WriteLine("!! Please be aware that the wallet loaded through the KeyFile needs to be authorized to perform these kind of transactions. !!");
        }

        private string AskFor(string type)
        {
            Console.WriteLine($"Please enter a {type}:");
            return Console.ReadLine();
        }

        private string AskForTargetWallet()
        {
            return AskFor("target wallet");
        }

        private int AskForEthereum()
        {
            Console.WriteLine("Enter amount in ETH (1 ETH = 100 tokens):");
            return Convert.ToInt32(Console.ReadLine());
        }

        private int AskForCoffeeProgram()
        {
            Console.WriteLine("Enter program number:");
            return Convert.ToInt32(Console.ReadLine());
        }

        private int AskForAmountOfCoffees()
        {
            Console.WriteLine("Enter amount of coffees:");
            return Convert.ToInt32(Console.ReadLine());
        }

        private int AskForAmountOfTokensToTransfer()
        {
            Console.WriteLine("Enter amount of tokens to transfer:");
            return Convert.ToInt32(Console.ReadLine());
        }

        private int AskForAmountOfTokensToSell()
        {
            Console.WriteLine("Enter amount of tokens to sell:");
            return Convert.ToInt32(Console.ReadLine());
        }

        private int AskForCoffeePrice()
        {
            Console.Write("How much tokens should this coffee cost (100 tokens = 1 €)?");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
