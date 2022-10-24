using Blockchain.Utils;
using System.Collections.Generic;

namespace Blockchain.Node.Logic.MemoryPool
{
    public interface IMempool
    {
        void PushIntoMempool(Transaction transaction);

        IList<Transaction> GetFromMempool();
    }
}