using Blockchain.Networking.Server;
using Blockchain.Node.Logic.MemoryPool;
using Blockchain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Node.CLI.Controllers
{
    [ApiController]
    [Route("/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly NetworkConnectionService _networkConnectionService;
        private readonly IMempool _mempool;

        public TransactionsController(NetworkConnectionService networkConnectionService, IMempool mempool)
        {
            _networkConnectionService = networkConnectionService;
            _mempool = mempool;
        }

        [HttpPost]
        public IActionResult CreateTransaction(Transaction transaction)
        {
            //notify about the new transaction
            _networkConnectionService.PushNewTransaction(transaction);

            //validate

            //record in the Memory Pool

            return Ok();
        }
    }
}