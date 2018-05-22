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
                    break;

                case "3": // TRANS Add Customer
                    await SendAddCustomerTransaction();
                    break;

                case "4": // TRANS Add Coffeemaker
                    break;

                case "5": // TRANS Add Coffeemaker Program
                    break;

                case "6": // TRANS Buy Tokens
                    await SendBuyTokensTransaction();
                    break;

                case "7": // TRANS Sell Tokens
                    break;

                case "8": // TRANS Transfer Tokens
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

        private async Task SendBuyTokensTransaction()
        {
            PrintAuthorizedWalletsOnlyWarning();

            var target = AskForTargetWallet();

            var amount = UnitConversion.Convert.ToWei(AskForEthereum());
            Console.WriteLine($"Transfering wei: {amount}");

            var result = await _coffeeEconomyService.BuyTokens(target, amount);
            Console.WriteLine($"Tokens successfully bought with transactionId {result}.");
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
