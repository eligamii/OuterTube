using Newtonsoft.Json.Linq;
using OuterTube.Enums;
using OuterTube.Exceptions;
using OuterTube.Models;
using OuterTube.Models.Media;
using OuterTube.Models.Media.Collections;
using OuterTube.Models.MediaInformation.Collections;
using System.Globalization;
using System.Text;
using System.Web;


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
        /// Search on Youtube Music.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A list of MediaShelf in this specific order</returns>
        public async Task<MusicSearchResult> SearchAsync(string query, MusicSearchFilter filter = MusicSearchFilter.Titles)
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

        /// <summary>
        /// Get the history of a user connected with the Shared.SetCookie(...) method. 
        /// </summary>
        /// <returns>The history of the user</returns>
        /// <exception cref="NotSignedInException">No connection cookie has been added with the Shared.SetCookie(...) method.</exception>
        private async Task<List<YoutubeMedia>> GetHistoryAsync()
        {
            dynamic payload = BaseWebPayload;
            payload.browseId = "FEmusic_history";

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            string requestUrl = _baseUrl + "/browse?key=" + _webApiKey + "&prettyPrint=false";

            Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
            Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

            byte[] buffer = Encoding.UTF8.GetBytes(payloadString);
            ByteArrayContent byteContent = new(buffer);

            HttpRequestMessage request = new(HttpMethod.Post, requestUrl);
            request.Content = byteContent;
            request.Headers.Add("Cookie", Shared.CookieHeader);
            request.Headers.Add("Authorization", Shared.SetAuthHeader());


            var response = await Shared.HttpClient.SendAsync(request);

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                string message = "You must add your Google Account connection cookies to OuterTube to perform this operation.\n" +
                "To add Google Account connection cookies, please use the `OuterTube.Shared.AddCookie(string name, string value)` method.\n\n" +
                "Note: On WinForm, WPF, UWP and WinUI3 apps, you can navigate to the Youtube sign in url then use CoreWebView2.CookieManager.GetCookiesAsync(\"https://www.youtube.com\") on a WebView2 to get the cookies. See more on the OuterTube GitHub.";

                throw new NotSignedInException(message);
            }

            string json = await response.Content.ReadAsStringAsync();

            return YoutubeMedia.ListFromJson(json);
        }

        /// <summary>
        /// Get the homepage of the Youtube Music website
        /// </summary>
        /// <returns>The homepage as a collection of MusicShelf objects</returns>
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

        /// <summary>
        /// Get additional information about a playlist.
        /// </summary>
        /// <param name="playlist">The playlist to get infomation from.</param>
        /// <returns>A PLaylistInfromation object containing the additional information of the playlist.</returns>
        public async Task<PlaylistInformation> GetPlaylistInfoAsync(YoutubePlaylist playlist)
        {
            dynamic payload = BaseWebPayload;
            payload.browseId = playlist.Id;

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            string requestUrl = _baseUrl + "/browse?key=" + _webApiKey + "&prettyPrint=false";

            Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
            Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

            // Make the actual request
            var response = await Shared.HttpClient.PostAsync(requestUrl, content);

            string json = await response.Content.ReadAsStringAsync();
            return PlaylistInformation.FromJson(json);
        }

        /// <summary>
        /// Get the playlist assiociated with the given id
        /// </summary>
        /// <param name="id">The videoId of the playlist</param>
        /// <returns></returns>
        public async Task<YoutubePlaylist> GetPlaylistAsync(string id)
        {
            dynamic payload = BaseWebPayload;
            payload.playlistId = id;
            payload.isAudioOnly = true;
            payload.@params = id.StartsWith("RD") ? "wAEB" : "MPREb_XLbJEEU4CpR"; 

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            string requestUrl = _baseUrl + "/next?key=" + _webApiKey + "&prettyPrint=false";

            Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
            Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

            // Make the actual request
            var response = await Shared.HttpClient.PostAsync(requestUrl, content);

            string json = await response.Content.ReadAsStringAsync();
            return YoutubePlaylist.FromJson(json, id.StartsWith("RD"));
        }

        /// <summary>
        /// Get the player of a media.
        /// </summary>
        /// <param name="video">The media to get the player.</param>
        /// <returns>A Player object containing the list of available formats and additionnal information about the media</returns>
        public async Task<Player> GetPlayerAsync(YoutubeMedia video) => await GetPlayerAsync(video.Id);

        /// <summary>
        /// Get the player of a media.
        /// </summary>
        /// <param name="videoId">The id of the media to get the player.</param>
        /// <returns>A Player object containing the list of available formats and additionnal information about the media</returns>
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
