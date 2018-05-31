using Nethereum.ABI.FunctionEncoding.Attributes;

namespace CoffeeChain.Connector.FunctionOutputs
{
    public class CoffeeBoughtEvent
    {
        [Parameter("address", "coffeeMaker", 1)]
        public string CoffeeMaker { get; set; }

        [Parameter("uint8", "program", 2)]
        public int Program { get; set; }

        [Parameter("uint8", "amount", 3)]
        public int Amount { get; set; }
    }
}
