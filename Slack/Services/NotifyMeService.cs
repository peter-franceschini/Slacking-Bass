using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Slack.Models.NotifyMe;
using System.Net.Http;
using System.Text;

namespace Slack.Services
{
    public class NotifyMeService : INotificationService
    {
        private static readonly HttpClient client = new HttpClient();
        private NotifyMeSettings NotifyMeSettings { get; set; }

        public NotifyMeService(IOptions<NotifyMeSettings> notifyMeSettings)
        {
            NotifyMeSettings = notifyMeSettings.Value;
        }

        public bool Notify(string title, string body)
        {
            var payload = new NotifyMePayload()
            {
                AccessCode = NotifyMeSettings.AccessCode,
                Notification = body,
                Title = title
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://api.notifymyecho.com/v1/NotifyMe", content).Result;

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
