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

        private static readonly string[] Avatars = new[]
        {
            "https://emoji.slack-edge.com/T0297JM3N/jared-fancy/eb4f5681f7b8c220.png",
            "https://emoji.slack-edge.com/T0297JM3N/jared-anime-pants/89d3ba80c629d106.png",
            "https://emoji.slack-edge.com/T0297JM3N/jared-farmer/6497e05835adf519.png",
            "https://emoji.slack-edge.com/T0297JM3N/jared-optimist-prime/b78e15b1dbfe2adf.png",
            "https://emoji.slack-edge.com/T0297JM3N/jared/d927b5ff3d0c8e41.jpg"
        };

        private static readonly Random Randomizer = new Random(DateTime.Now.ToString(CultureInfo.CurrentCulture).GetHashCode());

        public static SlackResponse Respond(SlackCommand cmd)
        {
            var template = PickTemplate(cmd);

            var text = FormatReply(template, cmd);

            var avatar = PickAvatar();

            return new SlackResponse(text, avatar);
        }

        public static string PickTemplate(SlackCommand cmd)
        {
            var r = Randomizer.Next(Templates.Length);

            return Templates[r];
        }

        public static string PickAvatar()
        {
            var r = Randomizer.Next(Avatars.Length);

            return Avatars[r];
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