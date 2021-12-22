
namespace Blockchain.Node.Configuration
{
    public class NodeDatabase
    {
        public string NodeId { get; set; }
        public short Sex { get; set; } //HA :D 0 for male 1 for female, generated at random, determinating the read/writte network chanels
        public string PrivateKeyEncrypted { get; set; }
    }
}
