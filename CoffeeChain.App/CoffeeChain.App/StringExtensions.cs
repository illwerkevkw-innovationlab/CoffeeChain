namespace CoffeeChain.App
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            if (s == null || s == "")
            {
                return true;
            }

            return false;
        }
    }
}
