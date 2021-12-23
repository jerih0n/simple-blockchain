using Blockchain.Cryptography.Block;
using Blockchain.Cryptography.Extenstions;
using Blockchain.Networking.Server;
using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.Algorithms.Validation;
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
        private readonly NetworkConnectionService _networkConnectionService;
        private readonly bool _continueMining;
        private bool _checkNewBlock;
        private Block _recievedBlock;
        private object _lockObj = new object();
        private object _lockOjc2 = new object();

        public BlockMinerProcessor(NodeLocalDataConnector nodelocalDataConnector,
            BlockchainLocalDataConnector blockchainLocalDataConnector,
            BlockRewardProccessor blockRewardProccessor,
            NetworkConnectionService networkConnectionService)
        {
            _nodeLocalDataConnector = nodelocalDataConnector;
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
            _blockRewardProccessor = blockRewardProccessor;
            _continueMining = true;
            _checkNewBlock = true;
            _networkConnectionService = networkConnectionService;
            _networkConnectionService.NewBlockDataRecieved += NetworkConnectionService_NewBlockDataRecieved;
        }

        public async Task StartMining(byte[] privateKey)
        {
            var nodeId = _nodeLocalDataConnector.GetNodeId();
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
            _networkConnectionService.Init(nodeId);
            while (_continueMining)
            {
                var newBlock = await Task<Block>.Factory.StartNew(() => MineNewBlock(previousHash, complexity, lastBlockId, nodeId));
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

        private Block MineNewBlock(string previousHash, int complexity, long lastBlockId, string nodeId)
        {
            var previousHashBytes = previousHash.ToByteArray();
            int incrementor = 1;
            string hashSolution = string.Empty;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                lock(_lockOjc2)
                {
                    if (!_checkNewBlock)
                    {
                        stopwatch.Stop();
                        if (_recievedBlock != null)
                        {
                            //1. Deep copy of the block obj
                            //2. run new task to check 
                                //2.1 Block Validation
                                //2.2 Block Reward Transaction validation
                                //2.3 All transactions in the block
                            //if only one of those is not correct is not correct - reject the entire block and continue
                            //if recieved block is ahead than current last than more than one - sync chain
                            //if everything is ok - accept the block and record it. Use it as a new block


                        }
                        //new block is recieved!
                    }
                }
               

                var possibleBlockHash = BlockHash.CalculateBlockHash(previousHash, incrementor);
                var isValidBlockSolution = BlockValidator.IsValidBlockSolution(possibleBlockHash, complexity);
                if(isValidBlockSolution)
                {
                    hashSolution = possibleBlockHash.ToHex();
                    break;
                }
                incrementor++;
            }
            stopwatch.Stop();
            var blockTimeSpan = stopwatch.Elapsed;
            var newBlock = new Block(lastBlockId + 1, hashSolution, incrementor, previousHash, nodeId, complexity, blockTimeSpan, complexity);
            return newBlock;
           
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

        private void PublishNewBlock(Block block) => _networkConnectionService.PushNewBlock(block);
       
        private void NetworkConnectionService_NewBlockDataRecieved(object sender, Networking.EventArgs.NewBlockEventArg e)
        {
            lock(_lockObj)
            {
                _checkNewBlock = false;
                _recievedBlock = e.Block;
            }
            
            var blockRecieved = e.Block.BlockHash;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"New block with hash {blockRecieved} mined by {e.Block.Node} recieved!");
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
