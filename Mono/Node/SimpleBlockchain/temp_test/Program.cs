using Blockchain.Cryptography.Addresses;
using Blockchain.Cryptography.EllipticCurve;
using Blockchain.Cryptography.Extenstions;
using Blockchain.Cryptography.Keys;
using System;
using System.Collections;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace temp_test
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //CheckIsMessage();
            Console.WriteLine("Hello World!");


            var previousHash = "546B99B24C23F6F3A7FF3624E7806AAE3BC8360637777B6DEFD5D3FD89FB31CB";
            //var NextHash = "546B99B24C23F6F3A7FF3624E7806AAE3BC8360637777B6DEFD5D3FD89FB31CB";
            var aa2 = "";
            var bytes = previousHash.ToByteArray();
            byte[] ina = new byte[32];
            RandomNumberGenerator.Create().GetBytes(ina);
            BigInteger bigInt = new BigInteger(bytes);
            int incrementor = 1;
            while(true)
            {
                bigInt = bigInt + incrementor;
                var sha256 = SHA256.Create().ComputeHash(bigInt.ToByteArray());
                BitArray bitArray = new BitArray(sha256);
                if(bitArray[0] == false && bitArray[1] == false)
                {
                    //solution found!
                    //sha256 is the solution
                    var aa = sha256.ToHex();
                    Console.WriteLine(aa);
                }
                incrementor++;
            }

            var ffafa = PrivateKeyGenerator.GeneratePrivateKey();

            var privateKeyAsHex = "30780201010420373C982CEFAEE8D3537541DAA90F37409078C8D55D42E60DD976E6C166096259A00B06092B2403030208010107A1440342000463132D1C555946146065659F744ED411304AAC38C65AB23C4D9BA24FF830664A1C70787B36801C02F4F3433D1345447397B1FBD8132856E8F200C4F04D7B7D4A";
            var privateKey = privateKeyAsHex.ToByteArray();
            ECDsa a = ECDsa.Create(EllipticCurveSettings.GetElllipticCurve());
            var span = new ReadOnlySpan<byte>(privateKey);
            a.ImportECPrivateKey(span, out int biteRead);
            var getPublicKey = a.ExportSubjectPublicKeyInfo();
            var publicKeyAsHex = getPublicKey.ToHex();
            var convertedPublicKeyAsBiteAray = publicKeyAsHex.ToByteArray();

            var someText = "Test";
            var msg = Encoding.UTF8.GetBytes(someText);
            var signature = a.SignData(msg, HashAlgorithmName.SHA256);
            var signatureAsHex = signature.ToHex();
            var verrifyResult = a.VerifyData(msg, signature, HashAlgorithmName.SHA256);
            if(verrifyResult)
            {
                Console.WriteLine("VERIFIED!");
            }

            var msgDec = Encoding.UTF8.GetString(msg);
            var address = AddressGenerator.GenerateFirstAddress(getPublicKey);
            Console.WriteLine(address);
           
            for(int i =0; i < 5; i ++)
            {
                address = AddressGenerator.GenerateNextAddress(address);
                Console.WriteLine(address);
            }
            //var publickey = ElipticCurverProcessor.GeneratePublicKeyFromPrivate(privateKey1);
            //var publicKey2 = ElipticCurverProcessor.GeneratePublicKeyDerivateFromPublicKey(publickey);
        }

        //basic validation logic for transactions!
        public static void CheckIsMessage()
        {
            var publicKeyAsHex = "305A301406072A8648CE3D020106092B24030302080101070342000463132D1C555946146065659F744ED411304AAC38C65AB23C4D9BA24FF830664A1C70787B36801C02F4F3433D1345447397B1FBD8132856E8F200C4F04D7B7D4A";
            var signitureAsHex = "15B0F593D87759531A91F1345B8085205EBE8E904E22F195D203B3D6AA0BBE3A0304CB164C87EADFAB377ABEE662E418CCB844D444FB98A3205AA28D3341E352";
            var signiture = signitureAsHex.ToByteArray();
            var someText = "Ivan Qde kiselo Zeli";
            var publicKey = publicKeyAsHex.ToByteArray();
            ECDsa a = ECDsa.Create(EllipticCurveSettings.GetElllipticCurve());
            a.ImportSubjectPublicKeyInfo(new ReadOnlySpan<byte>(publicKey), out int bytesRead);
            var msg = Encoding.UTF8.GetBytes(someText);
            var isVerrified = a.VerifyData(msg, signiture, HashAlgorithmName.SHA256);
            if(isVerrified)
            {
                // confirm
            }

        }
    }
}
