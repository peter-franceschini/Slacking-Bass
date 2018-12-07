using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Slack.Models;
using Newtonsoft.Json;
using Slack.Models.Slack;
using Slack.Services;
using Slack.Utilities.Slack;

namespace Slack.Controllers
{
    [Route("api/[controller]")]
    public class SlashCommandController : Controller
    {
        private AlexaNotificationService AlexaNotificationService { get; set; }

        public SlashCommandController()
        {
            AlexaNotificationService = new AlexaNotificationService();
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult Command(SlashCommandPayload slashCommandPayload)
        {
            // Verify request signature
            var slackSigningUtil = new SlackSigningUtil();
            if(!slackSigningUtil.ValidateSignature(Request.Headers["X-Slack-Signature"], Request.Headers["X-Slack-Request-Timestamp"], slashCommandPayload.Command))
            {
                return BadRequest();
            }

            // Dispatch to NotificationService
            AlexaNotificationService.Notify()

            // Send response to Slack
            var slashCommandResponse = new SlashCommandResponse() { ResponseType = "in_channel", Text = "Message sent to Billy Bass" };
            
            return Ok(slashCommandResponse);
        }
    }
}
