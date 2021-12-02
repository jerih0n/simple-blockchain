using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Blockchain.Node.Configuration
{
    public class NodeConfiguration
    { //node-local-database-path
        private const string NodeConfigurationSectionName = "node-general-config";
        private const string BlockchainLocalFilePathName = "blockchain-local-file-path";
        private const string NodePortName = "node-port";
        private const string NodeLocalDbPathName = "node-local-database-path";
        private const string PublicNodesDnsUrlName = "public-nodes-dns-url";
        private const string NodeIpName = "node-ip";

        private readonly IConfiguration _configuration;

        public NodeConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            var section = _configuration.GetSection(NodeConfigurationSectionName);
            if (!section.Exists()) throw new Exception("Configuration Section Is Missing");

            try
            {
                var children = section.GetChildren().ToList();
                BlockchainLocalFilePath = children.FirstOrDefault(x => x.Key == BlockchainLocalFilePathName).Value;
                NodePort = int.Parse(children.FirstOrDefault(x => x.Key == NodePortName).Value);
                NodeLocalDbPath = children.FirstOrDefault(x => x.Key == NodeLocalDbPathName).Value;
                PublicNodesDnsUrl = children.FirstOrDefault(x => x.Key == PublicNodesDnsUrlName).Value;
                NodeIp = children.FirstOrDefault(x => x.Key == NodeIpName).Value;
            }
            catch(Exception ex)
            {
                //todo: log
                throw ex;
            }
            
        }

        public string BlockchainLocalFilePath { get; }
        public int NodePort { get; }
        public string NodeLocalDbPath { get; }
        public string PublicNodesDnsUrl { get; }
        public string NodeIp { get; }
    }
}
