using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;

namespace rpass
{
    class Rencrypt
    {
        rpass.Rlang Rlang = new rpass.Rlang();
        rpass.Rdefaults Rdefaults = new rpass.Rdefaults();
        // Encryption and decryption processes happen here
        public string EncryptInterface(string password, string masterPassword, string salt)
        {
            // ENCRYPTION
            // Get the salt
            List<int> crSalt = new List<int>();
            int x = Convert.ToInt32(salt);
            crSalt.Add(x / 10000000); // index 0
            crSalt.Add(((x / 1000000) - (x / 10000000) * 10)); // index 1
            crSalt.Add(((x / 100000) - (x / 1000000) * 10)); // index 2
            crSalt.Add(((x / 10000) - (x / 100000) * 10)); // index 3
            crSalt.Add(((x / 1000) - (x / 10000) * 10)); // index 4
            crSalt.Add(((x / 100) - (x / 1000) * 10)); // index 5
            crSalt.Add(((x / 10) - (x / 100) * 10)); // index 6
            crSalt.Add((x - (x / 10) * 10)); // index 7
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(password);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(masterPassword);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes,
                Convert.ToByte(crSalt[0]),
                Convert.ToByte(crSalt[1]),
                Convert.ToByte(crSalt[2]),
                Convert.ToByte(crSalt[3]),
                Convert.ToByte(crSalt[4]),
                Convert.ToByte(crSalt[5]),
                Convert.ToByte(crSalt[6]),
                Convert.ToByte(crSalt[7]));

            string hash = Convert.ToBase64String(bytesEncrypted);
            // End
            crSalt.Clear();
            return hash;
        }

        public string DecryptInterface(string hash, string masterPassword, int currentLanguage, string salt)
        {
            // DECRYPTION
            // Get the salt
            List<int> crSalt = new List<int>();
            int x = Convert.ToInt32(salt);
            crSalt.Add(x / 10000000); // index 0
            crSalt.Add(((x / 1000000) - (x / 10000000) * 10)); // index 1
            crSalt.Add(((x / 100000) - (x / 1000000) * 10)); // index 2
            crSalt.Add(((x / 10000) - (x / 100000) * 10)); // index 3
            crSalt.Add(((x / 1000) - (x / 10000) * 10)); // index 4
            crSalt.Add(((x / 100) - (x / 1000) * 10)); // index 5
            crSalt.Add(((x / 10) - (x / 100) * 10)); // index 6
            crSalt.Add((x - (x / 10) * 10)); // index 7
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(hash);
            byte[] passwordBytesdecrypt = Encoding.UTF8.GetBytes(masterPassword);
            passwordBytesdecrypt = SHA256.Create().ComputeHash(passwordBytesdecrypt);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytesdecrypt, currentLanguage,
                Convert.ToByte(crSalt[0]),
                Convert.ToByte(crSalt[1]),
                Convert.ToByte(crSalt[2]),
                Convert.ToByte(crSalt[3]),
                Convert.ToByte(crSalt[4]),
                Convert.ToByte(crSalt[5]),
                Convert.ToByte(crSalt[6]),
                Convert.ToByte(crSalt[7]));

            string password = Encoding.UTF8.GetString(bytesDecrypted);
            // End
            return password;
        }

        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes, byte s0, byte s1, byte s2, byte s3, byte s4, byte s5, byte s6, byte s7)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, 8 bytes minimum
            byte[] saltBytes = new byte[] { s0, s1, s2, s3, s4, s5, s6, s7 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes, int currentLanguage, byte s0, byte s1, byte s2, byte s3, byte s4, byte s5, byte s6, byte s7)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, 8 bytes minimum
            byte[] saltBytes = new byte[] { s0, s1, s2, s3, s4, s5, s6, s7 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        try
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        catch
                        {
                            decryptedBytes = Encoding.UTF8.GetBytes(Rlang.error1[currentLanguage]);
                            return decryptedBytes;
                        }
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
    }
}
