using Blockchain.Networking.Clients;
using Blockchain.Networking.Server;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blockchain.Networking
{
    public static class BlockchainNetworkingModule
    {
        public static void RegisterNetworkModule(this IServiceCollection serviceCollection)
        {
            var random = new Random();

            var sex = random.Next(0, 2);
            // haha 0 for male 1 for female. used to determinate the read/write chanels of the node

            serviceCollection.AddSingleton(x => ActivatorUtilities.CreateInstance<NodeServer>(x, sex));

            serviceCollection.AddSingleton(x => ActivatorUtilities.CreateInstance<NodeClient>(x, sex));

            serviceCollection.AddSingleton<NetworkConnectionService>();
        }
    }
}
