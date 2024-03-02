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
        private string _baseUrl = "https://music.youtube.com/youtubei/v1";
        private string _webApiKey = "AIzaSyC9XL3ZjWddXya6X74dJoCTL-WEYFDNX30";
        private string _iosApiKey = "AIzaSyBAETezhkwP0ZWA02RsqT1zu78Fpt0bC_s";


        private dynamic BaseWebPayload
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

        private dynamic BaseIOSPayload // To bypass issues when getting the player
        {
            get
            {
                dynamic payload = BaseWebPayload;
                payload.context.client.clientName = ClientName.IOS_MUSIC.ToString();
                payload.context.client.clientVersion = "5.26.1";

                return payload;
            }
        }


        public async Task<SearchResult> SearchAsync(string query)
        {
            // Create the payload for the request
            dynamic payload = BaseWebPayload;
            payload.query = HttpUtility.HtmlEncode(query); // The search parameter

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            // Create the request url
            string requestUrl = _baseUrl + "/search?key=" + _webApiKey + "&prettyPrint=false";

            Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
            Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

            // Make the actual request
            var response = await Shared.HttpClient.PostAsync(requestUrl, content);

            return new SearchResult(Search.Parse(await response.Content.ReadAsStringAsync()), "");

        }

        public async Task<List<MusicShelf>> GetHomeAsync()
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

            return Trending.ParseData(json);
        }




        public async Task<Playlist> GetPlaylistAsync(string id)
        {
            dynamic payload = BaseWebPayload;
            payload.
            payload.playlistId = id;
            payload.isAudioOnly = true;
            payload.@params = "wAEB";

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

        public async Task<List<string>> GetPlayerAsync(YoutubeMedia video) => await GetPlayerAsync(video.Id);

        public async Task<List<string>> GetPlayerAsync(string videoId)
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
            return StreamingData.ParseForAudioOnly(json);
        }
    }
}
