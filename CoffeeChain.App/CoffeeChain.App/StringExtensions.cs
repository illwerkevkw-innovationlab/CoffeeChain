using System.Text.RegularExpressions;
using Nethereum.Util;

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

        public static bool IsValidEthereumAddress(this string address)
        {
            Regex r = new Regex("^(0x){1}[0-9a-fA-F]{40}$");
            // Does not match length, prefix or hex
            if (!r.IsMatch(address))
            {
                return false;
            }
            // It is all lowercase, so no checksum needed
            else if (address == address.ToLower())
            {
                return true;
            }
            // Do checksum
            else
            {
                return new AddressUtil().IsChecksumAddress(address);
            }
        }
    }
}
