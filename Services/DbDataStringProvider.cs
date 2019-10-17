using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ag.DbData.Abstraction.Services
{
    /// <summary>
    /// Provides basic encryption for connection string.
    /// </summary>
    public class DbDataStringProvider: IDbDataStringProvider
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] _hash;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] _cipher;
        
        /// <inheritdoc />
        [DebuggerBrowsable( DebuggerBrowsableState.Never)]
        public string ConnectionString
        {
            get => decryptStringFromBytesAes(_cipher);
            set
            {
                _hash = new byte[32];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetNonZeroBytes(_hash);
                }
                _cipher = encryptStringToBytesAes(value);
            }
        }

        [DebuggerHidden]
        private byte[] encryptStringToBytesAes(string plainText)
        {
            byte[] encrypted;

            if (string.IsNullOrEmpty(plainText))
                return null;

            using (var aesAlg = Aes.Create())
            {
                if (aesAlg == null) throw new InvalidOperationException("Cannot create Aes object");
                aesAlg.Key = _hash;
                aesAlg.IV = getIv();

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        [DebuggerHidden]
        private string decryptStringFromBytesAes(byte[] cipherText)
        {
            var plaintext = "";

            if (cipherText == null || cipherText.Length == 0)
                return plaintext;

            using (var aesAlg = Aes.Create())
            {
                if (aesAlg == null) throw new InvalidOperationException("Cannot create Aes object");
                aesAlg.Key = _hash;
                aesAlg.IV = getIv();

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        [DebuggerHidden]
        private byte[] getIv()
        {
            var iv= Encoding.ASCII.GetBytes(Convert.ToBase64String(_hash).Substring(7, 16));
            Array.Reverse(iv);
            return iv;
        }
    }
}
