#r "Newtonsoft.Json"
#r "../dist/Bot.Extensions.dll"
using System;
using System.Net;
using Newtonsoft.Json;
using Bot.Extensions;

public static string token = System.Configuration.ConfigurationManager.AppSettings["token"];

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
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
    if(token != token){
        return req.CreateResponse(HttpStatusCode.Forbidden, "your mom forbids you.");
    }

    var response = new {
        response_type= "in_channel",
        text = RandomResponse(cmd),
        username = RandomEmoji()     
    };

    await Collector.Collect(cmd);

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
        "jared",
        "jared-fancy",
        "jared-farmer",
        "jared-anime-pants",
        "jared-optimist-prime"
    };

    var rand = new Random(DateTime.Now.ToString().GetHashCode());

    var r = rand.Next(emojiList.Length);

    return emojiList[r];
}