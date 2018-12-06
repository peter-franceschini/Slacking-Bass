using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slack.Models
{
    public class SlashCommandReply
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public List<Dictionary<string, string>> Attachments { get; set; }
    }
}
