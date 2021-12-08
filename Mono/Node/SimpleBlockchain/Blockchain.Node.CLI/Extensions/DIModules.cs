using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.Cache;
using Blockchain.Node.Logic.LocalConnectors;
using Microsoft.Extensions.DependencyInjection;

namespace Blockchain.Node.CLI.Extensions
{
    public static class DIModules
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<NodeConfiguration>();
            services.AddSingleton<BlockchainLocalDataConnector>();
            services.AddSingleton<BlockchainCacheServiceProvider>();
            services.AddSingleton<NodeLocalDataConnector>();
            services.AddSingleton<NodeDatabaseCacheServiceProvider>();
        }
    }
}
