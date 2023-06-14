using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace StudentsRM.Helper
{
    public static class HashingHelper
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String
                (KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public static byte[] GenerateSalt2()
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[32];
            rng.GetBytes(salt);

            return salt;
        }

        public static byte[] ComputeHash(string password, byte[] salt)
        {
            using var sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];
            passwordBytes.CopyTo(saltedPasswordBytes, 0);
            salt.CopyTo(saltedPasswordBytes, passwordBytes.Length);

            return sha256.ComputeHash(saltedPasswordBytes);
        }

        public static string GetPasswordHash(string password, byte[] salt)
        {
            byte[] hash = ComputeHash(password, salt);

            return Convert.ToBase64String(hash);
        }

    }
}
