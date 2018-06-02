namespace CoffeeChain.App.Models
{
    public class CoffeeMaker : Connector.FunctionOutputs.CoffeeMaker
    {
        public string Address { get; set; }

        public string MachineTypeName => MachineType.GetDescription();
    }
}
