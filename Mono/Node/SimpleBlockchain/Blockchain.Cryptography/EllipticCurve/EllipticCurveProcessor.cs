using Blockchain.Cryptography.Extenstions;
using System.Security.Cryptography;

namespace Blockchain.Cryptography.EllipticCurve
{
    public class EllipticCurveProcessor
    {
        private readonly ECDsa _elipticCurveAlgorithInstance;

        public EllipticCurveProcessor()
        {
            _elipticCurveAlgorithInstance = ECDsa.Create(EllipticCurveSettings.GetElllipticCurve());
        }

        public byte[] GetPublicKeyFromPrivate(byte[] privateKey)
        {
           _elipticCurveAlgorithInstance.ImportECPrivateKey(new System.ReadOnlySpan<byte>(privateKey), out int bytesReade);
            var publicKey = _elipticCurveAlgorithInstance.ExportSubjectPublicKeyInfo();
            return publicKey;
        }

        public byte[] GetPublicKeyFromPrivate(string privateKey)
        {
            var privateKeyAsByteArray = privateKey.ToByteArray();
            return GetPublicKeyFromPrivate(privateKeyAsByteArray);
        }

        public byte[] SignTransactionHash(byte[] transactionHash, byte[] privateKey)
        {
            _elipticCurveAlgorithInstance.ImportECPrivateKey(privateKey, out int bytes);

            return _elipticCurveAlgorithInstance.SignData(transactionHash, HashAlgorithmName.SHA256);
        }
    }
}
