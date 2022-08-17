using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Node.CLI.Controllers
{
    public class TestController : ControllerBase
    {
        [Route("/api/test")]
        public IActionResult Test()
        {
            return new OkObjectResult(new { Fine = "Fine" });
        }
    }
}