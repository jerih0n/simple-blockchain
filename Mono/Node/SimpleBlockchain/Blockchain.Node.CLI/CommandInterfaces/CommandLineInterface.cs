using Blockchain.Node.CLI.Constants;
using Blockchain.Node.CLI.Processors;
using Blockchain.Node.Logic.LocalConnectors;
using System;

namespace Blockchain.Node.CLI.CommandInterfaces
{
    public class CommandLineInterface
    {
        private readonly NodeProcessor _nodeProcessor;
        public CommandLineInterface(NodeProcessor nodeProcessor)
        {
            _nodeProcessor = nodeProcessor;
        }

        public void OpenCLI(NodeLocalDataConnector nodeLocalDataConnector, BlockchainLocalDataConnector blockchainLocalDataConnector)
        {       
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("********************************************* NODE STARTED *********************************************");
            // block from local storage is loaded and we are ready for sync
            Console.WriteLine("********************************************************************************************************");
            if(nodeLocalDataConnector.IsNodeSet)
            {
                SetUpNodeAccount();

            }
            Console.WriteLine("Chose command");
            Console.WriteLine(_nodeProcessor.ListAllCommands());
        }
        private void SetUpNodeAccount()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Node is not set! Set new node or import existing one \n{SupportedCommands.SetNode}\n{SupportedCommands.RestoreNode}");
            while (true)
            {
                var command = Console.ReadLine();
                string password = string.Empty;
                if (!string.IsNullOrEmpty(command) && command.ToLower() == SupportedCommands.SetNode)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Enter password");
                    break;
                }
                if (!string.IsNullOrEmpty(command) && command.ToLower() == SupportedCommands.RestoreNode)
                {
                    break;
                }
            }
        }
    }
}
