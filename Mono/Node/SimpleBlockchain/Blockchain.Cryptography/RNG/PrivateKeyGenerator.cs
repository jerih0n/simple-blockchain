using System.Security.Cryptography;

namespace Blockchain.Cryptography.RNG
{
    public static class PrivateKeyGenerator
    {

        private static RNGCryptoServiceProvider CryptoServiceProvider = new RNGCryptoServiceProvider();
        private static SHA256 SHA256 = SHA256.Create();

        public static byte[] GeneratePrivateKey()
        {
            byte[] privateKey = new byte[32];          
            CryptoServiceProvider.GetBytes(privateKey);
            var sha256PrivateKey = SHA256.ComputeHash(privateKey);
            return sha256PrivateKey;
        }
    }
}
