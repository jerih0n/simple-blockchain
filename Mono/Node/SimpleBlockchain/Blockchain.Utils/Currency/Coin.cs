

namespace Blockchain.Utils.Currency
{
    public class Coin
    {
        public Coin(long amount)
        {
            Amount = amount;
        }
        public long Amount { get; }
    }
}
