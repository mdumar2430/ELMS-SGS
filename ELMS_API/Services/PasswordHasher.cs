using ELMS_API.Interfaces;
using System.Security.Cryptography;

namespace ELMS_API.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int saltSize = 128 / 8;
        private const int keySize = 256 / 8;
        private const int iterattions = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';
        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(saltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterattions, _hashAlgorithmName, keySize);
            string hashedPassword = string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
            return hashedPassword;
        }

        public bool VerifyPassword(string passwordHash, string inputPassword)
        {
            var element = passwordHash.Split(Delimiter);
            var salt = Convert.FromBase64String(element[0]);
            var hash = element[1];
            var inputPasswordHash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, iterattions, _hashAlgorithmName, keySize);
            bool isVerified = hash.Equals(Convert.ToBase64String(inputPasswordHash));
            return isVerified;
        }
    }
}
