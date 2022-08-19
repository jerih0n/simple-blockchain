using Blockchain.Cryptography.Block;
using Blockchain.Cryptography.Extenstions;
using Blockchain.Networking.Server;
using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.Algorithms.Validation;
using Blockchain.Node.Logic.LocalConnectors;
using Blockchain.Utils;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
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
        private ConcurrentDictionary<long, Block> _recievedBlocks;
        private Block _lastMinedBlock;
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
            _networkConnectionService = networkConnectionService;
            _networkConnectionService.NewBlockDataRecieved += NetworkConnectionService_NewBlockDataRecieved;
            _recievedBlocks = new ConcurrentDictionary<long, Block>();
            _lastMinedBlock = null;
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
                previousHash = lastBlock.BlockHeader.BlockHash;
                complexity = lastBlock.BlockHeader.NextComplexity;
                lastBlockId = lastBlock.BlockHeader.Id;
                _lastMinedBlock = lastBlock;
            }
            _networkConnectionService.Init(nodeId);
            while (_continueMining)
            {
                var (newBlock, isMindedBlock) = await Task<(Block, bool)>.Factory.StartNew(() => MineNewBlock(previousHash, complexity, lastBlockId, nodeId));

                if (isMindedBlock)
                {
                    CreateNewBlock(newBlock, privateKey);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"new block found with hash {newBlock.BlockHeader.BlockHash} block time {newBlock.BlockHeader.BlockTime}");
                    previousHash = newBlock.BlockHeader.BlockHash;
                    complexity = newBlock.BlockHeader.NextComplexity;
                    lastBlockId = newBlock.BlockHeader.Id;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    _lastMinedBlock = newBlock;
                }
                else
                {
                    if (ValidateBlock(newBlock))
                    {
                        RecordInLocalDb(newBlock);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"new block ACCEPTED with hash {newBlock.BlockHeader.BlockHash} block time {newBlock.BlockHeader.BlockTime} from node {newBlock.BlockHeader.Node}");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        previousHash = newBlock.BlockHeader.BlockHash;
                        complexity = newBlock.BlockHeader.NextComplexity;
                        lastBlockId = newBlock.BlockHeader.Id;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"block REJECTED with hash {newBlock.BlockHeader.BlockHash} block time {newBlock.BlockHeader.BlockTime} from node {newBlock.BlockHeader.Node}");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                }
            }
        }

        private (Block, bool) MineNewBlock(string previousHash, int complexity, long lastBlockId, string nodeId)
        {
            var previousHashBytes = previousHash.ToByteArray();
            int incrementor = 1;
            string hashSolution = string.Empty;
            Stopwatch stopwatch = new Stopwatch();

            while (true)
            {
                lock (_lockOjc2)
                {
                    if (_recievedBlocks.Count > 0)
                    {
                        var lastBlock = _recievedBlocks.OrderBy(x => x.Key).FirstOrDefault();
                        _recievedBlocks.Clear();
                        if (lastBlock.Key >= lastBlockId)
                        {
                            //new block
                            if (lastBlock.Key > lastBlockId + 1)
                            {
                                //difference is bigger than 1 block sync is required
                                //TODO:
                            }
                            return (lastBlock.Value, false);
                        }
                    }
                }

                var possibleBlockHash = BlockHash.CalculateBlockHash(previousHash, incrementor);
                var isValidBlockSolution = BlockValidator.IsValidBlockSolution(possibleBlockHash, complexity);
                if (isValidBlockSolution)
                {
                    hashSolution = possibleBlockHash.ToHex();
                    break;
                }
                incrementor++;
            }

            stopwatch.Stop();
            var blockTimeSpan = stopwatch.Elapsed;
            var newBlock = new Block(lastBlockId + 1, hashSolution, incrementor, previousHash, nodeId, complexity, blockTimeSpan, complexity);
            return (newBlock, true);
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
            var rewardingTransaction = _blockRewardProccessor.CreateNewRewardTransaction(block.BlockHeader.BlockHash, privateKey, TransactionTypesEnum.Reward);
            rewardingTransaction.TransactionStatus = TransactionStatusEnum.Success;
            rewardingTransaction.IsVerified = true;
            block.Transactions.Add(rewardingTransaction);
            return block;
        }

        private void PublishNewBlock(Block block) => _networkConnectionService.PushNewBlock(block);

        private void NetworkConnectionService_NewBlockDataRecieved(object sender, Networking.EventArgs.NewBlockEventArg e)
        {
            lock (_lockObj)
            {
                _recievedBlocks.TryAdd(e.Block.BlockHeader.Id, e.Block);
            }

            var blockRecieved = e.Block.BlockHeader.BlockHash;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"New block with hash {blockRecieved} mined by {e.Block.BlockHeader.Node} recieved!");
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private void CreateNewBlock(Block newBlock, byte[] privateKey)
        {
            newBlock = AddTransactionsToTheNewBlock(newBlock);
            newBlock = TakeRewardForFindingNewBlock(newBlock, privateKey);
            newBlock = AddTransactionsToTheNewBlock(newBlock);
            PublishNewBlock(newBlock);
            RecordInLocalDb(newBlock);
        }

        private bool ValidateBlock(Block block)
        {
            var blockValidationStatus = BlockValidator.ValidateRecievedBlock(block, _lastMinedBlock);
            if (blockValidationStatus != AfterBlockValidationAction.Accept) return false;

            //TODO: validate transactions

            return true;
        }
    }
}