using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class Cryptography
    {
        private static AesManaged aes;
        private static RSAParameters RsaParam;
        public static RSAParameters ServerRsa;
        private static int RsaDataBlockSize = 50;
        public static int RsaKeySize = 2048;
        public static byte[] AesKey { get; private set; }
        public static byte[] AesIV { get; private set; }

        public Cryptography() { }
        static string k = "< RSAKeyValue >< Modulus > gWXgT + JRYur2q / g6cBA31iSk9CXcIBZ / SE3itE4jSDZWcj3lznwoLvpsgJ / 2xlXc2Q6buQC+2MUVXGyqA8Y79czhcb0BUKYR4XuUNhcFMhPal208n+54Q/w7ng8TaJyQDPatMebVkN5CBEE52G/3fuwFeeh86Sydkr/v1ydMWRQDbx7WMMvYaMGEbCWkSWIzgzNucshG5Z675DwmzEWGu7OeuLeEIKC/urjGdiDjufSvYNOqn2fC3OEutJ6l1tH1T/s2NELccdgxUPeNLf8xnjb3DbkhCe4NJ2kelg8v1JXTpa+3eRRgpX32VTNMyQUCkUZqDD+uPlN4l/Cc0bLa1w==</Modulus><Exponent>EQ==</Exponent><P>xhNtxK0/gDMthSyEW3I+4PX8oBpm7qNJ0N2RaKsO1x8qup7nmwp59YSaPaTgbxlr2DzYsTBMqmbhOybwZ3KpkvPrgwPJGhiaUG6jY3yZa11IchuxoxGLbpTFeTNPyyguxlA6t6QgQdS7mR1k7DOHAp4vYWV4Z0PPYzwkeNKnjL8=</P><Q>pz0BY/jJJ9f5/nQV5No/FA/z5qZ1a/Sv/lt8Xpw8H6OV5Hj20qQF1LuR69z5Og9kLV0uFR7pVk3CWQ8Cemm+ywGB6CBOPXJH/heEBStlSMI6I31ZXyFfoQ9Xjv3h4atwU3kwIDI4RIE3Gn3JJCxMOJPrCYkbH74C12jvQSLff+k=</Q><DP>Rei9VHlhtMbExZc9xewWMUfCktwkVDmhlQLoBtL2Lc7DyWVCrzDfwBCu6JSLcoFxW2DE8z45LRVAbzrrb84du2Ul8gFWCTXcHGNIubOBcS/7c5FNwRVARSVy33uFkv8fkUl+IrJlvOGrn3PJRE5r4s5rE1D9UZ92X0JnObPCqiU=</DP><DQ>nWaX5Z7bcMtFpDEFjBiz1qWaQn6Modc8OrB1DcA4lj+cIlO7IJpf10cf7Qw13Eq4oyqFuYaBQisRRMLVJ+sN7D2ncQ9Ysk1w7yUw9c59cae+P4UI0gFK8fBSaHZ6Pc6H1he00wIW1xAz3LKfMRqiFyHOJxehDtDznY/wPU3/h3E=</DQ><InverseQ>FTdwDhD2mskqqb55wy0qxLPUt/qlq2WaJfxgA7/WY38v6HdEBbQax3sT2EQfzedexjYl7o5z9Bv9EOq5IfMmNF9598xKDKifZQrcJcTVycClZupVnDRy8l/Nedm0hVjNF44XAlW6O/modRZJ87FZzFtZDngHZnYpWMupgj8N40Y=</InverseQ><D>Lat8WG3+m2H8tSpu+mATtP3f3bMCZa2WVcEi1jmyGXyW+yTnsknwEJSew/w4+rTkiNf6ucQHH1SeIJ7Sl+ucsRsiZGDTSaQGT5UHIiY+EayngL0kdK6EzLNgVec0Bs3YfQvEqDNab12AtjUjeZDt8I+JdlIN9/GhBp4Yag3etgaPVh/i5a5NTjZaojEyWUF/d4EGewf5DFhTh7YDmFhvDJH/3mq8oMBWo7koVjdB2N/Dc1dfo9VTlbtjtDyfhY6t47mS9qXyq95QC1iFXHikw064/AE4D6az0/y8nBHyAFcUa9wSsefysqmfFHpRjooKIZcOvy2zfsQz+10Exi2UEQ==</D></RSAKeyValue>";
        public static void InitilizeCryptography()
        {
            Cryptography.aes = new AesManaged();
            Cryptography.ServerRsa = new RSAParameters();
            using (var r = new RSACryptoServiceProvider(RsaKeySize))
            {
#if DEBUG
                r.FromXmlString(k);
#endif
                Cryptography.RsaParam = r.ExportParameters(true);
            }
        }

        public string Decrypt(string encryptedText)
        {
            using (var r = new RSACryptoServiceProvider(RsaKeySize))
            {
                r.ImportParameters(Cryptography.RsaParam);
                var encryptedBytes = Convert.FromBase64String(encryptedText);
                string decryptedText = "";
                int index = 0;
                var countToRead = 256;
                int maxIndex = encryptedBytes.Length / 256;
                while (true)
                {
                    if (index >= maxIndex)
                        break;
                    var t = encryptedBytes.Skip<byte>(index * countToRead).Take<byte>(countToRead).ToArray();
                    var tBytes = r.Decrypt(t, false);
                    decryptedText += Encoding.Unicode.GetString(tBytes);
                    index++;
                }
                return decryptedText;
            }
        }

        public string Encrypt(string plainText)
        {

            using (var r = new RSACryptoServiceProvider(RsaKeySize))
            {
                r.ImportParameters(Cryptography.ServerRsa);
                List<byte> encryptedBytes = new List<byte>();
                int index = 0;
                int textLength = plainText.Length;
                int maxIndex = textLength / RsaDataBlockSize;
                int readingSize = RsaDataBlockSize;
                while (true)
                {
                    if (index > maxIndex)
                        break;
                    if (index == maxIndex)
                        readingSize = textLength - index * RsaDataBlockSize;
                    var ts = plainText.Substring(index * RsaDataBlockSize, readingSize);
                    var tsBytes = Encoding.Unicode.GetBytes(ts);
                    var t = r.Encrypt(tsBytes, false);
                    encryptedBytes.AddRange(t);
                    index++;
                }

                string encryptedData = Convert.ToBase64String(encryptedBytes.ToArray());
                return encryptedData;
            }
        }

        public void SetServerPublicKey(byte[] modulus, byte[] exponent)
        {
            Cryptography.ServerRsa.Modulus = modulus;
            Cryptography.ServerRsa.Exponent = exponent;
        }

        public void SetServerPublicKey(RSAParameters rsaParam)
        {
            Cryptography.ServerRsa = rsaParam;
        }

        public MyRSAParameters GetPublicKey()
        {
            return new MyRSAParameters(RsaParam.Modulus, RsaParam.Exponent);
        }

        public string EncryptAes(string plainText)
        {
            byte[] encrypted;
            using (var encryptor = Cryptography.aes.CreateEncryptor(Cryptography.AesKey, Cryptography.AesIV))
            {
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                            swEncrypt.Flush();
                            csEncrypt.FlushFinalBlock();
                            swEncrypt.Flush();

                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        public byte[] EncryptAes(byte[] plainData)
        {
            byte[] encrypted;
            using (var encryptor = Cryptography.aes.CreateEncryptor(Cryptography.AesKey, Cryptography.AesIV))
            {
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainData, 0, plainData.Length);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }

        public string DecryptAes(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            string plaintext = "";
            using (var decryptor = Cryptography.aes.CreateDecryptor(Cryptography.AesKey, Cryptography.AesIV))
            {
                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        public byte[] DecryptAes(byte[] cipherBytes)
        {
            byte[] decryptedBytes;
            using (var decryptor = Cryptography.aes.CreateDecryptor(Cryptography.AesKey, Cryptography.AesIV))
            {
                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        decryptedBytes = new byte[cipherBytes.Length];
                        int desOffset = 0;
                        int bufferLength = 1024;
                        byte[] buffer = new byte[bufferLength];
                        int bytesCount = 0;
                        do
                        {
                            bytesCount = csDecrypt.Read(buffer, 0, bufferLength);
                            System.Buffer.BlockCopy(buffer, 0, decryptedBytes, desOffset, bytesCount);
                            desOffset += bytesCount;
                        } while (bytesCount == bufferLength);
                        Array.Resize<byte>(ref decryptedBytes, desOffset);
                    }
                }
            }
            return decryptedBytes;
        }
        public void SetAesParams(byte[] Key, byte[] IV)
        {
            Cryptography.AesIV = IV;
            Cryptography.AesKey = Key;
        }
    }
}
