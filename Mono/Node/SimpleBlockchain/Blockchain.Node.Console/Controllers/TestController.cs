using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Node.CLI.Controllers
{
    [ApiController]
    [Route("/api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("get-it")]
        public IActionResult Test()
        {
            return new OkObjectResult(new { Success = true, Message = "LALALAL" });
        }
    }
}