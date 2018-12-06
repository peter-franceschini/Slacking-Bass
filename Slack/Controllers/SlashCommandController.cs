using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Slack.Models;
using Newtonsoft.Json;

namespace Slack.Controllers
{
    [Route("api/[controller]")]
    public class SlashCommandController : Controller
    {
        [HttpPost]
        [Produces("application/json")]
        public string HandleCommand(SlashCommandPayload slashCommandPayload)
        {
            return JsonConvert.SerializeObject(slashCommandPayload);
        }
    }
}
