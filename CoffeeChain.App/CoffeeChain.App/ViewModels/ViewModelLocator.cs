using CommonServiceLocator;

namespace CoffeeChain.App.ViewModels
{
    public class ViewModelLocator
    {
        public AccountViewModel BalanceViewModel => ServiceLocator.Current.GetInstance<AccountViewModel>();
    }
}
