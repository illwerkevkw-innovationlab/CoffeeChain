namespace CoffeeChain.App.Models
{
    public class Customer : Connector.FunctionOutputs.Customer
    {
        public string Wallet { get; set; }
        public long CoffeeTokens { get; set; }
    }
}
