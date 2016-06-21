using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;

namespace SharedExpenseManager.DataStorageUtil
{
    public static class PassHash // Used to create/verify hashes out of passwords
    {
        private const int c_saltSize = 32; // 32 bytes of salt
        private const int c_hashSize = 32; // 32 byte hash
        private const int c_iterations = 5000;

        public static string CreatePassHash(string password)
        {
            var data = Encoding.Unicode.GetBytes(password); // Convert to byte array
            var salt = GetSalt(c_saltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(data, salt, c_iterations);

            var hash = pbkdf2.GetBytes(c_hashSize);

            var saltHash = new byte[c_saltSize + c_hashSize];
            // create storable data
            Array.Copy(salt, 0, saltHash, 0, c_saltSize);
            Array.Copy(hash, 0, saltHash, c_saltSize, c_hashSize);

            var stringSaltHash = Convert.ToBase64String(saltHash);
            return stringSaltHash;
        }

        public static bool CheckPassword(string passwordInput, string passwordSaved)
        {
            var data = Encoding.Unicode.GetBytes(passwordInput); // Data to check
            var saltHash = Convert.FromBase64String(passwordSaved); // Saved record

            var salt = new byte[c_saltSize];
            Array.Copy(saltHash, 0, salt, 0, c_saltSize); // Get stored salt

            var pbkdf2 = new Rfc2898DeriveBytes(data, salt, c_iterations);
            var hash = pbkdf2.GetBytes(c_hashSize); // Generated hash

            var storedHash = new byte[c_hashSize]; // Get stored hash
            Array.Copy(saltHash, c_saltSize, storedHash, 0, c_hashSize);

            for(int i = 0; i < c_hashSize; i++) // Check the bytes
            {
                if (storedHash[i] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static byte[] GetSalt(int size)
        {
            byte[] salt = new byte[size];
            new RNGCryptoServiceProvider().GetBytes(salt); // Create salt
            return salt;
        }
    }
}
