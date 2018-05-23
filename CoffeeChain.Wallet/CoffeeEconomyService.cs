using System;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace CoffeeChain.Wallet
{
    public class CoffeeEconomyService
    {
        private static int DefaultGasToUse = 90000000;

        private readonly Account _account;
        private readonly Web3 _web3;
        private readonly Contract _contract;

        public CoffeeEconomyService(Account account, Web3 web3, string address)
        {
            _account = account;
            _web3 = web3;

            var abi = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "abi.json"));
            _contract = web3.Eth.GetContract(abi, address);
        }

        // 1
        public async Task<int> GetTokens(string address)
        {
            return await _contract.GetFunction("getTokens").CallAsync<int>(address);
        }

        //2
        public async Task<string> AddAuthorizedExchangeWallet (string address)
        {
            var addAuthorizedExchangeWallet = _contract.GetFunction("addAuthorizedExchangeWallet");
            return await addAuthorizedExchangeWallet.SendTransactionAsync(_account.Address, new HexBigInteger(140000), new HexBigInteger(144000000000000000), null, address);
        }

        //3
        public async Task<string> AddCustomer(string address, string name, string department, string telephone, string email)
        {
            var addCustomer = _contract.GetFunction("addCustomer");
            // SendTransactionAsync schema is: function.SendTransactionAsync(senderAddress, gas, value, ... additionalParameters);
            return await addCustomer.SendTransactionAsync(_account.Address, new HexBigInteger(140000), new HexBigInteger(144000000000000000), null, address, name, department, telephone, email);
        }

        //4
        public async Task<string> AddCoffeemaker(string target, string name, string locDescriptive, string locDepartment, string locLatitude, string locLongitude, int infoMachineType, string infoDescription)
        {
            var addCoffeemaker = _contract.GetFunction("addCoffeemaker");
            return await addCoffeemaker.SendTransactionAsync(_account.Address, new HexBigInteger(140000), new HexBigInteger(144000000000000000), null, target, name, locDescriptive, locDepartment, locLatitude, locLongitude, infoMachineType, infoDescription);

        }

        //5
        public async Task<string> AddCoffeemakerPogram(string target, string name, int price)
        {
            var addcoffeemakerprogramm = _contract.GetFunction("addCoffeeMakerProgram");
            return await addcoffeemakerprogramm.SendTransactionAsync(_account.Address, new HexBigInteger(140000), new HexBigInteger(144000000000000000), null, target, name, price);
        }

        //6
        public async Task<string> BuyTokens(string forAddress, BigInteger amount)
        {
            var buyTokens = _contract.GetFunction("buyTokens");

            // SendTransactionAsync schema is: function.SendTransactionAsync(senderAddress, gas, gas_price, value, ... additionalParameters);
            return await buyTokens.SendTransactionAsync(_account.Address, new HexBigInteger(140000), new HexBigInteger(144000000000000000), new HexBigInteger(amount), forAddress);
        }

        //7
        public async Task<string> SellTokens(string seller, int tokens)
        {
            var selltokens = _contract.GetFunction("sellTokens");
            return await selltokens.SendTransactionAsync(_account.Address, new HexBigInteger(140000), new HexBigInteger(144000000000000000), null, seller, tokens);
        }

        //8
        public async Task<string> TransfareTokens(string receiver, int tokens)
        {
            var transtokens = _contract.GetFunction("transferTokens");
            return await transtokens.SendTransactionAsync(_account.Address, new HexBigInteger(140000), new HexBigInteger(144000000000000000), null, receiver, tokens);
        }

        //9
        
        
        
    }
}
