using Blockchain.Utils;
using Blockchain.Utils.Nodes;
using Blockchain.Wallet.Configuration;
using Blockchain.Wallet.Models.NodeResponses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blockchain.Wallet.NodeConnection
{
    public class NodeHttpClient
    {
        private const string PingEndpoint = "/ping";
        private const string BalanceEndpoint = "/balance";
        private const string CreateNewTransactionEndpoint = "/transaction";
        private readonly HttpClient _httpClient;

        public NodeHttpClient()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = new TimeSpan(40000000);
        }

        public async Task<NodeConnectionModel> PingNodeAsync()
        {
            var nodeUrl = WalletConfigurationManager.GetNetworkConfiguration();

            var result = await _httpClient.GetAsync($"{nodeUrl.NodeUrl}{PingEndpoint}");

            return await DeserializeOutput<NodeConnectionModel>(result);
        }

        public async Task<Balance> GetBalanceForAddressAsync(string address)
        {
            var nodeConfig = WalletConfigurationManager.GetNetworkConfiguration();
            var result = await _httpClient.GetAsync($"{nodeConfig.NodeUrl}{BalanceEndpoint}/{address}");

            return await DeserializeOutput<Balance>(result);
        }

        public async Task<bool> CreateNewTransactionAsync(Transaction transaction)
        {
            var nodeConfig = WalletConfigurationManager.GetNetworkConfiguration();
            var url = $"{nodeConfig.NodeUrl}{CreateNewTransactionEndpoint}";

            var content = new StringContent(JsonConvert.SerializeObject(transaction));

            try
            {
                var response = await _httpClient.PostAsync(url, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<T> DeserializeOutput<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

#pragma warning disable CS8603 // Possible null reference return.
            return JsonConvert.DeserializeObject<T>(json);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}