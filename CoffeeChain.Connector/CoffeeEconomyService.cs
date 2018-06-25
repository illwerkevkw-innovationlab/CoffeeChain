using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using CoffeeChain.Connector.FunctionOutputs;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Accounts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace CoffeeChain.Connector
{
    public class CoffeeEconomyService : ICoffeeEconomyService
    {
        private static HexBigInteger DefaultGasToUse = new HexBigInteger(2000000);
        private static HexBigInteger DefaultGasPrice = new HexBigInteger(Nethereum.Util.UnitConversion.Convert.ToWei(20, Nethereum.Util.UnitConversion.EthUnit.Gwei));

        private readonly IAccount _account;
        private readonly Web3 _web3;
        private readonly Contract _contract;

        public CoffeeEconomyService(IAccount account, Web3 web3, string address)
        {
            _account = account;
            _web3 = web3;

            //var abi = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "abi.json"));
            var abi = ABI;
            _contract = web3.Eth.GetContract(abi, address);
        }

        public async Task<string> AddAuthorizedExchangeWalletAsync(string address)
        {
            var addAuthorizedExchangeWallet = _contract.GetFunction("addAuthorizedExchangeWallet");
            return await addAuthorizedExchangeWallet.SendTransactionAsync(_account.Address, DefaultGasToUse, DefaultGasPrice, null, address);
        }

        public async Task<string> AddCustomerAsync(string address, string name, string department, string telephone, string email)
        {
            var addCustomer = _contract.GetFunction("addCustomer");
            return await addCustomer.SendTransactionAsync(_account.Address, DefaultGasToUse, DefaultGasPrice, null, address,
                name, department, telephone, email);
        }

        public async Task<string> AddCoffeemakerAsync(string target, string name, string locDescriptive, string locDepartment,
            string locLatitude, string locLongitude, int infoMachineType, string infoDescription)
        {
            var addCoffeemaker = _contract.GetFunction("addCoffeemaker");
            return await addCoffeemaker.SendTransactionAsync(_account.Address, DefaultGasToUse, DefaultGasPrice, null, target,
                name, locDescriptive, locDepartment, locLatitude, locLongitude, infoMachineType, infoDescription);
        }

        public async Task<string> AddCoffeemakerPogramAsync(string target, string name, int price)
        {
            var addcoffeemakerprogramm = _contract.GetFunction("addCoffeeMakerProgram");
            return await addcoffeemakerprogramm.SendTransactionAsync(_account.Address, DefaultGasToUse, DefaultGasPrice,
                null, target, name, price);
        }

        public async Task<string> BuyTokensAsync(string forAddress, BigInteger amount)
        {
            var buyTokens = _contract.GetFunction("buyTokens");
            return await buyTokens.SendTransactionAsync(_account.Address, DefaultGasToUse, DefaultGasPrice, new HexBigInteger(amount), forAddress);
        }

        public async Task<string> SellTokensAsync(string seller, int tokens)
        {
            var selltokens = _contract.GetFunction("sellTokens");
            return await selltokens.SendTransactionAsync(_account.Address, DefaultGasToUse, DefaultGasPrice, null, seller, tokens);
        }

        public async Task<string> TransfareTokensAsync(string receiver, int tokens)
        {
            var transtokens = _contract.GetFunction("transferTokens");
            return await transtokens.SendTransactionAsync(_account.Address, DefaultGasToUse, DefaultGasPrice, null, receiver, tokens);
        }

        public async Task<string> BuyCoffeeAsync(string coffeeMaker, int program, int amount)
        {
            var BuyCoffee = _contract.GetFunction("buyCoffee");
            return await BuyCoffee.SendTransactionAsync(_account.Address, DefaultGasToUse, DefaultGasPrice, null, coffeeMaker, program, amount);
        }

        public async Task<int> GetTokensAsync(string wallet)
        {
            return await _contract.GetFunction("getTokens").CallAsync<int>(wallet);
        }

        public async Task<bool> IsCustomerAsync(string wallet)
        {
            return await _contract.GetFunction("isCustomer").CallAsync<bool>(wallet);
        }

        public async Task<bool> IsCoffeeMakerAsync(string wallet)
        {
            return await _contract.GetFunction("isCoffeeMaker").CallAsync<bool>(wallet);
        }

        public async Task<bool> IsAuthorizedExchangeWalletAsync(string wallet)
        {
            return await _contract.GetFunction("isAuthorizedExchangeWallet").CallAsync<bool>(wallet);
        }

        public async Task<Customer> GetCustomerDataAsync(string wallet)
        {
            return await _contract.GetFunction("getCustomerData").CallDeserializingToObjectAsync<Customer>(wallet);
        }

        public async Task<CoffeeMaker> GetCoffeeMakerDataAsync(string wallet)
        {
            return await _contract.GetFunction("getCoffeeMakerData").CallDeserializingToObjectAsync<CoffeeMaker>(wallet);
        }

        public async Task<CoffeeMakerProgram> GetCoffeeMakerProgramDetailsAsync(string wallet, int program)
        {
            return await _contract.GetFunction("getCoffeeMakerProgramDetails").CallDeserializingToObjectAsync<CoffeeMakerProgram>(wallet, program);
        }

        public async Task<int> GetCoffeeMakerProgramCountAsync(string wallet)
        {
            return await _contract.GetFunction("getCoffeeMakerProgramCount").CallAsync<int>(wallet);
        }

        public async Task<IList<EventLog<CoffeeBoughtEvent>>> GetCoffeeBoughtEventsForWallet(string wallet)
        {
            var eventLog = _contract.GetEvent("CoffeeBought");
            var filterInput = await eventLog.CreateFilterAsync(new object[] { wallet }, BlockParameter.CreateEarliest(), BlockParameter.CreateLatest());
            return await eventLog.GetFilterChanges<CoffeeBoughtEvent>(filterInput);
        }

        private const string ABI = @"[{
    ""constant"": false,
    ""inputs"": [],
    ""name"": ""CoffeeEconomy"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": true,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""getCustomerData"",
    ""outputs"": [{
        ""name"": ""name"",
        ""type"": ""string""
    },
    {
        ""name"": ""department"",
        ""type"": ""string""
    },
    {
        ""name"": ""telephone"",
        ""type"": ""string""
    },
    {
        ""name"": ""email"",
        ""type"": ""string""
    }],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""addAuthorizedExchangeWallet"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": true,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""isCustomer"",
    ""outputs"": [{
        ""name"": ""trueOrFalse"",
        ""type"": ""bool"",
        ""value"": false
    }],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""coffeeMaker"",
        ""type"": ""address""
    },
    {
        ""name"": ""name"",
        ""type"": ""string""
    },
    {
        ""name"": ""price"",
        ""type"": ""uint256""
    }],
    ""name"": ""addCoffeeMakerProgram"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [],
    ""name"": ""kill"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    },
    {
        ""name"": ""name"",
        ""type"": ""string""
    },
    {
        ""name"": ""department"",
        ""type"": ""string""
    },
    {
        ""name"": ""telephone"",
        ""type"": ""string""
    },
    {
        ""name"": ""email"",
        ""type"": ""string""
    }],
    ""name"": ""addCustomer"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": true,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""getTokens"",
    ""outputs"": [{
        ""name"": ""tokens"",
        ""type"": ""uint256"",
        ""value"": ""0""
    }],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    },
    {
        ""name"": ""name"",
        ""type"": ""string""
    },
    {
        ""name"": ""locDescriptive"",
        ""type"": ""string""
    },
    {
        ""name"": ""locDepartment"",
        ""type"": ""string""
    },
    {
        ""name"": ""locLatitude"",
        ""type"": ""string""
    },
    {
        ""name"": ""locLongitude"",
        ""type"": ""string""
    },
    {
        ""name"": ""infoMachineType"",
        ""type"": ""uint8""
    },
    {
        ""name"": ""infoDescription"",
        ""type"": ""string""
    }],
    ""name"": ""addCoffeemaker"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""coffeeMaker"",
        ""type"": ""address""
    },
    {
        ""name"": ""program"",
        ""type"": ""uint8""
    },
    {
        ""name"": ""amount"",
        ""type"": ""uint8""
    }],
    ""name"": ""buyCoffee"",
    ""outputs"": [{
        ""name"": ""transferedTokens"",
        ""type"": ""uint256""
    }],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": true,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    },
    {
        ""name"": ""program"",
        ""type"": ""uint8""
    }],
    ""name"": ""getCoffeeMakerProgramDetails"",
    ""outputs"": [{
        ""name"": ""name"",
        ""type"": ""string"",
        ""value"": """"
    },
    {
        ""name"": ""price"",
        ""type"": ""uint256"",
        ""value"": ""0""
    }],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
},
{
    ""constant"": true,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""getCoffeeMakerProgramCount"",
    ""outputs"": [{
        ""name"": ""programCount"",
        ""type"": ""uint8"",
        ""value"": ""0""
    }],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
},
{
    ""constant"": true,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""isAuthorizedExchangeWallet"",
    ""outputs"": [{
        ""name"": ""trueOrFalse"",
        ""type"": ""bool"",
        ""value"": false
    }],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
},
{
    ""constant"": true,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""getCoffeeMakerData"",
    ""outputs"": [{
        ""name"": ""owner"",
        ""type"": ""address""
    },
    {
        ""name"": ""name"",
        ""type"": ""string""
    },
    {
        ""name"": ""descriptiveLocation"",
        ""type"": ""string""
    },
    {
        ""name"": ""department"",
        ""type"": ""string""
    },
    {
        ""name"": ""latitude"",
        ""type"": ""string""
    },
    {
        ""name"": ""longitude"",
        ""type"": ""string""
    },
    {
        ""name"": ""machineType"",
        ""type"": ""uint8""
    },
    {
        ""name"": ""description"",
        ""type"": ""string""
    }],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""receiver"",
        ""type"": ""address""
    },
    {
        ""name"": ""tokens"",
        ""type"": ""uint256""
    }],
    ""name"": ""transferTokens"",
    ""outputs"": [{
        ""name"": ""transferedTokens"",
        ""type"": ""uint256""
    }],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""seller"",
        ""type"": ""address""
    },
    {
        ""name"": ""tokens"",
        ""type"": ""uint256""
    }],
    ""name"": ""sellTokens"",
    ""outputs"": [{
        ""name"": ""soldTokens"",
        ""type"": ""uint256""
    }],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""constant"": true,
    ""inputs"": [{
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""isCoffeeMaker"",
    ""outputs"": [{
        ""name"": ""trueOrFalse"",
        ""type"": ""bool"",
        ""value"": false
    }],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""buyer"",
        ""type"": ""address""
    }],
    ""name"": ""buyTokens"",
    ""outputs"": [{
        ""name"": ""receivedTokens"",
        ""type"": ""uint256""
    }],
    ""payable"": true,
    ""stateMutability"": ""payable"",
    ""type"": ""function""
},
{
    ""constant"": false,
    ""inputs"": [{
        ""name"": ""newOwner"",
        ""type"": ""address""
    }],
    ""name"": ""transferOwnership"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""ExchangeWalletAuthorized"",
    ""type"": ""event""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""wallet"",
        ""type"": ""address""
    }],
    ""name"": ""CustomerAdded"",
    ""type"": ""event""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""wallet"",
        ""type"": ""address""
    },
    {
        ""indexed"": true,
        ""name"": ""owner"",
        ""type"": ""address""
    }],
    ""name"": ""CoffeeMakerAdded"",
    ""type"": ""event""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""coffeeMaker"",
        ""type"": ""address""
    },
    {
        ""indexed"": true,
        ""name"": ""name"",
        ""type"": ""string""
    },
    {
        ""indexed"": true,
        ""name"": ""price"",
        ""type"": ""uint256""
    }],
    ""name"": ""CoffeeMakerProgramAdded"",
    ""type"": ""event""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""coffeeMaker"",
        ""type"": ""address""
    },
    {
        ""indexed"": true,
        ""name"": ""program"",
        ""type"": ""uint8""
    },
    {
        ""indexed"": true,
        ""name"": ""amount"",
        ""type"": ""uint8""
    }],
    ""name"": ""CoffeeBought"",
    ""type"": ""event""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""customer"",
        ""type"": ""address""
    },
    {
        ""indexed"": true,
        ""name"": ""tokens"",
        ""type"": ""uint256""
    }],
    ""name"": ""TokensBought"",
    ""type"": ""event""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""customer"",
        ""type"": ""address""
    },
    {
        ""indexed"": true,
        ""name"": ""tokens"",
        ""type"": ""uint256""
    }],
    ""name"": ""TokensSold"",
    ""type"": ""event""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""sender"",
        ""type"": ""address""
    },
    {
        ""indexed"": true,
        ""name"": ""recipient"",
        ""type"": ""address""
    },
    {
        ""indexed"": true,
        ""name"": ""tokens"",
        ""type"": ""uint256""
    }],
    ""name"": ""TokensTransfered"",
    ""type"": ""event""
},
{
    ""anonymous"": false,
    ""inputs"": [{
        ""indexed"": true,
        ""name"": ""oldOwner"",
        ""type"": ""address""
    },
    {
        ""indexed"": true,
        ""name"": ""newOwner"",
        ""type"": ""address""
    }],
    ""name"": ""ContractOwnershipChanged"",
    ""type"": ""event""
}]";
    }
}