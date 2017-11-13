using Newtonsoft.Json;

namespace SlackBot
{
    public class SlackResponse
    {
        [JsonProperty(PropertyName = "response_type")]
        public string ResponseType { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
