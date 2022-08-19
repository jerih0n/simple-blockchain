using Blockchain.Node.Logic.LocalConnectors;
using Blockchain.Utils.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Node.CLI.Controllers
{
    [ApiController]
    [Route("/ping")]
    public class HealtCheckController : ControllerBase
    {
        private readonly NodeLocalDataConnector _nodeLocalDataConnector;

        public HealtCheckController(NodeLocalDataConnector nodeLocalDataConnector)
        {
            _nodeLocalDataConnector = nodeLocalDataConnector;
        }

        [HttpGet]
        public IActionResult Test()
        {
            var nodeId = _nodeLocalDataConnector.GetNodeId();
            return new OkObjectResult(new NodeConnectionModel { NodeId = nodeId });
        }
    }
}