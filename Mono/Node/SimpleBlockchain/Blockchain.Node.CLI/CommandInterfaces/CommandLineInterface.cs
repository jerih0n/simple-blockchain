using Blockchain.Cryptography.Extenstions;
using Blockchain.Node.CLI.Constants;
using Blockchain.Node.CLI.Processors;
using Blockchain.Node.Logic.LocalConnectors;
using System;

namespace Blockchain.Node.CLI.CommandInterfaces
{
    public class CommandLineInterface
    {
        private readonly NodeLocalDataConnector _nodeLocalDataConnector;
        private readonly BlockchainLocalDataConnector _blockchainLocalDataConnector;
        private readonly NodeProcessor _nodeProcessor;
        

        public CommandLineInterface(NodeLocalDataConnector nodeLocalDataConnector, 
            BlockchainLocalDataConnector blockchainLocalDataConnector,
            NodeProcessor nodeProcessor)
        {
            _nodeLocalDataConnector = nodeLocalDataConnector;
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
            _nodeProcessor = nodeProcessor;

            _nodeLocalDataConnector.ReadLocalNodeDb();
            _blockchainLocalDataConnector.ReadLocalDb();
        }

        public void OpenCLI()
        {       
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("********************************************* NODE STARTED *********************************************");
            // block from local storage is loaded and we are ready for sync
            Console.WriteLine("********************************************************************************************************");
            if(!_nodeLocalDataConnector.IsNodeSet)
            {
                SetUpNodeAccount();
            }
            LoadPrivateKeyInMemory();
            Console.WriteLine("Node account is set");
            Console.WriteLine("Chose command");
            Console.WriteLine(_nodeProcessor.ListAllCommands());
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
        private void LoadPrivateKeyInMemory()
        {
            Console.WriteLine("Plase enter password for your account");
            var password = Console.ReadLine();
            _nodeLocalDataConnector.
        }
    }
}
