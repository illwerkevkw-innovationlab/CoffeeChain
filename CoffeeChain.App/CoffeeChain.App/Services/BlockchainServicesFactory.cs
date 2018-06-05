using CoffeeChain.App.Models;
using CoffeeChain.Connector;
using Nethereum.RPC.Accounts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts.Managed;

namespace CoffeeChain.App.Services
{
    public static class BlockchainServicesFactory
    {
        public static IAccount BuildAccount()
        {
            return new ManagedAccount(Settings.Current.PublicWalletAddress, Settings.Current.Passphrase);
        }

        public static Web3 BuildWeb3(IAccount account)
        {
            return new Web3(account, Settings.Current.ServerIpAddress);
        }

        public static ICoffeeEconomyService BuildCoffeeEconomyService(IAccount account, Web3 web3)
        {
            return new CoffeeEconomyService(account, web3, Settings.Current.ContractAddress);
        }
    }
}
