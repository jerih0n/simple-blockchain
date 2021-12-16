
namespace Blockchain.Cryptography.Keys
{
    public class PrivateKeyConstants
    {
        public static readonly byte[] FirstBytesArrayPrivateKey = new byte[] { 48, 120, 2, 1, 1, 4, 32 }; //+32 byte
        public static readonly byte[] IntermediatBytesArrayPrivateKey = new byte[] { 160, 11, 6, 9, 43, 36, 3, 3, 2, 8, 1, 1, 7, 161, 68, 3, 66, 0, 4 }; //from 38 to 57 byte is constants  + 64 bytes total 121 bytes private key 
    }
}
