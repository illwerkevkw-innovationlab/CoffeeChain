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
            get => AppSettings.GetValueOrDefault(nameof(ServerIpAddress), "http://192.168.10.1:30304");
            set => AppSettings.AddOrUpdateValue(nameof(ServerIpAddress), value);
        }

        public int ChainId
        {
            get => AppSettings.GetValueOrDefault(nameof(ChainId), 42);
            set => AppSettings.AddOrUpdateValue(nameof(ChainId), value);
        }

        public string ContractAddress
        {
            get => AppSettings.GetValueOrDefault(nameof(ContractAddress), "0x6f7A0Eb8303CDD57597A59AD79D17aEd39f01fCb");
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
