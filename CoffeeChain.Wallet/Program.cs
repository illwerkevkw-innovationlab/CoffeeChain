using System;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using Xunit;

namespace CoffeeChain.Wallet {

    public class Program {
        // [Fact]
        // public async Task<T> CallFuncion () {
        //     var KeyFile = @"{""address"":""54585691af6387f8a23eae6f280d2b6a4c9dc586"",""crypto"":{""cipher"":""aes-128-ctr"",""ciphertext"":""4b6379cd740dd45c8498b605bf0942df088bcce42d2c780fced563925fed5c82"",""cipherparams"":{""iv"":""685fe051614a893cad8cdaa3d128d87c""},""kdf"":""scrypt"",""kdfparams"":{""dklen"":32,""n"":262144,""p"":1,""r"":8,""salt"":""7c98b570a0e5807778d01db039a5a3bc88936511e02f4ab28a8c208b5b7c9278""},""mac"":""8cea0b9b4db934276d7b6aceac96a70d4b3c2c0aa1f2bc9d2dcee119fa295729""},""id"":""16657499-d1ef-4342-bb66-3e6b61ac89ad"",""version"":3}";
        //     var ContractAbi = @"[ { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCustomerData"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""addAuthorizedExchangeWallet"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""addCoffeeMakerProgram"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""kill"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""name"": ""addCustomer"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getTokens"", ""outputs"": [ { ""name"": ""tokenss"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""locDescriptive"", ""type"": ""string"" }, { ""name"": ""locDepartment"", ""type"": ""string"" }, { ""name"": ""locLatitude"", ""type"": ""string"" }, { ""name"": ""locLongitude"", ""type"": ""string"" }, { ""name"": ""infoMachineType"", ""type"": ""uint8"" }, { ""name"": ""infoDescription"", ""type"": ""string"" } ], ""name"": ""addCoffeemaker"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" }, { ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""buyCoffee"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" } ], ""name"": ""getCoffeeMakerProgramDetails"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"", ""value"": """" }, { ""name"": ""price"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerProgramCount"", ""outputs"": [ { ""name"": ""programCount"", ""type"": ""uint8"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerData"", ""outputs"": [ { ""name"": ""owner"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""descriptiveLocation"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""latitude"", ""type"": ""string"" }, { ""name"": ""longitude"", ""type"": ""string"" }, { ""name"": ""machineType"", ""type"": ""uint8"" }, { ""name"": ""machineInfo"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""receiver"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""transferTokens"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""seller"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""sellTokens"", ""outputs"": [ { ""name"": ""soldTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""buyer"", ""type"": ""address"" } ], ""name"": ""buyTokens"", ""outputs"": [ { ""name"": ""receivedTokens"", ""type"": ""uint256"" } ], ""payable"": true, ""stateMutability"": ""payable"", ""type"": ""function"" }, { ""inputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""constructor"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""ExchangeWalletAuthorized"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""CustomerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""owner"", ""type"": ""address"" } ], ""name"": ""CoffeeMakerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""name"", ""type"": ""string"" }, { ""indexed"": false, ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""CoffeeMakerProgramAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""program"", ""type"": ""uint8"" }, { ""indexed"": false, ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""CoffeeBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensSold"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""sender"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""recipient"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensTransfered"", ""type"": ""event"" } ]";
        //       var ContractAddress = @"0x3F45D4615A21cB534E1b0173EDBCb0305E41da96";
        //     var password = "testtest";
        //     var target_addr = "0x04A83E168A69E9937990bf1A50b3505202B77929";

        //     var account = Account.LoadFromKeyStore (KeyFile, password);
        //     var web3 = new Nethereum.Web3.Web3 (account, "http://192.168.1.166:30304");
        //     Console.WriteLine ("Executing contract...");
        //     var contract = web3.Eth.GetContract (ContractAbi, ContractAddress);

        //     function Call_getToken () {
        //         var f_getTokens = contract.GetFunction ("getTokens");
        //         var gas = await f_getTokens.EstimateGasAsync (target);
        //         return gas;
        //     }

        //     //Console.WriteLine ($"Contract address: {f_getTokens.ContractAddress}");
        // }

        // [FunctionOutput]
        // // public class enum MachineType
        // // {
        // //     string Capsules,
        // //     string Pads, 
        // //     Filter, Pulver, FullyAutomatic, VendingMachine
        // // }
        // public class Location {
        //     [Parameter ("string", "descriptive", 1)]
        //     public string Beschreibung { get; set; }

        //     [Parameter ("string", "department", 2)]
        //     public string Abteilung { get; set; }

        //     [Parameter ("string", "latitude", 3)]
        //     public string Breitengrad { get; set; }

        //     [Parameter ("string", "longitude", 4)]
        //     public string Längengrad { get; set; }
        // }
        // //     string descriptive;
        // //     string department;
        // //     string latitude;
        // //     string longitude;
        // // }

        // public class MachineInfo {
        //     [Parameter ("MachineType", "machineType", 1)]
        //     public string MachineType { get; set; }
        //     [Parameter ("string", "description", 2)]
        //     public string Beschreibung { get; set; }
        // }
        // // struct MachineInfo {
        // //     MachineType machineType;
        // //     string description;
        // // }

        // public class CoffeeProgram {
        //     [Parameter ("string", "name", 1)]
        //     public string Name { get; set; }
        //     [Parameter ("unit", "price", 2)]
        //     public string Price { get; set; }
        // }
        // // struct CoffeeProgram {
        // //     string name;
        // //     uint price;
        // // }

        // public class CoffeeMaker {
        //     [Parameter ("bool", "exists", 1)]
        //     public string Exists { get; set; }
        //     [Parameter ("address", "owner", 2)]
        //     public string OwnerAdd { get; set; }
        //     [Parameter ("string", "name", 3)]
        //     public string Name { get; set; }
        //     [Parameter ("Location", "location", 4)]
        //     public string Location { get; set; }
        //     [Parameter ("MachineInfo", "machineInfo", 5)]
        //     public string MachineInfo { get; set; }

        // }   
        // // struct CoffeeMaker {
        // //     bool exists;
        // //     address owner;
        // //     string name;
        // //     Location location;
        // //     MachineInfo machineInfo;
        // //     mapping (uint8 => CoffeeProgram) programs;
        // //     uint8 programCounter;
        // // }

        // public class Customer {
        //     [Parameter ("bool", "exists", 1)]
        //     public string Exists { get; set; }
        //     [Parameter ("string", "name", 2)]
        //     public string OwnerAdd { get; set; }
        //     [Parameter ("string", "department", 3)]
        //     public string Name { get; set; }
        //     [Parameter ("string", "telephone", 4)]
        //     public string Location { get; set; }
        //     [Parameter ("string", "email", 4)]
        //     public string email { get; set; }
        // }
        // // struct Customer {
        // //     bool exists;
        // //     string name;
        // //     string department;
        // //     string telephone;
        // //     string email;
        // // }

        // //private static string KeyFile = @"{""address"":""e49ba2298028fc9c062a7f1be893f5272ff7b0c0"",""crypto"":{""cipher"":""aes-128-ctr"",""ciphertext"":""cc05b86823cec6196d01471ca67a7787e20f0fc1cc7fcce6a41b12680ae48015"",""cipherparams"":{""iv"":""21bbcb1d0ddf463b7c7806f80b4ed72c""},""kdf"":""scrypt"",""kdfparams"":{""dklen"":32,""n"":262144,""p"":1,""r"":8,""salt"":""6a4b5b01a4d097f18b87cbd88017abf4c26cf3df7685b76f6227da341a988d63""},""mac"":""ada1c84ee1daa04341706f10cc9dcc6540946d49d1a3db6bec857c6874d324cc""},""id"":""22ea6bb0-3706-49ba-a7fa-19076ba6e268"",""version"":3}";
        private static string KeyFile = @"{""address"":""54585691af6387f8a23eae6f280d2b6a4c9dc586"",""crypto"":{""cipher"":""aes-128-ctr"",""ciphertext"":""4b6379cd740dd45c8498b605bf0942df088bcce42d2c780fced563925fed5c82"",""cipherparams"":{""iv"":""685fe051614a893cad8cdaa3d128d87c""},""kdf"":""scrypt"",""kdfparams"":{""dklen"":32,""n"":262144,""p"":1,""r"":8,""salt"":""7c98b570a0e5807778d01db039a5a3bc88936511e02f4ab28a8c208b5b7c9278""},""mac"":""8cea0b9b4db934276d7b6aceac96a70d4b3c2c0aa1f2bc9d2dcee119fa295729""},""id"":""16657499-d1ef-4342-bb66-3e6b61ac89ad"",""version"":3}";
        // private static string ContractAbi = @"[ { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCustomerData"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""addAuthorizedExchangeWallet"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""addCoffeeMakerProgram"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""kill"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""name"": ""addCustomer"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getTokens"", ""outputs"": [ { ""name"": ""tokenss"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""locDescriptive"", ""type"": ""string"" }, { ""name"": ""locDepartment"", ""type"": ""string"" }, { ""name"": ""locLatitude"", ""type"": ""string"" }, { ""name"": ""locLongitude"", ""type"": ""string"" }, { ""name"": ""infoMachineType"", ""type"": ""uint8"" }, { ""name"": ""infoDescription"", ""type"": ""string"" } ], ""name"": ""addCoffeemaker"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" }, { ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""buyCoffee"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" } ], ""name"": ""getCoffeeMakerProgramDetails"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"", ""value"": """" }, { ""name"": ""price"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerProgramCount"", ""outputs"": [ { ""name"": ""programCount"", ""type"": ""uint8"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerData"", ""outputs"": [ { ""name"": ""owner"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""descriptiveLocation"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""latitude"", ""type"": ""string"" }, { ""name"": ""longitude"", ""type"": ""string"" }, { ""name"": ""machineType"", ""type"": ""uint8"" }, { ""name"": ""machineInfo"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""receiver"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""transferTokens"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""seller"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""sellTokens"", ""outputs"": [ { ""name"": ""soldTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""buyer"", ""type"": ""address"" } ], ""name"": ""buyTokens"", ""outputs"": [ { ""name"": ""receivedTokens"", ""type"": ""uint256"" } ], ""payable"": true, ""stateMutability"": ""payable"", ""type"": ""function"" }, { ""inputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""constructor"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""ExchangeWalletAuthorized"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""CustomerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""owner"", ""type"": ""address"" } ], ""name"": ""CoffeeMakerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""name"", ""type"": ""string"" }, { ""indexed"": false, ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""CoffeeMakerProgramAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""program"", ""type"": ""uint8"" }, { ""indexed"": false, ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""CoffeeBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensSold"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""sender"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""recipient"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensTransfered"", ""type"": ""event"" } ]";
        private static string ContractAddress = @"0x3F45D4615A21cB534E1b0173EDBCb0305E41da96";

        static async Task Main (string[] args) {

            Console.WriteLine ("Welcome to the CoffeeChain Wallet Application.");
            Console.WriteLine ("==============================================");

            Console.WriteLine ("Bootstrapping Nethereum...");

            //var password =  Console.ReadLine();
            var password = "testtest";

            //Console.WriteLine ("Please enter a target wallet:");
            // var target = Console.ReadLine();
            //var target = "1";

            var account = Account.LoadFromKeyStore (KeyFile, password);

            var web3 = new Nethereum.Web3.Web3 (account, "http://192.168.1.166:30304");
            var service = new Service (web3, ContractAddress);

            Console.WriteLine (@"What would you like to do ? 
            Press a Number 
            1    ---     get Tokens From an Address
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
            string option = Console.ReadLine ();
            string target;
            switch (option) {

                case "0":
                    break;
                case "1":
                    //CALL Get Tokens from Address
                    Console.WriteLine ("Please enter a target wallet:");
                    target = Console.ReadLine ();
                    var token = await service.getTokens (target);
                    Console.WriteLine ("Es befinden sich " + token + " Token auf der Wallet");
                    break;
                case "2":
                    //TRANS Add  Authorized Exchange Wallet

                    break;
                case "3":
                    //TRANS Add Customer

                    break;
                case "4":
                    //TRANS Add Coffeemaker

                    break;
                case "5":
                    //TRANS Add Coffeemaker Program

                    break;
                case "6":
                    //TRANS Buy Tokens

                    Console.WriteLine ("Enter Amount in ETH (1 ETH = 100 Token)");
                    var amount = UnitConversion.Convert.ToWei (Console.ReadLine ());

                    Console.WriteLine ($"Transfering wei: {amount}");
                    Console.WriteLine ("Please enter Wallet to deposit Tokens:");
                    //target = Console.ReadLine ();
                    // var result = web3.TransactionManager.SendTransactionAsync (account.Address, ContractAddress, new HexBigInteger (amount)).Result;
                    // Console.WriteLine ($"Transaction Id: {result}");
                    //var buytoken = await CallTranEvents.buyTokens;

                    // var KeyFile = @"{""address"":""54585691af6387f8a23eae6f280d2b6a4c9dc586"",""crypto"":{""cipher"":""aes-128-ctr"",""ciphertext"":""4b6379cd740dd45c8498b605bf0942df088bcce42d2c780fced563925fed5c82"",""cipherparams"":{""iv"":""685fe051614a893cad8cdaa3d128d87c""},""kdf"":""scrypt"",""kdfparams"":{""dklen"":32,""n"":262144,""p"":1,""r"":8,""salt"":""7c98b570a0e5807778d01db039a5a3bc88936511e02f4ab28a8c208b5b7c9278""},""mac"":""8cea0b9b4db934276d7b6aceac96a70d4b3c2c0aa1f2bc9d2dcee119fa295729""},""id"":""16657499-d1ef-4342-bb66-3e6b61ac89ad"",""version"":3}";
                    var abi = @"[ { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCustomerData"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""addAuthorizedExchangeWallet"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""addCoffeeMakerProgram"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""kill"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""name"": ""addCustomer"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getTokens"", ""outputs"": [ { ""name"": ""tokenss"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""locDescriptive"", ""type"": ""string"" }, { ""name"": ""locDepartment"", ""type"": ""string"" }, { ""name"": ""locLatitude"", ""type"": ""string"" }, { ""name"": ""locLongitude"", ""type"": ""string"" }, { ""name"": ""infoMachineType"", ""type"": ""uint8"" }, { ""name"": ""infoDescription"", ""type"": ""string"" } ], ""name"": ""addCoffeemaker"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" }, { ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""buyCoffee"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" } ], ""name"": ""getCoffeeMakerProgramDetails"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"", ""value"": """" }, { ""name"": ""price"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerProgramCount"", ""outputs"": [ { ""name"": ""programCount"", ""type"": ""uint8"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerData"", ""outputs"": [ { ""name"": ""owner"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""descriptiveLocation"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""latitude"", ""type"": ""string"" }, { ""name"": ""longitude"", ""type"": ""string"" }, { ""name"": ""machineType"", ""type"": ""uint8"" }, { ""name"": ""machineInfo"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""receiver"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""transferTokens"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""seller"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""sellTokens"", ""outputs"": [ { ""name"": ""soldTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""buyer"", ""type"": ""address"" } ], ""name"": ""buyTokens"", ""outputs"": [ { ""name"": ""receivedTokens"", ""type"": ""uint256"" } ], ""payable"": true, ""stateMutability"": ""payable"", ""type"": ""function"" }, { ""inputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""constructor"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""ExchangeWalletAuthorized"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""CustomerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""owner"", ""type"": ""address"" } ], ""name"": ""CoffeeMakerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""name"", ""type"": ""string"" }, { ""indexed"": false, ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""CoffeeMakerProgramAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""program"", ""type"": ""uint8"" }, { ""indexed"": false, ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""CoffeeBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensSold"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""sender"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""recipient"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensTransfered"", ""type"": ""event"" } ]";
                    target = "0x04A83E168A69E9937990bf1A50b3505202B77929";
                    // var account = Account.LoadFromKeyStore (KeyFile, "testtest");


                    // var web3 = new Nethereum.Web3.Web3 (account, "http://192.168.1.166:30304");
                    var contract = web3.Eth.GetContract (abi, ContractAddress);
                    var buyTokens = contract.GetFunction ("buyTokens");
                    // public class tar {
                    //     public string buyer;
                    //     public uint amount;
                    // }
                    // var rec = new tar;
                    // tar.buyer =  Console.ReadLine ();
                    // tar.amount = amount;
                    var transactionHash = await buyTokens.SendTransactionAsync("0x04A83E168A69E9937990bf1A50b3505202B77929", amount);

                    break;
                case "7":
                    //TRANS Sell Tokens

                    break;
                case "8":
                    //TRANS Transfere Tokens

                    break;
                case "9":
                    //TRANS  Buy Coffee

                    break;
                case "10":
                    //CALL  Display Customer Data

                    break;
                case "11":
                    //CALL  Display Coffeemaker Data

                    break;
                case "12":
                    //CALL Count Coffeemaker Programs

                    break;
                case "13":
                    //CALL Get Programm Details

                    break;
            }

            Console.ReadKey ();
        }

    }
    class Service {
        private readonly Web3 web3;
        private static string abi = @"[ { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCustomerData"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""addAuthorizedExchangeWallet"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""addCoffeeMakerProgram"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""kill"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""name"": ""addCustomer"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getTokens"", ""outputs"": [ { ""name"": ""tokenss"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""locDescriptive"", ""type"": ""string"" }, { ""name"": ""locDepartment"", ""type"": ""string"" }, { ""name"": ""locLatitude"", ""type"": ""string"" }, { ""name"": ""locLongitude"", ""type"": ""string"" }, { ""name"": ""infoMachineType"", ""type"": ""uint8"" }, { ""name"": ""infoDescription"", ""type"": ""string"" } ], ""name"": ""addCoffeemaker"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" }, { ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""buyCoffee"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" } ], ""name"": ""getCoffeeMakerProgramDetails"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"", ""value"": """" }, { ""name"": ""price"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerProgramCount"", ""outputs"": [ { ""name"": ""programCount"", ""type"": ""uint8"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerData"", ""outputs"": [ { ""name"": ""owner"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""descriptiveLocation"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""latitude"", ""type"": ""string"" }, { ""name"": ""longitude"", ""type"": ""string"" }, { ""name"": ""machineType"", ""type"": ""uint8"" }, { ""name"": ""machineInfo"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""receiver"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""transferTokens"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""seller"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""sellTokens"", ""outputs"": [ { ""name"": ""soldTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""buyer"", ""type"": ""address"" } ], ""name"": ""buyTokens"", ""outputs"": [ { ""name"": ""receivedTokens"", ""type"": ""uint256"" } ], ""payable"": true, ""stateMutability"": ""payable"", ""type"": ""function"" }, { ""inputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""constructor"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""ExchangeWalletAuthorized"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""CustomerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""owner"", ""type"": ""address"" } ], ""name"": ""CoffeeMakerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""name"", ""type"": ""string"" }, { ""indexed"": false, ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""CoffeeMakerProgramAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""program"", ""type"": ""uint8"" }, { ""indexed"": false, ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""CoffeeBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensSold"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""sender"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""recipient"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensTransfered"", ""type"": ""event"" } ]";
        private Contract contract;

        public Service (Web3 web3, string address) {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract (abi, address);
        }

        public async Task<int> getTokens (string address) {
            return await contract.GetFunction ("getTokens").CallAsync<int> (address);
        }
    }

}