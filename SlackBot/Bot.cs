using System;
using System.Globalization;

namespace SlackBot
{
    public class Bot
    {
        private static readonly string[] Templates = new[]
        {
            "Your mom is {0}",
            "Your nose is {0}",
            "You're {0}",
            "{0}, brah",
            "It'll be great",
            "Great job",
            ":gj:",
            "Let's go for a walk",
            "Ock",
            ":smoothie-bagel-walk:",
            ":whiskey:"
        };

        private static readonly Random Randomizer = new Random(DateTime.Now.ToString(CultureInfo.CurrentCulture).GetHashCode());

        public static SlackResponse Respond(SlackCommand cmd)
        {
            var template = PickTemplate(cmd);

            var text = FormatReply(template, cmd);

            return new SlackResponse
            {
                ResponseType = "in_channel",
                Text = text
            };
        }

        public static string PickTemplate(SlackCommand cmd)
        {
            var r = Randomizer.Next(Templates.Length);

            return Templates[r];
        }

        public static string FormatReply(string template, SlackCommand cmd)
        {
            var reply = string.Format(template, cmd.Text);

            return string.Compare(cmd.ChannelName, "all-caps", StringComparison.InvariantCulture) == 0
                ? reply.ToUpper()
                : reply;
        }
    }
}