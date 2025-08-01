using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Encyrption
{
    public static class AesService
    {

        private static readonly byte[] key = new byte[16];


        static AesService()
        {
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(key);
        }
        
        public static string EncryptStringToBytes(string plainText)
        {
            byte[] iv = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(iv);


            if (plainText == null || plainText.Length < 0)
                throw new ArgumentNullException("plainttext");
            if (key == null || key.Length < 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length < 0)
                throw new ArgumentNullException("iv");

            byte[] encrypted;

            using var rijAlg = new RijndaelManaged();
            rijAlg.Mode=CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;


            rijAlg.Key = key;
            rijAlg.IV=iv;

            var encryptor=rijAlg.CreateEncryptor(rijAlg.Key,rijAlg.IV);
            
            using(var mEncrypt=new MemoryStream())
            {
                using (var csEncrypt=new CryptoStream(mEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using(var swEncrypt=new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted=mEncrypt.ToArray();
                }
            }
            byte[] combined=new byte[iv.Length+encrypted.Length];
            Buffer.BlockCopy(iv,0,combined,0,iv.Length);    
            Buffer.BlockCopy(encrypted,0,combined,iv.Length,encrypted.Length);
            return Convert.ToBase64String(combined);

        }

        public static string DecryptStringFromBytes(string base64CipherText)
        {
            byte[] combined=Convert.FromBase64String(base64CipherText);

            byte[] iv = new byte[16];
            byte[] encryptedData = new byte[combined.Length - 16];

            Buffer.BlockCopy(combined, 0, iv, 0, 16);
            Buffer.BlockCopy(combined, 16, encryptedData, 0, encryptedData.Length);


            if (encryptedData == null || encryptedData.Length < 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length < 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length < 0)
                throw new ArgumentNullException("key");
            string plainText = null;

            using(var rijAlg=new RijndaelManaged())
            {
                rijAlg.Mode= CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key=key;
                rijAlg.IV = iv;

                var decryptor=rijAlg.CreateDecryptor(rijAlg.Key,rijAlg.IV);

                try
                {
                    using(var  msDecrypt=new MemoryStream(encryptedData))
                    {
                        using(var csDecrypt=new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using(var srDecyrpt=new StreamReader(csDecrypt))
                            {
                                plainText = srDecyrpt.ReadToEnd();
                            }
                        } 
                    }
                }
                catch
                {
                    plainText = "keyError";
                }
            }
            return plainText;
        }

    }
}
