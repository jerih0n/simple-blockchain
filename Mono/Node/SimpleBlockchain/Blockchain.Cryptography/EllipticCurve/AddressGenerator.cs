using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Cryptography.EllipticCurve
{
    public static class AddressGenerator
    {
        private const string ADDRESS_PREFIX = "bchain_0x_{0}_{1}";

        public static string GenerateFirstAddress(byte[] publicKey)
        {
            var rowAddress = SHA256.Create().ComputeHash(publicKey);
            var base64 = Convert.ToBase64String(rowAddress);
            return string.Format(ADDRESS_PREFIX, 0, base64);
        }

        public static string GenerateNextAddress(string address)
        {
            try
            {
                var addressSections = address.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (addressSections[0].ToLower() == "bchain" && addressSections[1] == "0x")
                {
                    var addressNumber = int.Parse(addressSections[2]);
                    addressNumber += 1;
                    var base64Hash = addressSections[3];
                    var bytes = Convert.FromBase64String(base64Hash);
                    var newAddress = SHA256.Create().ComputeHash(bytes);
                    var newAddressAsBase64String = Convert.ToBase64String(newAddress);
                    return string.Format(ADDRESS_PREFIX, addressNumber, newAddressAsBase64String);
                }
            }catch
            {
                throw new Exception("Invalid input");
            }
           
            throw new NotImplementedException();
        }
    }
}
