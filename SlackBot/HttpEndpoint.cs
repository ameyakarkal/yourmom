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

            var response = Bot.Respond(cmd);

            await Collector.Collect(cmd);

            return req.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
