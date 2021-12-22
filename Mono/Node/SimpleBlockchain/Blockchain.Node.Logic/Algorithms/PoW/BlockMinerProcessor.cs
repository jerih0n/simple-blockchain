using Blockchain.Cryptography.Extenstions;
using Blockchain.Networking.Server;
using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.LocalConnectors;
using Blockchain.Utils;
using System;
using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Blockchain.Node.Logic.Algorithms.PoW
{
    public class BlockMinerProcessor
    {
        private readonly NodeLocalDataConnector _nodeLocalDataConnector;
        private readonly BlockchainLocalDataConnector _blockchainLocalDataConnector;
        private readonly BlockRewardProccessor _blockRewardProccessor;
        private readonly bool _continueMining;

        public BlockMinerProcessor(NodeLocalDataConnector nodelocalDataConnector,
            BlockchainLocalDataConnector blockchainLocalDataConnector,
            BlockRewardProccessor blockRewardProccessor)
        {
            _nodeLocalDataConnector = nodelocalDataConnector;
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
            _blockRewardProccessor = blockRewardProccessor;
            _continueMining = true;
        }

     
        public async Task StartMining(byte[] privateKey)
        {
            string previousHash = string.Empty;
            var lastBlock = _blockchainLocalDataConnector.GetLastBlock();
            int complexity = 0;
            long lastBlockId = 0;
            if (lastBlock == null)
            {
                previousHash = BlockchainConstants.GenesisBlockInitialHash;
                complexity = BlockchainConstants.DefaultComplexity;
                lastBlockId = 0;
            }
            else
            {
                previousHash = lastBlock.BlockHash;
                complexity = lastBlock.NextComplexity;
                lastBlockId = lastBlock.Id;
            }
            while(_continueMining)
            {
                var newBlock = await Task<Block>.Factory.StartNew(() => MineNewBlock(previousHash, complexity, lastBlockId));
                newBlock = AddTransactionsToTheNewBlock(newBlock);
                newBlock = TakeRewardForFindingNewBlock(newBlock, privateKey);
                newBlock = AddTransactionsToTheNewBlock(newBlock);
                PublishNewBlock(newBlock);
                RecordInLocalDb(newBlock);
                
                //Take block reward - mint new token
                //Notify nodes
                //TODO: for now console write line
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"new block found with hash {newBlock.BlockHash} block time {newBlock.BlockTime}");
                previousHash = newBlock.BlockHash;
                complexity = newBlock.NextComplexity;
                lastBlockId = newBlock.Id;
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

        }

        private Block MineNewBlock(string previousHash, int complexity, long lastBlockId)
        {
            var previousHashBytes = previousHash.ToByteArray();
            BigInteger previousHashBigIntager = new BigInteger(previousHashBytes);
            int incrementor = 1;
            var sha256 = SHA256.Create();
            string hashSolution = string.Empty;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                previousHashBigIntager = previousHashBigIntager + incrementor;
                var possibleBlockHash = sha256.ComputeHash(previousHashBigIntager.ToByteArray());
                var isValidBlockSolution = IsValidBlockSolution(possibleBlockHash, complexity);
                if(isValidBlockSolution)
                {
                    hashSolution = possibleBlockHash.ToHex();
                    break;
                }
                incrementor++;
            }
            stopwatch.Stop();
            var blockTimeSpan = stopwatch.Elapsed;
            var newBlock = new Block(lastBlockId + 1, hashSolution, incrementor, previousHash, "TODO", complexity, blockTimeSpan, complexity);
            return newBlock;
           
        }

        private bool IsValidBlockSolution(byte[] possibleBlockHash, int complexity)
        {
            if(complexity > 254)
            {
                throw new Exception("Invalid Complexity!");
            }
            BitArray bitArray = new BitArray(possibleBlockHash);
            bool isSolution = false;
            for(int i = 0; i< complexity; i++ )
            {
                isSolution = bitArray[i];
                if (!isSolution) 
                {
                    continue;
                    // basicaly isSolution is bit => true or false . In order to be a solution need to have 0 value or false
                };
                break;
            }
            return !isSolution;
        }

        private Block AddTransactionsToTheNewBlock(Block newBlock)
        {
            //TODO:
            return newBlock;
        }

        private void RecordInLocalDb(Block newBlock)
        {
            _blockchainLocalDataConnector.AddNewBlock(newBlock);
        }

        private Block TakeRewardForFindingNewBlock(Block block, byte[] privateKey)
        {
            var rewardingTransaction = _blockRewardProccessor.CreateNewRewardTransaction(block.BlockHash, privateKey);
            rewardingTransaction.TransactionStatus = TransactionStatusEnum.Success;
            rewardingTransaction.IsVerified = true;
            block.BlockHeader.RewardTransaction = rewardingTransaction;
            return block;
        }

        private void PublishNewBlock(Block block)
        {
            
        }
    }
}
