using OuterTube.Enums;

namespace OuterTube
{
    /// <summary>
    /// Everything used in both Youtube and Youtube Music are stored here
    /// </summary>
    public static class Shared
    {
        public static RequestsClient RequestsClient { get; set; } = new HttpClientWrapper();

        /// <summary>
        /// This does not work
        /// </summary>
        public static void SetCookies(LoginData? loginData = null, VisitorData? visitorData = null)
        {
            if (loginData is null && visitorData is null) throw new ArgumentNullException();
            RequestsClient.SetAuthentificationCookies(loginData, visitorData);
        }

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

            RequestsClient.SetReferrer(client.Referrer);
            string response = await RequestsClient.POSTAsync(requestUrl, payload.ToString());

            return response;
        }
    }
}
