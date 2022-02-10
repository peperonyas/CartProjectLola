using Newtonsoft.Json.Linq;

namespace Cart.Core.Utilities
{
    public static class Shared
    {
        public static bool ValidateJSON(this string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
