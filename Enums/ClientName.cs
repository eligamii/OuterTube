using Newtonsoft.Json.Linq;
using OuterTube.Interfaces;
using System.Globalization;
using System.Runtime.InteropServices;

namespace OuterTube.Enums
{
    internal enum ClientName
    {
        WEB_REMIX = 67,
        IOS_MUSIC = 21,
        WEB = 1,
        IOS = 5
    }

    public class RequestTarget
    {
        internal RequestTarget(Client web, Client ios)
        {
            WebClient = web;
            IOSClient = ios;
        }

        internal Client WebClient { get; set; } = new();
        internal Client IOSClient { get; set; } = new();

        public static RequestTarget Youtube => new(Client.Web, Client.IOS);
        public static RequestTarget YoutubeMusic => new(Client.WebMusic, Client.IOSMusic);
    }

    public class Client
    {
        internal Client() { }
        internal ClientName Name { get; set; }
        internal string ApiKey { get; set; } = string.Empty;
        internal string Base { get; set; } = string.Empty;
        internal string Referrer => $"https://{Base}.youtube.com";
        internal string BaseRequestUrl => $"{Referrer}/youtubei/v1/";
        internal string Version { get; set; } = "1.0";
        internal dynamic BaseClientPayload
        {
            get
            {
                dynamic payload = BasePayload;
                payload.context.client.clientName = Name.ToString();
                payload.context.client.clientVersion = Version;

                return payload;
            }
        }
        private static dynamic BasePayload
        {
            get
            {
                CultureInfo ci = CultureInfo.InstalledUICulture;

                dynamic client = new JObject();
                client.hl = ci.Name.Split("-")[0];
                client.gl = ci.Name.Split("-")[1];

                dynamic context = new JObject();
                context.client = client;

                dynamic payload = new JObject();
                payload.context = context;

                return payload;
            }
        }

        internal static Client IOS => new Client()
        {
            Name = ClientName.IOS,
            ApiKey = Shared.IOS_APIKEY,
            Base = Shared.BASE_URL,
            Version = "17.36.4"

        };

        internal static Client IOSMusic => new Client()
        {
            Name = ClientName.IOS_MUSIC,
            ApiKey = Shared.MUSIC_IOS_APIKEY,
            Base = Shared.MUSIC_BASE_URL,
            Version = "5.26.1"
        };

        internal static Client Web => new Client()
        {
            Name = ClientName.WEB,
            ApiKey = Shared.WEB_APIKEY,
            Base = Shared.BASE_URL,
            Version = "2.20220918"
        };

        internal static Client WebMusic => new Client()
        {
            Name = ClientName.WEB_REMIX,
            ApiKey = Shared.MUSIC_WEB_APIKEY,
            Base = Shared.MUSIC_BASE_URL,
            Version = "1.20220918"
        };
    }
}
