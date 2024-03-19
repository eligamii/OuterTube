using OuterTube.Enums;
using OuterTube.Models.Media.Collections;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace OuterTube
{
    /// <summary>
    /// everything used in both Youtube and Youtube Music are stored here
    /// </summary>
    public static class Shared
    {
        internal static HttpClient HttpClient { get; set; } = CreateHttpClient();
        internal const string RADIO_PLAYLIST_PREFIX = "RDAMVM";
        internal const string MY_MIX_PLAYLIST = "RDMM";

        internal const string MUSIC_BASE_URL = "music";
        internal const string BASE_URL = "www";

        internal const string MUSIC_WEB_APIKEY = "AIzaSyC9XL3ZjWddXya6X74dJoCTL-WEYFDNX30";
        internal const string MUSIC_IOS_APIKEY = "AIzaSyBAETezhkwP0ZWA02RsqT1zu78Fpt0bC_s";
        internal const string WEB_APIKEY = "AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8";
        internal const string IOS_APIKEY = "AIzaSyB-63vPrdThhKuerbB2N_l7Kwwcxj6yUAc";

        internal static async Task<string> RequestAsync(dynamic payload, Client client, string action, string ctoken = "", string type = "")
        {
            string requestUrl = client.BaseRequestUrl + action + "?key=" + client.ApiKey + "&prettyPrint=false";
            if (ctoken != string.Empty) requestUrl += "&ctoken=" + ctoken + "&continuation=" + ctoken;
            if (type != string.Empty) requestUrl += "&type=" + type;

            string payloadString = payload.ToString();
            StringContent content = new(payloadString, Encoding.UTF8);

            HttpClient.DefaultRequestHeaders.Remove("Referrer");
            HttpClient.DefaultRequestHeaders.Add("Referrer", client.Referrer);

            var response = await HttpClient.PostAsync(requestUrl, content);

            return await response.Content.ReadAsStringAsync();
        }

        private static HttpClient CreateHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate, // Will divide the response time by ~8
                UseCookies = true
            };

            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive"); // Will also highly decrase the response time
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return client;
        }
    }
}
