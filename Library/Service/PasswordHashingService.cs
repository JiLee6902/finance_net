using Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class PasswordHashingService : IPasswordHashingService
    {
        public PasswordHashingService()
        {

        }
        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] ComputeHash(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, passwordWithSaltBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, passwordWithSaltBytes, passwordBytes.Length, salt.Length);

                return sha256.ComputeHash(passwordWithSaltBytes);
            }
        }
        public string HashPassword(string password, byte[] salt)
        {
            // Compute the hash of the password with the salt
            byte[] hash = ComputeHash(password, salt);

            // Combine the salt and hash into a single string
            string saltBase64 = Convert.ToBase64String(salt);
            string hashBase64 = Convert.ToBase64String(hash);
            string hashedPassword = $"{saltBase64}:{hashBase64}";

            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Split the hashed password into salt and hash
            string[] parts = hashedPassword.Split(':');
            string saltBase64 = parts[0];
            string hashBase64 = parts[1];

            // Decode the salt and hash from Base64
            byte[] salt = Convert.FromBase64String(saltBase64);
            byte[] hash = Convert.FromBase64String(hashBase64);

            // Compute the hash of the password with the stored salt
            byte[] computedHash = ComputeHash(password, salt);

            // Compare the computed hash with the stored hash
            return CompareHashes(hash, computedHash);
        }

        private bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
            {
                return false;
            }

            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
