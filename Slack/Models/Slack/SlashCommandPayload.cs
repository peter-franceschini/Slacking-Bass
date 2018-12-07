using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slack.Models.Slack
{
    public class SlashCommandPayload
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("team_id")]
        public string Team_Id { get; set; }

        [JsonProperty("team_domain")]
        public string Team_Domain { get; set; }

        [JsonProperty("channel_id")]
        public string Channel_Id { get; set; }

        [JsonProperty("channel_name")]
        public string Channel_Name { get; set; }

        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [JsonProperty("user_name")]
        public string User_Name { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("response_url")]
        public string Response_Url { get; set; }
    }
}
