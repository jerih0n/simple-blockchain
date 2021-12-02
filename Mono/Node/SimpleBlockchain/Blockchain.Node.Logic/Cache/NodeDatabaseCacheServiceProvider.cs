using Blockchain.Node.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Blockchain.Node.Logic.Cache
{ 
    public class NodeDatabaseCacheServiceProvider
    {
        private const string _nodeDatabaseCacheKey = "nodeDatabase";
        private readonly IMemoryCache _memoryCache;
        private NodeConfiguration _nodeConfiguration;


        public NodeDatabaseCacheServiceProvider(NodeConfiguration nodeConfiguration, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        public void LoadNewNodeConfigInCache(NodeDatabase database)
        {
            var existingNodeDatabase = _memoryCache.Get(_nodeDatabaseCacheKey) as NodeDatabase;

            if(existingNodeDatabase != null)
            {
                return;
            }

            _memoryCache.Set(_nodeDatabaseCacheKey, database, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            });

        }
    }
}
