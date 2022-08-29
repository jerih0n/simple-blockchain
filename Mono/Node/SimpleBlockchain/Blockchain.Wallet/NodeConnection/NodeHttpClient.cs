using Blockchain.Utils.Nodes;
using Blockchain.Wallet.Configuration;
using Blockchain.Wallet.Models.NodeResponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Wallet.NodeConnection
{
    public class NodeHttpClient
    {
        private const string PingEndpoint = "/ping";

        private const string BalanceEndpoint = "/balance";
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
            var nodeUrl = WalletConfigurationManager.GetNetworkConfiguration();
            var result = await _httpClient.GetAsync($"{nodeUrl.NodeUrl}{BalanceEndpoint}/{address}");

            return await DeserializeOutput<Balance>(result);
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