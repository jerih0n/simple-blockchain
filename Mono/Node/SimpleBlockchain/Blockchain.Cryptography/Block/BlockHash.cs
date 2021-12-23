using Blockchain.Cryptography.Extenstions;
using System.Numerics;
using System.Security.Cryptography;

namespace Blockchain.Cryptography.Block
{
    public static class BlockHash
    {
        public static byte[] CalculateBlockHash(string previousBlockHash, long nonce)
        {
            BigInteger previousHashBigIntager = new BigInteger(previousBlockHash.ToByteArray());

            var sha256 = SHA256.Create();

            previousHashBigIntager = previousHashBigIntager + nonce;
            return sha256.ComputeHash(previousHashBigIntager.ToByteArray());
        }
    }
}
