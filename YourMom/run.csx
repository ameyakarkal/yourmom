#r "Newtonsoft.Json"
using System.Net;
using Newtonsoft.Json;

public static string token = System.Configuration.ConfigurationManager.AppSettings["token"];

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    var data = await req.Content.ReadAsFormDataAsync();

    var cmd  = new SlackCommand{ Text = data["text"], Token = data["token"] };
    
    /* your MOM is NOT open to everyone */
    if(cmd == null || cmd.Token != token){
        return req.CreateResponse(HttpStatusCode.Forbidden, "your mom forbids you.");
    }

    var response = new {
        response_type= "in_channel",
        text = RandomResponse(cmd.Text)     
    };

    return req.CreateResponse(HttpStatusCode.OK, response);
}

public class SlackCommand
{
    public string Text { get; set; }

    public string Token { get; set; }
}

public string RandomResponse(string text)
{
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

    return possibleResponses[r];
}