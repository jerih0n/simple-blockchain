using Blockchain.Node.CLI.Models;
using Blockchain.Node.Logic.LocalConnectors;
using Blockchain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Node.CLI.Controllers
{
    [ApiController]
    [Route("/balance")]
    public class BalanceController : ControllerBase
    {
        private readonly BlockchainLocalDataConnector _blockchainLocalDataConnector;

        public BalanceController(BlockchainLocalDataConnector blockchainLocalDataConnector)
        {
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
        }

        [HttpGet("{address}")]
        public IActionResult GetBalance([FromRoute] string address)
        {
            var lastTransaction = _blockchainLocalDataConnector.GetLastTransactionForAddress(address);

            if (lastTransaction == null)
            {
                return new OkObjectResult(new Balance(0));
            }

            return new ObjectResult(new Balance(GetCorrectBalance(lastTransaction, address)));
        }

        private long GetCorrectBalance(Transaction transaction, string address)
        {
            if (transaction.FromAddress.ToLower() == address)
            {
                return transaction.NewState.SenderAmount;
            }
            return transaction.NewState.RecieverAmount;
        }
    }
}