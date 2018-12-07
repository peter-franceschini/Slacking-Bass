using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slack.Models.Slack
{
    public class SlashCommandResponse
    {
        [JsonProperty("response_type")]
        public string ResponseType { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public List<Dictionary<string, string>> Attachments { get; set; }
    }
}
