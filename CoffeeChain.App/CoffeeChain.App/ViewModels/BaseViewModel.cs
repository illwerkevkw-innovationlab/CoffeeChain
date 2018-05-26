using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoffeeChain.Connector;
using Xamarin.Forms;

namespace CoffeeChain.App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ICoffeeEconomyService CoffeeEconomyService => DependencyService.Get<ICoffeeEconomyService>();

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
