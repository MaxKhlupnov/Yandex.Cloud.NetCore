using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Yandex.Cloud.NetCore.Sample.Common.Security
{
    public class AuthCrypto
    {
        internal const string InnerSalt = "по-пу-ти";

        public static string GenerateSha256String(string safeCode)
        {
            var inputString = safeCode + "#" + InnerSalt;
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha256.ComputeHash(bytes);

            return GetStringFromHash(hash);
        }

        public static string GetConnectionToken()
        {
            return GenerateSha256String(DateTime.Now.Ticks.ToString());
        }

        private static string GetStringFromHash(IEnumerable<byte> hash)
        {
            var result = new StringBuilder();
            foreach (var t in hash) result.Append(t.ToString("X2"));

            return result.ToString();
        }
    }
}
