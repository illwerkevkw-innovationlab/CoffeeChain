using System.Numerics;
using System.Threading.Tasks;
using CoffeeChain.Connector.FunctionOutputs;

namespace CoffeeChain.Connector
{
    public interface ICoffeeEconomyService
    {
        Task<string> AddAuthorizedExchangeWalletAsync(string address);

        Task<string> AddCustomerAsync(string address, string name, string department, string telephone, string email);

        Task<string> AddCoffeemakerAsync(string target, string name, string locDescriptive, string locDepartment,
            string locLatitude, string locLongitude, int infoMachineType, string infoDescription);

        Task<string> AddCoffeemakerPogramAsync(string target, string name, int price);

        Task<string> BuyTokensAsync(string forAddress, BigInteger amount);

        Task<string> SellTokensAsync(string seller, int tokens);

        Task<string> TransfareTokensAsync(string receiver, int tokens);

        Task<string> BuyCoffeeAsync(string coffeeMaker, int program, int amount);

        Task<int> GetTokensAsync(string address);

        Task<Customer> GetCustomerDataAsync(string wallet);

        Task<CoffeeMaker> GetCoffeeMakerDataAsync(string wallet);

        Task<CoffeeMakerProgram> GetCoffeeMakerProgramDetailsAsync(string wallet, int program);

        Task<int> GetCoffeeMakerProgramCountAsync(string wallet);
    }
}
