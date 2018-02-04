using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackBot
{
    public class SlackResponse
    {
        public SlackResponse(string text, string imageUrl)
        {
            Attachments = new List<Attachment>
            {
                new Attachment
                {
                    ImageUrl = imageUrl,
                    Text = text
                }
            };
        }

        protected SlackResponse() { }

        [JsonProperty(PropertyName = "response_type")]
        public string ResponseType => "in_channel";

        [JsonProperty(PropertyName = "attachments")]
        public List<Attachment> Attachments { get; }
    }

    public class Attachment
    {
        [JsonProperty("fallback")]
        public string Fallback => "Jared";

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("color")]
        public string Color => "#764F45";

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
