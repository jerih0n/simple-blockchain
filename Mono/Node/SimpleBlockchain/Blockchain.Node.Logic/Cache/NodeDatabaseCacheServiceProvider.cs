using Blockchain.Node.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Blockchain.Node.Logic.Cache
{ 
    public class NodeDatabaseCacheServiceProvider
    {
        private const string _nodeDatabaseCacheKey = "nodeDatabase";
        private const string _nodeDecryptedPrivateKey = "nodeDecryptedPrivateKey";
        private readonly IMemoryCache _memoryCache;

        public NodeDatabaseCacheServiceProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        public void LoadNewNodeDatabase(NodeDatabase database) => 
            _memoryCache.Set(_nodeDatabaseCacheKey, database, new MemoryCacheEntryOptions
        {
            Priority = CacheItemPriority.NeverRemove
        });

        public void AddPrivateKey(byte[] privateKey)
        {
            _memoryCache.Set(_nodeDecryptedPrivateKey, privateKey, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            });
        }

        public byte[] GetPrivateKey()
        {
            var privateKey = _memoryCache.Get<byte[]>(_nodeDecryptedPrivateKey);
            if(privateKey == null || privateKey.Length != 122)
            {
                throw new System.Exception("Corrupted Private Key");
            }
            return privateKey;
        }
    }
}
