using System;
using System.Security.Cryptography;

namespace Blockchain.Cryptography.Addresses
{
    public static class AddressGenerator
    {
        private const string ADDRESS_PREFIX = "mychain_{0}_{1}";

        public static string GenerateFirstAddress(byte[] publicKey)
        {
            var newAddressSha = SHA256.Create().ComputeHash(publicKey);
            return CreateAddress(newAddressSha, 0);
        }

        public static string GenerateNextAddress(string address)
        {
            try
            {
                var addressSections = address.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if(addressSections[0].ToLower() == "mychain_")
                {
                    throw new Exception("Invalid Address");
                }

                var addressNumber = int.Parse(addressSections[1]);
                addressNumber += 1;
                var base64Hash = addressSections[2];
                var bytes = Convert.FromBase64String(base64Hash);
                var newAddressSha = SHA256.Create().ComputeHash(bytes);
                return CreateAddress(newAddressSha, addressNumber);
                
            }catch
            {
                throw new Exception("Invalid input");
            }
        }

        private static string CreateAddress(byte[] sha256, int addressNumber)
        {
            var sha256AsString = Convert.ToBase64String(sha256).ToLower();
            return string.Format(ADDRESS_PREFIX, addressNumber, sha256AsString);
        }
    }
}
