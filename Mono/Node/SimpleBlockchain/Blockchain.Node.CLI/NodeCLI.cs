using Blockchain.Node.CLI.CommandInterfaces;
using Blockchain.Node.CLI.Processors;
using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.LocalConnectors;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blockchain.Node.CLI
{
    public class NodeCLI : IHostedService
    {

        private readonly NodeConfiguration _nodeConfiguration;

        private bool _isNodeSet;
        private readonly CommandLineInterface _commandLineInterface;

        public NodeCLI(NodeConfiguration nodeConfiguration, 
            CommandLineInterface commandLineInterface)
        {
            _nodeConfiguration = nodeConfiguration;
            _commandLineInterface = commandLineInterface;
        }

        public async Task StartAsync(CancellationToken cancellationToken) => await StartNode();

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task StartNode()
        {
            _commandLineInterface.OpenCLI();
        }
    }
}
