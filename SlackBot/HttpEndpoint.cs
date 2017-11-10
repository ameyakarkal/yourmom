using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bot.Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace SlackBot
{
    public static class HttpEndpoint
    {
        private static string SecretToken = ConfigurationManager.AppSettings["token"];

        [FunctionName("JaredBotEndpoint")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "POST", Route = "bot")]HttpRequestMessage req, TraceWriter log)
        {
            var data = await req.Content.ReadAsFormDataAsync();

            var token = data["token"];

            var cmd = new SlackCommand
            {
                Text = data["text"],
                ChannelName = data["channel_name"],
                UserId = data["user_id"],
                UserName = data["user_name"]
            };

            /* your MOM is NOT open to everyone */
            if (token != SecretToken)
            {
                return req.CreateResponse(HttpStatusCode.Forbidden, "your mom forbids you.");
            }

            var response = new
            {
                response_type = "in_channel",
                text = RandomResponse(cmd),
                icon_emoji = RandomEmoji()
            };

            //await Collector.Collect(cmd);

            return req.CreateResponse(HttpStatusCode.OK, response);
        }

        public static string RandomResponse(SlackCommand cmd)
        {
            var text = cmd.Text;

            var possibleResponses = new[]
            {
                $"Your mom is {text}",
                $"Your nose is {text}",
                $"You're {text}",
                $"{text}, brah",
                "It'll be great",
                "Great job",
                ":gj:",
                "Let's go for a walk",
                "Ock",
                ":smoothie-bagel-walk:",
                ":whiskey:"
            };

            var rand = new Random(DateTime.Now.ToString().GetHashCode());

            var r = rand.Next(possibleResponses.Length);

            var response = possibleResponses[r];

            return string.Compare(cmd.ChannelName, "all-caps", StringComparison.InvariantCulture) == 0 ? response.ToUpper() : response;
        }

        public static string RandomEmoji()
        {
            var emojiList = new[]
            {
                ":jared:",
                ":jared-fancy:",
                ":jared-farmer:",
                ":jared-anime-pants:",
                ":jared-optimist-prime:"
            };

            var rand = new Random(DateTime.Now.ToString().GetHashCode());

            var r = rand.Next(emojiList.Length);

            return emojiList[r];
        }
    }
}
