using System;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace CoffeeChain.Wallet {
    public class CoffeeEconomyService {
        private static int DefaultGasToUse = 90000000;

        private readonly Account _account;
        private readonly Web3 _web3;
        private readonly Contract _contract;

        public CoffeeEconomyService (Account account, Web3 web3, string address) {
            _account = account;
            _web3 = web3;

            var abi = File.ReadAllText (Path.Combine (Environment.CurrentDirectory, "abi.json"));
            _contract = web3.Eth.GetContract (abi, address);
        }

        // 1
        public async Task<int> GetTokens (string address) {
            return await _contract.GetFunction ("getTokens").CallAsync<int> (address);
        }

        //2
        public async Task<string> AddAuthorizedExchangeWallet (string address) {
            var addAuthorizedExchangeWallet = _contract.GetFunction ("addAuthorizedExchangeWallet");
            return await addAuthorizedExchangeWallet.SendTransactionAsync (_account.Address, new HexBigInteger (140000), new HexBigInteger (144000000000000000), null, address);
        }

        //3
        public async Task<string> AddCustomer (string address, string name, string department, string telephone, string email) {
            var addCustomer = _contract.GetFunction ("addCustomer");
            // SendTransactionAsync schema is: function.SendTransactionAsync(senderAddress, gas, value, ... additionalParameters);
            return await addCustomer.SendTransactionAsync (_account.Address, new HexBigInteger (140000), new HexBigInteger (144000000000000000), null, address, name, department, telephone, email);
        }

        //4
        public async Task<string> AddCoffeemaker (string target, string name, string locDescriptive, string locDepartment, string locLatitude, string locLongitude, int infoMachineType, string infoDescription) {
            var addCoffeemaker = _contract.GetFunction ("addCoffeemaker");
            return await addCoffeemaker.SendTransactionAsync (_account.Address, new HexBigInteger (140000), new HexBigInteger (144000000000000000), null, target, name, locDescriptive, locDepartment, locLatitude, locLongitude, infoMachineType, infoDescription);

        }

        //5
        public async Task<string> AddCoffeemakerPogram (string target, string name, int price) {
            var addcoffeemakerprogramm = _contract.GetFunction ("addCoffeeMakerProgram");
            return await addcoffeemakerprogramm.SendTransactionAsync (_account.Address, new HexBigInteger (140000), new HexBigInteger (144000000000000000), null, target, name, price);
        }

        //6
        public async Task<string> BuyTokens (string forAddress, BigInteger amount) {
            var buyTokens = _contract.GetFunction ("buyTokens");

            // SendTransactionAsync schema is: function.SendTransactionAsync(senderAddress, gas, gas_price, value, ... additionalParameters);
            return await buyTokens.SendTransactionAsync (_account.Address, new HexBigInteger (140000), new HexBigInteger (144000000000000000), new HexBigInteger (amount), forAddress);
        }

        //7
        public async Task<string> SellTokens (string seller, int tokens) {
            var selltokens = _contract.GetFunction ("sellTokens");
            return await selltokens.SendTransactionAsync (_account.Address, new HexBigInteger (140000), new HexBigInteger (144000000000000000), null, seller, tokens);
        }

        //8
        public async Task<string> TransfareTokens (string receiver, int tokens) {
            var transtokens = _contract.GetFunction ("transferTokens");
            return await transtokens.SendTransactionAsync (_account.Address, new HexBigInteger (140000), new HexBigInteger (144000000000000000), null, receiver, tokens);
        }

        //9
        public async Task<string> BuyCoffee (string coffeeMaker, int program, int amount) {
            var BuyCoffee = _contract.GetFunction ("buyCoffee");
            return await BuyCoffee.SendTransactionAsync (_account.Address, new HexBigInteger (140000), new HexBigInteger (144000000000000000), null, coffeeMaker, program, amount);
        }

        //10
        [FunctionOutput]
        public class Customer {
            // [Parameter ("bool", "exists", 1)]
            // public string Exists { get; set; }

            [Parameter ("string", "name", 1)]
            public string Name { get; set; }

            [Parameter ("string", "department", 2)]
            public string Department  { get; set; }

            [Parameter ("string", "telephone", 3)]
            public string Telephone { get; set; }

            [Parameter ("string", "email", 4)]
            public string Email { get; set; }
        }
        public async Task<Customer> DisplayCustomerData (string wallet) 
        {  
            return await _contract.GetFunction ("getCustomerData").CallDeserializingToObjectAsync<Customer>(wallet);
            // return ret_data(data);
        }

        //11 
        // [FunctionOutput]
        // public class MachineType {
        //     [Parameter ("string", "Capsules", 1)]
        //     public string Capsules { get; set; }

        //     [Parameter ("string", "Pads", 2)]
        //     public string Pads { get; set; }

        //     [Parameter ("string", "Filter", 3)]
        //     public string Filter  { get; set; }

        //     [Parameter ("string", "Pulver", 4)]
        //     public string Pulver { get; set; }

        //     [Parameter ("string", "FullyAutomatic", 5)]
        //     public string FullyAutomatic { get; set; }

        //     [Parameter ("string", "VendingMachine", 6)]
        //     public string VendingMachine { get; set; }
        // }
        //[FunctionOutput]
        // enum MachineType { Capsules, Pads, Filter, Pulver, FullyAutomatic, VendingMachine }
        [FunctionOutput]
        public class CoffeeMaker {
            [Parameter ("address", "owner", 1)]
            public string OwenerAddress { get; set; }

            [Parameter ("string", "name", 2)]
            public string Name { get; set; }

            [Parameter ("string", "descriptiveLocation", 3)]
            public string DescriptiveLocation  { get; set; }

            [Parameter ("string", "department", 4)]
            public string Department { get; set; }

            [Parameter ("string", "latitude", 5)]
            public string Latitude { get; set; }

            [Parameter ("string", "longitude", 6)]
            public string Longitude  { get; set; }

            [Parameter ("string", "machineType", 7)]
            public string MachineType { get; set; }

            [Parameter ("string", "machineInfo", 8)]
            public string MachineInfo { get; set; }
        }
        public async Task<CoffeeMaker> DisplayCoffeeMakerData (string wallet) 
        {  
            return await _contract.GetFunction ("getCoffeeMakerData").CallDeserializingToObjectAsync<CoffeeMaker>(wallet);
            // return ret_data(data);
        }



        //12
        public async Task<int> CountPrograms (string wallet)
        {
            return await _contract.GetFunction("getCoffeeMakerProgramCount").CallAsync<int>(wallet);
        }

    }
}