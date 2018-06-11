using System;
using CoffeeChain.App.Views;

namespace CoffeeChain.App.Models
{
    public class MainPageMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }

        public MainPageMenuItem()
        {
            TargetType = typeof(DefaultPage);
        }
    }
}
