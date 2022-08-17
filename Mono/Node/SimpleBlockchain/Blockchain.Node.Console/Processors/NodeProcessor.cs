using Blockchain.Cryptography.Encryption;
using Blockchain.Cryptography.Extenstions;
using Blockchain.Cryptography.Keys;
using Blockchain.Node.CLI.Constants;
using Blockchain.Node.Logic.LocalConnectors;
using System;

namespace Blockchain.Node.CLI.Processors
{
    public class NodeProcessor
    {
        private readonly NodeLocalDataConnector _nodeLocalDataConnector;

        public NodeProcessor(NodeLocalDataConnector nodeLocalDataConnector)
        {
            _nodeLocalDataConnector = nodeLocalDataConnector;
        }

        public string ListAllCommands()
        {
            return string.Join(" \n", SupportedCommands.AllSupportedCommands);
        }

        public void SetNewNodeKey(string password)
        {
            var privateKey = PrivateKeyGenerator.GeneratePrivateKey();
            var encryptedPrivateKey = AESEncryptionProvider.Encrypt(privateKey.ToHex(), password);

            _nodeLocalDataConnector.SetNewEncyptedPrivateKey(encryptedPrivateKey);
        }

        public void RestoreFromPrivateKeyEncypted(string privateKeyAsString, string password)
        {
            try
            {
                var encryptedPrivateKey = AESEncryptionProvider.Encrypt(privateKeyAsString, password);
                var decryptedPk = AESEncryptionProvider.Decrypt(encryptedPrivateKey, password);
                var decPK = decryptedPk.ToByteArray();
                _nodeLocalDataConnector.SetNewEncyptedPrivateKey(encryptedPrivateKey);
            }
            catch
            {
                throw new Exception("Invalid private key or password");
            }
        }
    }
}