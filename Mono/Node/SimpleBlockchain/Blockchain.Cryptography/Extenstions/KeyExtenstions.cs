using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Cryptography.Extenstions
{
    public static class KeyExtenstions
    {
        public static string ToHex(this byte[] key)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var oneByteOfKey in key)
            {
                builder.Append(BitConverter.ToString(new byte[] { oneByteOfKey }));
            }

            var privateKeyAsHex = builder.ToString();
            return privateKeyAsHex;
        }
        
        public static byte[] ToByteArray(this string keyAsHex)
        {           
            var keyLenght = keyAsHex.Length;
            if(keyLenght % 2 != 0)
            {
                //invalid format
                throw new Exception("Invalid key passed");
            }
            List<byte> result = new List<byte>(keyLenght / 2);
            for(int i = 1; i< keyLenght; i+=2)
            {
                var resultByte = Convert.ToByte($"{keyAsHex[i - 1]}{keyAsHex[i]}", 16);
                result.Add(resultByte);
            }

            return result.ToArray();
        }

        public static byte[] Sha256(this string inputedString) => Sha256(inputedString.ToByteArray());

        public static byte[] TotUtf8ByteArray(this string input) => Encoding.UTF8.GetBytes(input);
       
        public static byte[] Sha256Utf8String(this string input) => Sha256(TotUtf8ByteArray(input));
       
        public static byte[] Sha256(this byte[] byteArray) => SHA256.Create().ComputeHash(byteArray); 
    }
}
