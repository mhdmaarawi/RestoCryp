using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class SecurityOperation
    {
        public SecurityOperation()
        {
            Cryptography.InitilizeCryptography();
        }

        internal void SetServerPublicKey(byte[] modulus, byte[] exponent)
        {
            (new Cryptography()).SetServerPublicKey(modulus, exponent);
        }

        internal MyRSAParameters GetPublicKey()
        {
            return (new Cryptography()).GetPublicKey();
        }

        public string Decrypt(string encryptedData)
        {
            return (new Cryptography()).Decrypt(encryptedData);
        }
        public string Encrept(string plainData)
        {
            return (new Cryptography()).Encrypt(plainData);
        }
        public string DecryptAes(string encryptedData)
        {
            return (new Cryptography()).DecryptAes(encryptedData);
        }
        public byte[] DecryptAes(byte[] encryptedBytes)
        {
            return (new Cryptography()).DecryptAes(encryptedBytes);
        }
        public string EncreptAes(string plainData)
        {
            return (new Cryptography()).EncryptAes(plainData);
        }
        public byte[] EncreptAes(byte[] plainBytes)
        {
            return (new Cryptography()).EncryptAes(plainBytes);
        }
        public void SetAesParams(byte[] Key, byte[] IV)
        {
            (new Cryptography()).SetAesParams(Key, IV);
        }
    }
}
