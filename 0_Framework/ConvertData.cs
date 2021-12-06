using System.Text.RegularExpressions;

namespace _0_Framework
{
    public static class ConvertData
    {
        public static int ConvertStringToInt(string message)
        {
            var r = new Regex("^[0-9]*$");
            return r.IsMatch(message) ? Int32.Parse(message) : 0;
        }

    }
}