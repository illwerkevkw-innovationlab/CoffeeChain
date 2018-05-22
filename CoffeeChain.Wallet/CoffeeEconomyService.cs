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

        public async Task<string> AddCustomer(string address, string name, string department, string telephone, string email)
        {
            var buyTokens = _contract.GetFunction("addCustomer");

            // SendTransactionAsync schema is: function.SendTransactionAsync(senderAddress, gas, value, ... additionalParameters);
            return await buyTokens.SendTransactionAsync(_account.Address, new HexBigInteger(90000000), null, address, name, department, telephone, email);
        }

        public async Task<string> BuyTokens(string forAddress, BigInteger amount)
        {
            var buyTokens = _contract.GetFunction("buyTokens");

            // SendTransactionAsync schema is: function.SendTransactionAsync(senderAddress, gas, value, ... additionalParameters);
            return await buyTokens.SendTransactionAsync(_account.Address, new HexBigInteger(90000000), new HexBigInteger(amount), forAddress);
        }

        public async Task<int> GetTokens(string address)
        {
            return await _contract.GetFunction("getTokens").CallAsync<int>(address);
        }
    }
}
