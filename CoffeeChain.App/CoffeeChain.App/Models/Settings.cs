using System.Collections.Generic;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CoffeeChain.App.Models
{
    public class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static Settings Current { get; } = new Settings();

        protected Settings()
        {
        }

        public string ServerIpAddress
        {
            get => AppSettings.GetValueOrDefault(nameof(ServerIpAddress), "http://192.168.1.166:30304");
            set => AppSettings.AddOrUpdateValue(nameof(ServerIpAddress), value);
        }

        public int ChainId
        {
            get => AppSettings.GetValueOrDefault(nameof(ChainId), 242);
            set => AppSettings.AddOrUpdateValue(nameof(ChainId), value);
        }

        public string ContractAddress
        {
            get => AppSettings.GetValueOrDefault(nameof(ContractAddress), "0x3F45D4615A21cB534E1b0173EDBCb0305E41da96");
            set => AppSettings.AddOrUpdateValue(nameof(ContractAddress), value);
        }

        public string PublicWalletAddress
        {
            get => AppSettings.GetValueOrDefault(nameof(PublicWalletAddress), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(PublicWalletAddress), value);
        }

        public string Passphrase
        {
            get => AppSettings.GetValueOrDefault(nameof(Passphrase), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Passphrase), value);
        }

        public bool IsWalletAvailable => AppSettings.Contains(nameof(PublicWalletAddress));

        public List<CoffeeMaker> KnownCoffeeMakers
        {
            get => JsonConvert.DeserializeObject<List<CoffeeMaker>>(AppSettings.GetValueOrDefault(nameof(KnownCoffeeMakers), "[]"));
            set => AppSettings.AddOrUpdateValue(nameof(KnownCoffeeMakers), JsonConvert.SerializeObject(value));
        }
    }
}
