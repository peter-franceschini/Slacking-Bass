using Microsoft.AspNetCore.Mvc;

namespace Slack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: Test
        [HttpGet]
        public string Get()
        {
            return "test";
        }
    }
}