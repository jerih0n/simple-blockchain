using System.Security.Cryptography;

namespace Blockchain.Cryptography.EllipticCurve
{
    public static class EllipticCurveSettings
    {
        public static ECCurve GetElllipticCurve()
        {
            var ellipticCurve = ECCurve.NamedCurves.brainpoolP256r1;
            return ellipticCurve;
        }
    }
}
