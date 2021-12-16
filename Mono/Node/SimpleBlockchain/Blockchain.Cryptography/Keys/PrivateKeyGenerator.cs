using Blockchain.Cryptography.EllipticCurve;
using System.Security.Cryptography;

namespace Blockchain.Cryptography.Keys
{
    public static class PrivateKeyGenerator
    {
        public static byte[] GeneratePrivateKey()
        {
            ECDsa curveProtocol = ECDsa.Create(EllipticCurveSettings.GetElllipticCurve());

            curveProtocol.GenerateKey(EllipticCurveSettings.GetElllipticCurve());
            var privateKey = curveProtocol.ExportECPrivateKey();
            return privateKey;
        }

    }
}
