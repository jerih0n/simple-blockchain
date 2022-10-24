using Blockchain.Utils;
using System;
using System.Collections.Generic;

namespace Blockchain.Node.Logic.MemoryPool
{
    public class SimpleMemoryPool : IMempool
    {
        private readonly LinkedList<Transaction> _mempoolCore = new LinkedList<Transaction>();
        private readonly List<Transaction> _tempMempool = new List<Transaction>();
        private const int TRANSACTIONS_PER_BLOCK = 20;
        private readonly object _locker = new object();
        private bool IsPoolLocked = false;
        private HashSet<string> _transactionHash = new HashSet<string>();

        public SimpleMemoryPool()
        {
            _mempoolCore = new LinkedList<Transaction>();
        }

        public void PushIntoMempool(Transaction transaction)
        {
            lock (_locker)
            {
                if (IsPoolLocked)
                {
                    return;
                }

                RecordTransactionInMempool(transaction);
            }
        }

        public IList<Transaction> GetFromMempool()
        {
            IsPoolLocked = true;
            int counter = 0;
            List<Transaction> result = new List<Transaction>();

            lock (_locker)
            {
                var first = _mempoolCore.First;
                if (first == null)
                {
                    return result;
                }

                while (counter >= TRANSACTIONS_PER_BLOCK)
                {
                    if (first == null)
                    {
                        return result;
                    }

                    var next = first.Next;
                    result.Add(first.Value);
                    _mempoolCore.Remove(first);
                    _transactionHash.Remove(first.Value.Hash);
                    counter++;
                    first = next;
                }
                IsPoolLocked = false;

                if (_tempMempool.Count > 0)
                {
                    foreach (var pendinTransaction in _tempMempool)
                    {
                        RecordTransactionInMempool(pendinTransaction);
                    }
                    _tempMempool.Clear();
                }
                return result;
            }
        }

        private void RecordTransactionInMempool(Transaction transaction)
        {
            if (_transactionHash.Contains(transaction.Hash))
            {
                Console.WriteLine($"Transaction with hash {transaction.Hash} already exist!!", ConsoleColor.Red);
            }
            var first = _mempoolCore.First;
            if (first == null)
            {
                _mempoolCore.AddFirst(transaction);
                return;
            }
            if (transaction.Fee > first.Value.Fee)
            {
                _mempoolCore.AddBefore(first, transaction);
                return;
            }
            while (true)
            {
                var next = first.Next;
                if (next == null)
                {
                    _mempoolCore.AddAfter(first, transaction);
                }
                if (transaction.Fee > next.Value.Fee)
                {
                    _mempoolCore.AddBefore(next, transaction);
                    break;
                }
            }
        }
    }
}