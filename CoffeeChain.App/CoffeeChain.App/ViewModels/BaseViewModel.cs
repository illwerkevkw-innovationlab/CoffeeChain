using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoffeeChain.App.Services;
using CoffeeChain.Connector;
using Nethereum.RPC.Accounts;
using Nethereum.Web3;

namespace CoffeeChain.App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IAccount _account;
        protected readonly Web3 _web3;
        protected readonly ICoffeeEconomyService _coffeeEconomyService;

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
                OnPropertyChanged(nameof(IsLoaded));
            }
        }
        public bool IsLoaded
        {
            get { return !_isBusy; }
            set
            {
                SetProperty(ref _isBusy, !value);
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public BaseViewModel()
        {
            _account = BlockchainServicesFactory.BuildAccount();
            _web3 = BlockchainServicesFactory.BuildWeb3(_account);
            _coffeeEconomyService = BlockchainServicesFactory.BuildCoffeeEconomyService(_account, _web3);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
