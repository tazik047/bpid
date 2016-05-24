using System;
using System.Linq;
using BLL.Abstract;
using BLL.Models;

namespace BLL.Concrete
{
    public class KeyService : IKeyService
    {
        public string GenerateSimpleKey()
        {
            return new string(Guid.NewGuid().ToString().Where(char.IsLetterOrDigit).ToArray());
        }

        public RsaKeys GenerateRsaKey()
        {
            long publicKey;
            long secreteKey;
            var module = GenKey(out publicKey, out secreteKey);

            return new RsaKeys
            {
                PublicKey = publicKey,
                SecreteKey = secreteKey,
                Module = module
            };
        }

        private long GetPrimeNumber()
        {
            var n = (new Random().Next(3, 1000)) | 1;

            for (long i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                {
                    n += 2;
                    i = 1;
                }
            }

            return n;
        }

        private long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        private long GenKey(out long publicKey, out long privateKey)
        {

            var p = GetPrimeNumber();
            var q = GetPrimeNumber();
            while (q == p)
            {
                q = GetPrimeNumber();
            }
            var n = p * q;// "модуль" n
            var f = (p - 1) * (q - 1);// функция Эйлера	 
            for (privateKey = 3; GCD(privateKey, f) != 1; privateKey++) { } // ключ для шифрования (секретный ключ d и n). взаимно простой с f
			for (publicKey = 3; (publicKey * privateKey) % f != 1; publicKey += 2) { } // ключ для расшифровки (открытый ключ e и n)

			return n;
        }
    }
}
