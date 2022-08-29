using System.Collections.Generic;

namespace Blockchain.Wallet.Models
{
    public class WalletDb
    {
        public string PrivateKeyEnc { get; set; }
        public List<string> GeneratedAddresess { get; set; } = new List<string>();
    }
}