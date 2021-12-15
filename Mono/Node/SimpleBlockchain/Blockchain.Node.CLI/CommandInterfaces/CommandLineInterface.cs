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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("********************************************* NODE STARTED *********************************************");
            // block from local storage is loaded and we are ready for sync
            Console.WriteLine("********************************************************************************************************");
            if(!_nodeLocalDataConnector.IsNodeSet)
            {
                SetUpNodeAccount();
            }
            Console.WriteLine("Node account is set");
            Console.WriteLine("Chose command");
            Console.WriteLine(_nodeProcessor.ListAllCommands());

            string command = string.Empty;
            while(true)
            {
                command = Console.ReadLine();
                if(!SupportedCommands.AllSupportedCommands.Contains(command))
                {
                    Console.WriteLine("Invalid Command");
                    continue;
                }
                break;
            }
            await ExecuteCommandAsync(command);
        }
        public async Task ExecuteCommandAsync(string command)
        {
            switch(command)
            {
                case SupportedCommands.Start:
                    await _blockMiner.StartMining();
                    break;
            }
        }
        private void SetUpNodeAccount()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Node is not set! Set new node or import existing one Available commands - {SupportedCommands.SetNode} {SupportedCommands.RestoreNode}");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var command = Console.ReadLine();
                string password = string.Empty;
                Console.ForegroundColor = ConsoleColor.White;
                if (!string.IsNullOrEmpty(command) && command.ToLower() == SupportedCommands.SetNode)
                {                   
                    Console.WriteLine("Enter password");
                    password = Console.ReadLine();
                    _nodeProcessor.SetNewNodeKey(password);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("NODE IS INITIALIZED. Your private key is stored in the local database");
                    break;
                }
                else if (!string.IsNullOrEmpty(command) && command.ToLower() == SupportedCommands.RestoreNode)
                {
                    Console.WriteLine("Enter private key");
                    var privateKeyAsString = Console.ReadLine();
                    try
                    {
                        var privateKeyAsByteArray = privateKeyAsString.ToByteArray();
                    }
                    catch
                    {
                        Console.WriteLine("Invalid private key");
                        continue;
                    }
                    Console.WriteLine("Enter Password");
                    password = Console.ReadLine();
                    _nodeProcessor.RestoreFromPrivateKeyEncypted(privateKeyAsString, password);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("NODE IS INITIALIZED. Your private key is stored in the local database");
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown Commands");
                }
            }
        }
       
    }
}
