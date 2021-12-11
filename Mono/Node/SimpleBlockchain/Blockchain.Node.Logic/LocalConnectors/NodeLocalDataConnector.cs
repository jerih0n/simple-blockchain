using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.Cache;
using Blockchain.Utils.Cryptography.Node;
using Newtonsoft.Json;
using System.IO;

namespace Blockchain.Node.Logic.LocalConnectors
{
    public class NodeLocalDataConnector
    {
        private bool _isNodeSet;
        private const string NODE_DB_FILE_NAME = "node-data.json";
        private readonly NodeDatabaseCacheServiceProvider _cacheServiceProvider;
        private readonly NodeConfiguration _nodeConfiguration;
        private string _nodeDbFile;

        public NodeLocalDataConnector(NodeConfiguration nodeConfiguration, NodeDatabaseCacheServiceProvider cacheServiceProvider)
        {
            _cacheServiceProvider = cacheServiceProvider;
            _nodeConfiguration = nodeConfiguration;
            _nodeDbFile = $"{nodeConfiguration.NodeLocalDbPath}/{NODE_DB_FILE_NAME}";
            _isNodeSet = false;
        }

        public void ReadLocalNodeDb()
        {
            NodeDatabase nodeDatabase = null;

            if (!File.Exists(_nodeDbFile))
            {
                //create new one 

                Directory.CreateDirectory(_nodeConfiguration.NodeLocalDbPath);
                File.Create(_nodeDbFile);

            }
            using (var streamReaded = new StreamReader(_nodeDbFile))
            {
                var nodeDatabaseAsString = streamReaded.ReadToEnd();
                nodeDatabase = JsonConvert.DeserializeObject<NodeDatabase>(nodeDatabaseAsString);
            }

            if (nodeDatabase == null)
            {
                // no data is recorded 
                //init emtpy record InitializeEmptykLocalBlockchain();
                nodeDatabase = InitializeNewNode();

                using(var streamWriter = new StreamWriter(_nodeDbFile))
                {
                    streamWriter.WriteLine(JsonConvert.SerializeObject(nodeDatabase));
                }

            }
            if(!string.IsNullOrEmpty(nodeDatabase.PrivateKeyEncrypted))
            {
                _isNodeSet = true;
            }
            _cacheServiceProvider.LoadNewNodeConfigInCache(nodeDatabase);
        }

        public void SetNewEncyptedPrivateKey(string encryptedPrivateKey)
        {
            if (IsNodeSet) throw new System.Exception("Node already set!");
            if (!File.Exists(_nodeDbFile)) throw new System.Exception("Node local data cannot be found");

            NodeDatabase nodeDatabase = null;
            using (var streamReaded = new StreamReader(_nodeDbFile))
            {
                var nodeDatabaseAsString = streamReaded.ReadToEnd();
                 nodeDatabase = JsonConvert.DeserializeObject<NodeDatabase>(nodeDatabaseAsString);
            }

            nodeDatabase.PrivateKeyEncrypted = encryptedPrivateKey;
            using(var streamWritter = new StreamWriter(_nodeDbFile))
            {
                streamWritter.WriteLine(JsonConvert.SerializeObject(nodeDatabase));
            }
            _cacheServiceProvider.LoadNewNodeConfigInCache(nodeDatabase);

        }

        public bool IsNodeSet { get { return _isNodeSet; } }

        public bool LoadPrivateKeyInMemory(string password)
        {

            return false;  
        }

        private NodeDatabase InitializeNewNode()
        {
            return new NodeDatabase()
            {
                NodeId = NodeIdGenerator.GenerateNodeId(),
                ConnectionIP = $"{_nodeConfiguration.NodeIp}:{_nodeConfiguration.NodePort}"
            };
        }

    }
}
