using Nethereum.ABI.FunctionEncoding.Attributes;

namespace CoffeeChain.Wallet.FunctionOutputs
{
    [FunctionOutput]
    public class Customer
    {
        [Parameter("string", "name", 1)]
        public string Name { get; set; }

        [Parameter("string", "department", 2)]
        public string Department { get; set; }

        [Parameter("string", "telephone", 3)]
        public string Telephone { get; set; }

        [Parameter("string", "email", 4)]
        public string Email { get; set; }
    }
}
