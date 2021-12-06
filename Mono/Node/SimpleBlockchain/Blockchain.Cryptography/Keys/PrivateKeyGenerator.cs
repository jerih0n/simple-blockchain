using Blockchain.Cryptography.EllipticCurve;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Cryptography.Keys
{
    public static class PrivateKeyGenerator
    {
        public static byte[] GeneratePrivateKey()
        {
            ECDsaCng curveProtocol = new ECDsaCng(EllipticCurveSettings.GetElllipticCurve());

            curveProtocol.GenerateKey(EllipticCurveSettings.GetElllipticCurve());
            var privateKey = curveProtocol.ExportECPrivateKey();
            return privateKey;
        }
    }
}
