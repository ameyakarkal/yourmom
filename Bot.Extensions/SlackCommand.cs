using Newtonsoft.Json;

namespace Bot.Extensions
{
    public class SlackCommand
    {
        public string Text { get; set; }

        public string Token { get; set; }

        [JsonProperty("channel_name")]
        public string ChannelName { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }
    }
}
