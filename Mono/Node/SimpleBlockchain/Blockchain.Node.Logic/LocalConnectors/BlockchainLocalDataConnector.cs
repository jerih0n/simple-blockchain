using Blockchain.Node.Configuration;
using Blockchain.Utils;
using Newtonsoft.Json;
using System.IO;

namespace Blockchain.Node.Logic.LocalConnectors
{
    public class BlockchainLocalDataConnector
    {
        private const string BlockChainFileName = "chain.json";

        private readonly string _localDbFilePath;
        private readonly string _blockchainFile;

        public BlockchainLocalDataConnector(NodeConfiguration nodeConfiguration)
        {
            _localDbFilePath = nodeConfiguration.BlockchainLocalFilePath;
            _blockchainFile = $"{_localDbFilePath}/{BlockChainFileName}";
        }

        public void ReadLocalDb()
        {
            BlockchainModel blockchain = null;
            if (!File.Exists(_blockchainFile))
            {
                //create new one 
               
                Directory.CreateDirectory(_localDbFilePath);
                File.Create(_blockchainFile);
                
            }
            using(var streamReaded = new StreamReader(_blockchainFile))
            {
                var blockchainAsString = streamReaded.ReadToEnd();
                blockchain = JsonConvert.DeserializeObject<BlockchainModel>(blockchainAsString);
            }

        }
    }
}
