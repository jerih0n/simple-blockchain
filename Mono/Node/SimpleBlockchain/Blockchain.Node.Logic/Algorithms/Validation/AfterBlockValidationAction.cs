
namespace Blockchain.Node.Logic.Algorithms.Validation
{
    public enum AfterBlockValidationAction
    {
        Accept = 1,
        Reject = 2,
        RequestChainSynchronization = 4
    }
}
