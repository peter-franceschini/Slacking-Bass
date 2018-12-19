using SlackingBass.Models.Slack;
using System;

namespace SlackingBass.Services
{
    public class AlexaNotificationService
    {
        private INotificationService NotificationService { get; set; }

        public AlexaNotificationService(INotificationService notificationService)
        {
            NotificationService = notificationService;
        }

        public bool ProcessCommand(SlashCommandPayload slashCommandPayload)
        {
            var notificationMessage = BuildNotificationMessage(slashCommandPayload);

            return NotificationService.Notify($"Message from {slashCommandPayload.UserName}", notificationMessage);
        }

        private string BuildNotificationMessage(SlashCommandPayload slashCommandPayload)
        {
            var message = $"Message from {slashCommandPayload.UserName} at {DateTime.Now.ToShortTimeString()}... {slashCommandPayload.Text}";
            return message;
        }
    }
}
