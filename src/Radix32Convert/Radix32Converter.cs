using System.Numerics;
using System.Text;
using System.Linq;
using System.Collections.Generic;
namespace Radix32Convert
{
    public static class Radix32Converter
    {
        const string radixstr = "0123456789abcdefghijklmnopqrstuvwxyz";
        static readonly char[] radixchar = radixstr.ToCharArray();
        static readonly Dictionary<char, int> radixList = radixstr.Select((x, i) => new { x, i }).ToDictionary(x => x.x, x => x.i);
        public static string ConvertToString(BigInteger n)
        {
            if (n.IsZero)
            {
                return "0";
            }
            var sb = new StringBuilder();
            bool isNegative = n.Sign < 0;
            if(n.Sign < 0)
            {
                n = BigInteger.Abs(n);
            }
            while (true)
            {
                if (n.IsZero || n == BigInteger.MinusOne)
                {
                    break;
                }
                var i = (int)((n) & 0x1f);
                sb.Append(radixchar[i]);
                n = n >> 5;
            }
            if (isNegative)
            {
                return '-' + string.Join("", sb.ToString().ToCharArray().Reverse().ToArray());
            }
            else
            {
                return string.Join("", sb.ToString().ToCharArray().Reverse().ToArray());
            }
        }
        public static BigInteger ConvertToInteger(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return new BigInteger(0);
            }
            BigInteger ret = new BigInteger();
            s = s.Trim();
            bool isNegative = false;
            if (s[0] == '-')
            {
                isNegative = true;
            }
            foreach (var c in s.ToCharArray())
            {
                if (c == '-')
                {
                    continue;
                }
                ret = ret << 5;
                ret = ret + radixList[c];
            }
            return isNegative ? ret * BigInteger.MinusOne : ret;
        }
    }
}