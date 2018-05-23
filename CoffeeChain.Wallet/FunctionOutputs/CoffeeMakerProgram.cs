using Nethereum.ABI.FunctionEncoding.Attributes;

namespace CoffeeChain.Wallet.FunctionOutputs
{
    [FunctionOutput]
    public class CoffeeMakerProgram
    {
        [Parameter("string", "name", 1)]
        public string Name { get; set; }

        [Parameter("uint256", "price", 2)]
        public long Price { get; set; }
    }
}
