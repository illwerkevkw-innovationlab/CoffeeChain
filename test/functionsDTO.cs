using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts.Managed;
using Xunit;



namespace Nethereum.Tutorials {
    public class FunctionDTO {

        [Fact]
        public async Task ShouldBeAbleToEncodeDecodeComplexInputOutput () {
            private static string KeyFile = @"{""address"":""54585691af6387f8a23eae6f280d2b6a4c9dc586"",""crypto"":{""cipher"":""aes-128-ctr"",""ciphertext"":""4b6379cd740dd45c8498b605bf0942df088bcce42d2c780fced563925fed5c82"",""cipherparams"":{""iv"":""685fe051614a893cad8cdaa3d128d87c""},""kdf"":""scrypt"",""kdfparams"":{""dklen"":32,""n"":262144,""p"":1,""r"":8,""salt"":""7c98b570a0e5807778d01db039a5a3bc88936511e02f4ab28a8c208b5b7c9278""},""mac"":""8cea0b9b4db934276d7b6aceac96a70d4b3c2c0aa1f2bc9d2dcee119fa295729""},""id"":""16657499-d1ef-4342-bb66-3e6b61ac89ad"",""version"":3}";
            private static string abi = @"[ { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCustomerData"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""addAuthorizedExchangeWallet"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""addCoffeeMakerProgram"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""kill"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""telephone"", ""type"": ""string"" }, { ""name"": ""email"", ""type"": ""string"" } ], ""name"": ""addCustomer"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getTokens"", ""outputs"": [ { ""name"": ""tokenss"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""locDescriptive"", ""type"": ""string"" }, { ""name"": ""locDepartment"", ""type"": ""string"" }, { ""name"": ""locLatitude"", ""type"": ""string"" }, { ""name"": ""locLongitude"", ""type"": ""string"" }, { ""name"": ""infoMachineType"", ""type"": ""uint8"" }, { ""name"": ""infoDescription"", ""type"": ""string"" } ], ""name"": ""addCoffeemaker"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" }, { ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""buyCoffee"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""program"", ""type"": ""uint8"" } ], ""name"": ""getCoffeeMakerProgramDetails"", ""outputs"": [ { ""name"": ""name"", ""type"": ""string"", ""value"": """" }, { ""name"": ""price"", ""type"": ""uint256"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerProgramCount"", ""outputs"": [ { ""name"": ""programCount"", ""type"": ""uint8"", ""value"": ""0"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [ { ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""getCoffeeMakerData"", ""outputs"": [ { ""name"": ""owner"", ""type"": ""address"" }, { ""name"": ""name"", ""type"": ""string"" }, { ""name"": ""descriptiveLocation"", ""type"": ""string"" }, { ""name"": ""department"", ""type"": ""string"" }, { ""name"": ""latitude"", ""type"": ""string"" }, { ""name"": ""longitude"", ""type"": ""string"" }, { ""name"": ""machineType"", ""type"": ""uint8"" }, { ""name"": ""machineInfo"", ""type"": ""string"" } ], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""receiver"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""transferTokens"", ""outputs"": [ { ""name"": ""transferedTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""seller"", ""type"": ""address"" }, { ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""sellTokens"", ""outputs"": [ { ""name"": ""soldTokens"", ""type"": ""uint256"" } ], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [ { ""name"": ""buyer"", ""type"": ""address"" } ], ""name"": ""buyTokens"", ""outputs"": [ { ""name"": ""receivedTokens"", ""type"": ""uint256"" } ], ""payable"": true, ""stateMutability"": ""payable"", ""type"": ""function"" }, { ""inputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""constructor"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""ExchangeWalletAuthorized"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" } ], ""name"": ""CustomerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""wallet"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""owner"", ""type"": ""address"" } ], ""name"": ""CoffeeMakerAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""name"", ""type"": ""string"" }, { ""indexed"": false, ""name"": ""price"", ""type"": ""uint256"" } ], ""name"": ""CoffeeMakerProgramAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""coffeeMaker"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""program"", ""type"": ""uint8"" }, { ""indexed"": false, ""name"": ""amount"", ""type"": ""uint8"" } ], ""name"": ""CoffeeBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensBought"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""customer"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensSold"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""name"": ""sender"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""recipient"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""tokens"", ""type"": ""uint256"" } ], ""name"": ""TokensTransfered"", ""type"": ""event"" } ]";
            private static string ContractAddress = @"0x3F45D4615A21cB534E1b0173EDBCb0305E41da96";
            
            
            Console.WriteLine ("Welcome to the CoffeeChain Wallet Application.");
            Console.WriteLine ("==============================================");

            Console.WriteLine ("Bootstrapping Nethereum...");

            Console.WriteLine ("Please enter the wallet password:");
            // var password =  Console.ReadLine();
            var password = "testtest";

            Console.WriteLine ("Please enter a target wallet:");
            // var target = Console.ReadLine();
            var target = "0x04A83E168A69E9937990bf1A50b3505202B77929";

            var account = Account.LoadFromKeyStore (KeyFile, password);

            var web3 = new Geth.Web3Geth (account, "http://192.168.1.166:30304");

            var contract = web3.Eth.GetContract (abi, contractAddress);
             web3.eth.call.contract





        // class FunctionsCTO {

        //     // struct Location {
        //     //     string descriptive;
        //     //     string department;
        //     //     string latitude;
        //     //     string longitude;
        //     // }
        //     [FunctionOutput]
        //     public class Location {
        //         [Parameter ("string", "descriptive", 1)]
        //         public string Beschreibung { get; set; }

        //         [Parameter ("string", "department", 2)]
        //         public string Abteilung { get; set; }

        //         [Parameter ("string", "latitude", 3)]
        //         public string Breitengrad { get; set; }

        //         [Parameter ("string", "longitude", 4)]
        //         public string Längengrad { get; set; }
        //     }




        //     // struct MachineInfo {
        //     //     MachineType machineType;
        //     //     string description;
        //     // }
        //     public class MachineInfo {
        //         [Parameter ("string", "description", 1)]
        //         public string Beschreibung { get; set; }
        //     }




        //     // struct CoffeeProgram {
        //     //     string name;
        //     //     uint price;
        //     // }
        //     public class MachineInfo {
        //         [Parameter ("string", "description", 1)]
        //         public string Beschreibung { get; set; }
        //     }




        //     // struct CoffeeMaker {
        //     //     bool exists;
        //     //     address owner;
        //     //     string name;
        //     //     Location location;
        //     //     MachineInfo machineInfo;
        //     //     mapping (uint8 => CoffeeProgram) programs;
        //     //     uint8 programCounter;
        //     // }
        //     public class Location {
        //         [Parameter ("bool", "exists", 1)]
        //         public string Beschreibung { get; set; }

        //         [Parameter ("address", "owner", 2)]
        //         public string Abteilung { get; set; }

        //         [Parameter ("string", "name", 3)]
        //         public string Breitengrad { get; set; }

        //         [Parameter ("string", "longitude", 3)]
        //         public string Längengrad { get; set; }
        //     }

        //     // struct Customer {
        //     //     bool exists;
        //     //     string name;
        //     //     string department;
        //     //     string telephone;
        //     //     string email;
        //     // }

        // }