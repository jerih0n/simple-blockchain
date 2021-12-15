using Blockchain.Node.CLI.CommandInterfaces;
using Blockchain.Node.CLI.Processors;
using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.Algoriths.PoW;
using Blockchain.Node.Logic.Cache;
using Blockchain.Node.Logic.LocalConnectors;
using Microsoft.Extensions.DependencyInjection;

namespace Blockchain.Node.CLI.Extensions
{
    public static class IoCModule
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<NodeConfiguration>();
            services.AddSingleton<BlockchainLocalDataConnector>();
            services.AddSingleton<BlockchainCacheServiceProvider>();
            services.AddSingleton<NodeLocalDataConnector>();
            services.AddSingleton<NodeDatabaseCacheServiceProvider>();
            services.AddSingleton<NodeProcessor>();
            services.AddSingleton<CommandLineInterface>();
            services.AddSingleton<BlockMiner>();
        }
    }
}
