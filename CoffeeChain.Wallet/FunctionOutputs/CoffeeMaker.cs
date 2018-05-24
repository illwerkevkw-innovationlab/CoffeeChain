using Nethereum.ABI.FunctionEncoding.Attributes;

namespace CoffeeChain.Wallet.FunctionOutputs
{
    public enum MachineType
    {
        Capsule = 0,
        Pad = 1,
        Filter = 2,
        Pulver = 3,
        FullyAutomatic = 4,
        VendingMachine = 5,
    }

    [FunctionOutput]
    public class CoffeeMaker
    {
        [Parameter("address", "owner", 1)]
        public string OwenerAddress { get; set; }

        [Parameter("string", "name", 2)]
        public string Name { get; set; }

        [Parameter("string", "descriptiveLocation", 3)]
        public string DescriptiveLocation { get; set; }

        [Parameter("string", "department", 4)]
        public string Department { get; set; }

        [Parameter("string", "latitude", 5)]
        public string Latitude { get; set; }

        [Parameter("string", "longitude", 6)]
        public string Longitude { get; set; }

        [Parameter("uint8", "machineType", 7)]
        public int MachineTypeInt { get; set; }

        [Parameter("string", "machineInfo", 8)]
        public string MachineInfo { get; set; };


        // special accessor for machineType to not interfere with ABI
        public MachineType MachineType
        {
            get
            {
                return (MachineType)MachineTypeInt;
            }
            set
            {
                MachineTypeInt = (int)value;
            }
        }
    }
}
