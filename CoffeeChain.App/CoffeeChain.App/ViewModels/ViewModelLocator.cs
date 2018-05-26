using CommonServiceLocator;

namespace CoffeeChain.App.ViewModels
{
    public class ViewModelLocator
    {
        public BalanceViewModel BalanceViewModel => ServiceLocator.Current.GetInstance<BalanceViewModel>();
    }
}
