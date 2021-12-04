using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Cryptography.EllipticCurve
{
    public static class EllipticCurveSettings
    {
        public static void A()
        {
            var ecc = new ECCurve()
            {
                CurveType = ECCurve.ECCurveType.PrimeShortWeierstrass,
                A = new byte[] { },
            };
        }
    }
}
