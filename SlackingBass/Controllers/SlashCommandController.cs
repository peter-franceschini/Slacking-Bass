using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlackingBass.Models.Slack;
using SlackingBass.Services;
using SlackingBass.Utilities.Slack;
using System.IO;
using Microsoft.Extensions.Options;
using SlackingBass.Models.NotifyMe;
using SlackingBass.Utilities;

namespace SlackingBass.Controllers
{
    [Route("api/[controller]")]
    public class SlashCommandController : Controller
    {
        private SlackSettings SlackSettings { get; set; }
        private NotifyMeSettings NotifyMeSettings { get; set; }
        private AlexaNotificationService AlexaNotificationService { get; set; }
        private INotificationService NotificationService { get; set; }

        public SlashCommandController(IOptions<SlackSettings> slackSettings, IOptions<NotifyMeSettings> notifyMeSettings, INotificationService notificationService)
        {
            SlackSettings = slackSettings.Value;
            NotifyMeSettings = notifyMeSettings.Value;
            NotificationService = notificationService;
            AlexaNotificationService = new AlexaNotificationService(NotificationService);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post()
        {
            var streamHelper = new StreamHelper();
            var requestBody = streamHelper.ReadAsString(Request.Body).Result;

            var slashCommandPayload = new SlashCommandPayload(requestBody);

            // Verify request signature
            var slackSigningUtil = new SlackSigningUtil();
            if (!slackSigningUtil.SignatureValid(Request.Headers["X-Slack-Signature"], Request.Headers["X-Slack-Request-Timestamp"], requestBody, SlackSettings.SignatureSecret))
            {
                return BadRequest();
            }

            // Dispatch to NotificationService
            var success = AlexaNotificationService.ProcessCommand(slashCommandPayload);

            // Send response to Slack
            SlashCommandResponse slashCommandResponse;
            if (success)
            {
                slashCommandResponse = new SlashCommandResponse() { ResponseType = "in_channel", Text = "Message sent to Billy Bass" };
            }
            else
            {
                slashCommandResponse = new SlashCommandResponse() { ResponseType = "ephemeral", Text = "Error sending message to Billy Bass" };
            }

            return Ok(slashCommandResponse);
        }
    }
}
