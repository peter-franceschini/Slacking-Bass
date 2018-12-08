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
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Web;

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
        public async Task<IActionResult> Post()
        {
            var requestBody = ReadAsString(Request.Body).Result;

            var slashCommandPayload = new SlashCommandPayload(requestBody);

            // Verify request signature
            var slackSigningUtil = new SlackSigningUtil();
            if (!slackSigningUtil.SignatureValid(Request.Headers["X-Slack-Signature"], Request.Headers["X-Slack-Request-Timestamp"], requestBody))
            {
                return BadRequest();
            }

            // Dispatch to NotificationService
            AlexaNotificationService.ProcessCommand(slashCommandPayload);

            // Send response to Slack
            var slashCommandResponse = new SlashCommandResponse() { ResponseType = "in_channel", Text = "Message sent to Billy Bass" };

            return Ok(slashCommandResponse);
        }

        private async Task<string> ReadAsString(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
