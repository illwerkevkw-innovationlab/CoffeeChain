﻿using Plugin.Settings;
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
            get => AppSettings.GetValueOrDefault(nameof(PublicWalletAddress), "0x54585691af6387f8a23eae6f280d2b6a4c9dc586");
            set => AppSettings.AddOrUpdateValue(nameof(PublicWalletAddress), value);
        }

        public string PrivateWalletKey
        {
            get => AppSettings.GetValueOrDefault(nameof(PrivateWalletKey), "ba303798ccf5db5b474d133397fe45fd665a46358720163dfd48bc73d219adc6");
            set => AppSettings.AddOrUpdateValue(nameof(PrivateWalletKey), value);
        }
    }
}
