using Microsoft.Extensions.Configuration;

namespace Blockchain.Wallet.Configuration
{
    public static class WalletConfigurationManager
    {
        public static NetworkConfiguration GetNetworkConfiguration() => Program.Configuration.GetSection("Network").Get<NetworkConfiguration>();
    }
}