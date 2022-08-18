using Blockchain.Cryptography.Extenstions;
using Blockchain.Node.CLI.Constants;
using Blockchain.Node.CLI.Processors;
using Blockchain.Node.Logic.Algorithms.PoW;
using Blockchain.Node.Logic.LocalConnectors;
using System;
using System.Threading.Tasks;

namespace Blockchain.Node.CLI.CommandInterfaces
{
    public class CommandLineInterface
    {
        private readonly NodeLocalDataConnector _nodeLocalDataConnector;
        private readonly BlockchainLocalDataConnector _blockchainLocalDataConnector;
        private readonly NodeProcessor _nodeProcessor;
        private readonly BlockMinerProcessor _blockMiner;

        public CommandLineInterface(NodeLocalDataConnector nodeLocalDataConnector,
            BlockchainLocalDataConnector blockchainLocalDataConnector,
            NodeProcessor nodeProcessor,
            BlockMinerProcessor blockMiner)
        {
            _nodeLocalDataConnector = nodeLocalDataConnector;
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
            _nodeProcessor = nodeProcessor;
            _blockMiner = blockMiner;

            _nodeLocalDataConnector.ReadLocalNodeDb();
            _blockchainLocalDataConnector.ReadLocalDb();
        }

        public async Task OpenCLI()
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("********************************************* NODE STARTED *********************************************");
            // block from local storage is loaded and we are ready for sync
            System.Console.WriteLine("********************************************************************************************************");
            if (!_nodeLocalDataConnector.IsNodeSet)
            {
                SetUpNodeAccount();
            }
            System.Console.WriteLine("Node account is set");
            System.Console.WriteLine("Chose command");
            System.Console.WriteLine(_nodeProcessor.ListAllCommands());

            string command = string.Empty;
            while (true)
            {
                command = System.Console.ReadLine();
                if (!SupportedCommands.AllSupportedCommands.Contains(command))
                {
                    System.Console.WriteLine("Invalid Command");
                    continue;
                }
                break;
            }
            LoadPrivateKeyInMemory();
            await ExecuteCommandAsync(command);
        }

        public async Task ExecuteCommandAsync(string command)
        {
            switch (command)
            {
                case SupportedCommands.Start:
                    await RunMiner();
                    break;
            }
        }

        private void SetUpNodeAccount()
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"Node is not set! Set new node or import existing one Available commands - {SupportedCommands.SetNode} {SupportedCommands.RestoreNode}");
            while (true)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                var command = System.Console.ReadLine();
                string password = string.Empty;
                System.Console.ForegroundColor = ConsoleColor.White;
                if (!string.IsNullOrEmpty(command) && command.ToLower() == SupportedCommands.SetNode)
                {
                    System.Console.WriteLine("Enter password");
                    password = System.Console.ReadLine();
                    _nodeProcessor.SetNewNodeKey(password);
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("NODE IS INITIALIZED. Your private key is stored in the local database");
                    break;
                }
                else if (!string.IsNullOrEmpty(command) && command.ToLower() == SupportedCommands.RestoreNode)
                {
                    System.Console.WriteLine("Enter private key");
                    var privateKeyAsString = System.Console.ReadLine();
                    try
                    {
                        var privateKeyAsByteArray = privateKeyAsString.ToByteArray();
                    }
                    catch
                    {
                        System.Console.WriteLine("Invalid private key");
                        continue;
                    }
                    System.Console.WriteLine("Enter Password");
                    password = System.Console.ReadLine();
                    _nodeProcessor.RestoreFromPrivateKeyEncypted(privateKeyAsString, password);
                    _nodeLocalDataConnector.AddPrivateKeyInMemory(password);
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("NODE IS INITIALIZED. Your private key is stored in the local database");
                    break;
                }
                else
                {
                    System.Console.WriteLine("Unknown Commands");
                }
            }
        }

        private void LoadPrivateKeyInMemory()
        {
            while (true)
            {
                try
                {
                    System.Console.WriteLine("Enter wallet password");
                    string password = System.Console.ReadLine();
                    _nodeLocalDataConnector.AddPrivateKeyInMemory(password);
                    break;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid Attempt! Try Again!");
                }
            }
        }

        private async Task RunMiner()
        {
            var lastBlock = _blockchainLocalDataConnector.GetLastBlock();
            if (lastBlock != null)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Starting from last block with Id {lastBlock.BlockHeader.Id} and hash {lastBlock.BlockHeader.BlockHash}");
                System.Console.ForegroundColor = ConsoleColor.White;
            }
            await _blockMiner.StartMining(_nodeLocalDataConnector.GetPrivateKey());
        }
    }
}