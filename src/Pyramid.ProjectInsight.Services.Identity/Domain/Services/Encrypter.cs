using System;
using System.Security.Cryptography;
using System.Text;  

namespace Pyramid.ProjectInsight.Services.Identity.Domain.Services
{
    /// <summary>
    /// class for handle encryption
    /// </summary>
    public class Encrypter : IEncrypter
    {
        private static readonly int SaltSize = 40;
        private static readonly int DeriveBytesIterationsCount = 10000;

        /// <summary>
        /// get salt
        /// </summary>
        /// <returns>return salt</returns>
        public string GetSalt()
        {
            var random = new Random();
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// get encrypt value
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="salt">salt</param>
        /// <returns>return encrypt value</returns>
        public string GetHash(string value, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length*sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}