using System;
using System.Security.Cryptography;
using System.Text;

namespace WTS.BL.Utils
{
    public static class Hasher
    {
        public static string ToHash(this string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static void CheckHash(this string hash, string input)
        {
            if (hash != input.ToHash())
                throw new Exception("wrong hash");
        }

    }
}
