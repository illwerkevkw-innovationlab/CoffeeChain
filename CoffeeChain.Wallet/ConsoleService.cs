using System;
using System.Threading.Tasks;
using Nethereum.Util;
using Nethereum.Web3;

namespace CoffeeChain.Wallet
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
                    break;

                case "10": // CALL Display Customer Data
                    break;

                case "11": // CALL Display Coffeemaker Data
                    break;

                case "12": // CALL Count Coffeemaker Programs
                    break;

                case "13": // CALL Get Programm Details
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

            var tokens = await _coffeeEconomyService.GetTokens(target);
            Console.WriteLine($"Es befinden sich {tokens} Token in der Wallet.");
        }

        private async Task SendAddAuthorizedExchangeWallet()
        {
            var target = AskForTargetWallet();
            var result = await _coffeeEconomyService.AddAuthorizedExchangeWallet(target);
            Console.WriteLine($"Exchangewallet successfully created with transactionId {result}.");
        }

        private async Task SendAddCustomerTransaction()
        {
            var target = AskForTargetWallet();
            var name = AskFor("name");
            var department = AskFor("department");
            var telephone = AskFor("telephone");
            var email = AskFor("email");

            var result = await _coffeeEconomyService.AddCustomer(target, name, department, telephone, email);
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
            
            Console.Write(@"Which Machinetype do you have? Press a Number:
    1    ---     Capsules
    2    ---     Pads
    3    ---     Filter
    4    ---     Pulver
    5    ---     FullyAutomatic
    6    ---     VendingMachine 
    ");
            int infoMachineType = Convert.ToInt32(Console.ReadLine());
            var infoDescription = AskFor("description");
            
            var result = await _coffeeEconomyService.AddCoffeemaker(target, name, locDescriptive, locDepartment, locLatitude, locLongitude, infoMachineType, infoDescription);
            Console.WriteLine($"Coffeemaker successfully created with transactionId {result}.");
        }

        private async Task AddCoffeeMakerProgram()
        {
            var target = AskForTargetWallet();
            var name = AskFor("Coffeename");
            Console.Write(@"How much Tokens should this Coffee cost (100 Token = 1 €) ?
            ");
            int cost = Convert.ToInt32(Console.ReadLine());
            var result = await _coffeeEconomyService.AddCoffeemakerPogram(target, name, cost );
            Console.WriteLine($"Coffeemaker Program successfully created with transactionId {result}.");

        }

        private async Task SendBuyTokensTransaction()
        {
            PrintAuthorizedWalletsOnlyWarning();

            var target = AskForTargetWallet();

            var amount = UnitConversion.Convert.ToWei(AskForEthereum());
            Console.WriteLine($"Transfering wei: {amount}");

            var result = await _coffeeEconomyService.BuyTokens(target, amount);
            Console.WriteLine($"Tokens successfully bought with transactionId {result}.");
        }

        private async Task SendSellTokens()
        {
            var seller = AskFor("Seller Wallet");
            Console.WriteLine(@"Enter Amount of Tokens to sell:
            ");
            int amount = Convert.ToInt32(Console.ReadLine());
            var result = await _coffeeEconomyService.SellTokens(seller, amount);
            Console.WriteLine($"Tokens successfully sold with transactionId {result}.");

        }

        private async Task SendTransferTokens()
        {
            var receiver = AskFor("Receiver Wallet");
            Console.WriteLine(@"Enter Amount of Tokens to transfare:
            ");
            int amount = Convert.ToInt32(Console.ReadLine());
            
            var result = await _coffeeEconomyService.TransfareTokens(receiver, amount);
            Console.WriteLine($"Tokens successfully transfared with transactionId {result}.");

        }


        private void PrintMenu()
        {
            Console.WriteLine(@"What would you like to do? Press a Number:
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
    0    ---     Close ");
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
            Console.WriteLine("Enter Amount in ETH (1 ETH = 100 Token)");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
