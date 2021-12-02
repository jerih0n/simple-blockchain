using Blockchain.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace Blockchain.Node.Logic.Cache
{
    public class BlockchainCacheServiceProvider
    {
        private readonly IMemoryCache _memoryCache;
        private const string _blockchainCacheKey = "localBlockchain";
        public BlockchainCacheServiceProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void LoadBlockchainInMemory(BlockchainModel blockchain)
        {
            var cachedBlockchain = _memoryCache.Get(_blockchainCacheKey) as BlockchainModel;

            if(cachedBlockchain != null)
            {
                return;
            }

            _memoryCache.Set(_blockchainCacheKey, blockchain, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            });
        }

        public BlockchainModel GetEntireBlockchain()
        {
            return _memoryCache.Get(_blockchainCacheKey) as BlockchainModel;
        }

        public Block GetLastBlock()
        {
            var blockchain = GetEntireBlockchain();
            if(blockchain != null && blockchain.Blocks != null && blockchain.Blocks.Count > 0)
            {
                return blockchain.Blocks[blockchain.Blocks.Count - 1];
            }
            return null;
        }

        public Block InsertNewBlock(Block block)
        {
            var blockchain = GetEntireBlockchain();

            if (blockchain != null && blockchain.Blocks != null)
            {
                blockchain.Blocks.Add(block);
                UpdateCache(blockchain);

                return block;
            }

            throw new System.Exception("Insertion Failed!");
        }

        private void UpdateCache(BlockchainModel blockchain)
        {
            _memoryCache.Set(_blockchainCacheKey, blockchain, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            });
        }
    }
}
