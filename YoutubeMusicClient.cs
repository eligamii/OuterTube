using Newtonsoft.Json.Linq;
using OuterTube.Enums;
using OuterTube.Parsers;
using OuterTube.Models;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Web;
using System.Diagnostics;
using System.Net.Http.Headers;


namespace OuterTube
{

    public class YoutubeMusicClient
    {
        internal static string _baseUrl = "https://music.youtube.com/youtubei/v1";
        internal static string _webApiKey = "AIzaSyC9XL3ZjWddXya6X74dJoCTL-WEYFDNX30";
        private string _iosApiKey = "AIzaSyBAETezhkwP0ZWA02RsqT1zu78Fpt0bC_s";


        internal static dynamic BaseWebPayload
        {
            get
            {
                CultureInfo ci = CultureInfo.InstalledUICulture;

                dynamic client = new JObject();
                client.clientName = ClientName.WEB_REMIX.ToString();
                client.clientVersion = "1.20220918";
                client.hl = ci.Name.Split("-")[0];
                client.gl = ci.Name.Split("-")[1];

                dynamic context = new JObject();
                context.client = client;

                dynamic payload = new JObject();
                payload.context = context;

                return payload;
            }
        }

        internal static dynamic BaseIOSPayload // To bypass issues when getting the player
        {
            get
            {
                dynamic payload = BaseWebPayload;
                payload.context.client.clientName = ClientName.IOS_MUSIC.ToString();
                payload.context.client.clientVersion = "5.26.1";

                return payload;
            }
        }

        /// <summary>
        /// Return a list of MediaShelf in this specific order
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<MusicSearchResult> SearchAsync(string query, SearchFilter filter)
        {
            // Create the payload for the request
            dynamic payload = BaseWebPayload;
            payload.query = HttpUtility.HtmlEncode(query); // The search parameter
            payload.@params = filter.GetParam();

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            // Create the request url
            string requestUrl = _baseUrl + "/search?key=" + _webApiKey + "&prettyPrint=false";

            Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
            Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

            // Make the actual request
            var response = await Shared.HttpClient.PostAsync(requestUrl, content);

            return new MusicSearchResult(await response.Content.ReadAsStringAsync(), filter);

        }

        public async Task<MediaShelfCollection> GetHomeAsync()
        {
            dynamic payload = BaseWebPayload;
            payload.browseId = "FEmusic_home";

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            string requestUrl = _baseUrl + "/browse?key=" + _webApiKey + "&prettyPrint=false";

            Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
            Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

            // Make the actual request
            var response = await Shared.HttpClient.PostAsync(requestUrl, content);

            string json = await response.Content.ReadAsStringAsync();

            return MediaShelfCollection.FromJson(json);
        }




        public async Task<YoutubePlaylist> GetPlaylistAsync(string id)
        {
            dynamic payload = BaseWebPayload;
            payload.
            payload.playlistId = id;
            payload.isAudioOnly = true;
            payload.@params = "wAEB"; // radio

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            string requestUrl = _baseUrl + "/next?key=" + _webApiKey + "&prettyPrint=false";

            Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
            Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

            // Make the actual request
            var response = await Shared.HttpClient.PostAsync(requestUrl, content);

            string json = await response.Content.ReadAsStringAsync();
            return Playlists.Parse(json, id.StartsWith("RD"));
        }

        public async Task<Player> GetPlayerAsync(YoutubeMedia video) => await GetPlayerAsync(video.Id);

        public async Task<Player> GetPlayerAsync(string videoId)
        {
            
            dynamic payload = BaseIOSPayload;
            payload.videoId = videoId;

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            string requestUrl = _baseUrl + "/player?key=" + _iosApiKey + "&prettyPrint=false";

            Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
            Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

            // Make the actual request
            var response = await Shared.HttpClient.PostAsync(requestUrl, content);

            string json = await response.Content.ReadAsStringAsync();
             
            return new Player(FormatCollection.FromJson(json), json);
        }
    }
}
