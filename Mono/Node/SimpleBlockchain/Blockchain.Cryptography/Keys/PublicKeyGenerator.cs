
using Blockchain.Cryptography.EllipticCurve;
using System.Security.Cryptography;

namespace Blockchain.Cryptography.Keys
{
    public static class PublicKeyGenerator
    {
        public static byte[] GeneratePublicKey(byte[] privateKey)
        {
            ECDsa curveProtocol = ECDsa.Create(EllipticCurveSettings.GetElllipticCurve());
            curveProtocol.ImportECPrivateKey(new System.ReadOnlySpan<byte>(privateKey), out int bytesRead);

            return curveProtocol.ExportSubjectPublicKeyInfo();

        }
    }
}
