using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackingBass.Models.NotifyMe
{
    public class NotifyMePayload
    {
        [JsonProperty("notification")]
        public string Notification { get; set; }
        [JsonProperty("accessCode")]
        public string AccessCode { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
