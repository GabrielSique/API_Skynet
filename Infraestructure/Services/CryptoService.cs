using Core.Intefaces;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Infraestructure.Services
{
    public class CryptoService : ICryptoService
    {
        public CryptoService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public string Encode(string data)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                var key = "b14ca5898a4e4133bbce2ea2315a1916";
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(data);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
            //return encrypt.CryptData(data, Crypt.Encriptacion.Encripta).ToString();
        }
        public string Decode(string data)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(data);
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                throw;
            }
            
           
            //return encrypt.CryptData($"{data}", Crypt.Encriptacion.Desencripta).ToString();
        }
    }
}
