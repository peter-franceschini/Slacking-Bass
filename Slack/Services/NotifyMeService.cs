using Newtonsoft.Json;
using Slack.Models.NotifyMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services
{
    public class NotifyMeService : INotificationService
    {
        private static readonly HttpClient client = new HttpClient();

        public bool Notify(string title, string body)
        {
            var payload = new NotifyMePayload()
            {
                AccessCode = "",
                Notification = body,
                Title = title
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://api.notifymyecho.com/v1/NotifyMe", content).Result;

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
