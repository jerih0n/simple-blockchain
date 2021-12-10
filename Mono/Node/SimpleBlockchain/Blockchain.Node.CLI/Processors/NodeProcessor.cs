using Blockchain.Cryptography.Keys;
using Blockchain.Node.CLI.Constants;

namespace Blockchain.Node.CLI.Processors
{
    public class NodeProcessor
    {
        public string ListAllCommands()
        {
            return string.Join("\n", SupportedCommands.AllSupportedCommands);
        }

        public void SetNewNode(string password)
        {
            var privateKey = PrivateKeyGenerator.GeneratePrivateKey();

        }
    }
}
