using Blockchain.Node.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Blockchain.Node.Logic.Cache
{ 
    public class NodeDatabaseCacheServiceProvider
    {
        private const string _nodeDatabaseCacheKey = "nodeDatabase";
        private readonly IMemoryCache _memoryCache;

        public NodeDatabaseCacheServiceProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        public void LoadNewNodeConfigInCache(NodeDatabase database) => 
            _memoryCache.Set(_nodeDatabaseCacheKey, database, new MemoryCacheEntryOptions
        {
            Priority = CacheItemPriority.NeverRemove
        });

    }
}
