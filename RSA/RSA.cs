using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class RSA
    {
        public long P { get; set; }
        public long Q { get; set; }
        public long E { get; set; }
        public long D { get; set; }
        public long N { get; set; }

        public void Initialize(string p, string q)
        {
            P = Convert.ToInt64(p);
            Q = Convert.ToInt64(q);

            N = P * Q;
            long fi = (P - 1) * (Q - 1);
            E = GetPublicPartKey(fi);
            D = GetPrivatePartKey(fi, E);
        }

        private long GetPrivatePartKey(long fi, long e)
        {
            long d = 0;
            long k = 1;

            while (true)
            {
                d = (k * fi + 1) / e;

                if ((d * e) % fi == 1)
                    return d;

                k++;
            }
        }

        private long GetPublicPartKey(long fi)
        {
            long e = 3;

            while (true)
            {
                if (BigInteger.GreatestCommonDivisor(new BigInteger(e), new BigInteger(fi)) == BigInteger.One)
                    return e;

                e += 2;
            }
        }

        public string[] Encrypt(string text)
        {
            return Encode(text, E, N);
        }

        public string Decrypt(string[] data)
        {
            return Decode(data, D, N);
        }

        private string[] Encode(string text, long e, long n)
        {
            var data = new List<string>();

            BigInteger num;

            foreach (var ch in text)
            {
                int symbol = ch;

                num = BigInteger.ModPow(symbol, e, n);

                data.Add(num.ToString());
            }

            return data.ToArray();
        }

        private string Decode(string[] data, long d, long n)
        {
            var strBuilder = new StringBuilder();

            BigInteger num;

            foreach (var item in data)
            {
                var val = new BigInteger(Convert.ToInt64(item));
                num = BigInteger.ModPow(val, d, n);

                strBuilder.Append((char)num);
            }

            return strBuilder.ToString();
        }
    }
}
