using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Utilities
{
    public class EncryptDecryptManager
    {
        private readonly static string key = "sdksadtweebiasyween2321342345iyt";
        public static string Encrypt(string text)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encyptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encyptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cs))
                        {
                            streamWriter.Write(text);
                        }
                        array = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }
        public static string Decrypt(string text)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(text);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamWriter = new StreamReader(cs))
                        {
                            string data = streamWriter.ReadToEnd();
                            return data;
                        }
                    }
                }
            }
        }
    }
}
