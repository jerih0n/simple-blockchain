using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.Cache;
using Blockchain.Utils;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Blockchain.Node.Logic.LocalConnectors
{
    public class BlockchainLocalDataConnector
    {
        private const string BlockChainFileName = "chain.json";
        private readonly object _lock = new object();
        private readonly string _localDbFilePath;
        private readonly string _blockchainFile;
        private BlockchainCacheServiceProvider _cacheServiceProvider;

        public BlockchainLocalDataConnector(NodeConfiguration nodeConfiguration, BlockchainCacheServiceProvider cacheServiceProvider)
        {
            _localDbFilePath = nodeConfiguration.BlockchainLocalFilePath;
            _blockchainFile = $"{_localDbFilePath}/{BlockChainFileName}";
            _cacheServiceProvider = cacheServiceProvider;
        }

        public bool ReadLocalDb()
        {
            BlockchainModel blockchain = null;
            if (!File.Exists(_blockchainFile))
            {
                //create new one 
               
                Directory.CreateDirectory(_localDbFilePath);
                using (File.Create(_blockchainFile))
                {

                }
                
            }
            using(var streamReaded = new StreamReader(_blockchainFile))
            {
                var blockchainAsString = streamReaded.ReadToEnd();
                blockchain = JsonConvert.DeserializeObject<BlockchainModel>(blockchainAsString);
            }

            if(blockchain == null || blockchain.Blocks == null || blockchain.Blocks.Count == 0)
            {
                // no data is recorded 
                //init emtpy record InitializeEmptykLocalBlockchain();
               blockchain = InitializeEmptykLocalBlockchain();
                
            }
            _cacheServiceProvider.LoadBlockchainInMemory(blockchain);

            return true;
            //find last block in local copy 
        }

        public Block GetLastBlock()
        {
            return _cacheServiceProvider.GetLastBlock();
        }

        public void AddNewBlock(Block newBlock)
        {
            _cacheServiceProvider.InsertNewBlock(newBlock);
            var blockchain = _cacheServiceProvider.GetEntireBlockchain();

            lock(_lock)
            {
                using (var writter = new StreamWriter(_blockchainFile))
                {
                    writter.WriteLine(JsonConvert.SerializeObject(blockchain));
                }
            }
           
        }

        private BlockchainModel InitializeEmptykLocalBlockchain()
        {
            var newBlockchainLocalRecord = new BlockchainModel();

            using (var writter = new StreamWriter(_blockchainFile))
            {
                writter.WriteLine(JsonConvert.SerializeObject(newBlockchainLocalRecord));
            }

            return newBlockchainLocalRecord;
        }

        
        //After insertion in memory first
        private void InsertNewBlockInLocalStorage(Block newBlock)
        {
            var blockchain = _cacheServiceProvider.GetEntireBlockchain();
            if(blockchain != null)
            {
                blockchain.Blocks.Add(newBlock);
            }
        }
    }
}
