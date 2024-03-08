using System.Net;
using System.Net.Http.Headers;

namespace OuterTube
{
    /// <summary>
    /// everything used in both Youtube and Youtube Music are stored here
    /// </summary>
    internal static class Shared
    {
        internal static HttpClient HttpClient { get; set; } = CreateHttpClient();
        private static CookieContainer _cookieContainer;


        public const string RADIO_PLAYLIST_PREFIX = "RDAMVM";
        public const string MY_MIX_PLAYLIST = "RDMM";

        public static void SetConnectionCokies(string __Secure_3PSIDCC, string __Secure_1PSIDCC)
        {
            var __Secure_3PSIDCC_Cookie = new Cookie("__Secure-3PSIDCC", __Secure_3PSIDCC, "/", ".youtube.com")
            {
                Secure = true,
                HttpOnly = true,
            };

            var __Secure_1PSIDCC_Cookie = new Cookie("__Secure-1PSIDCC", __Secure_1PSIDCC, "/", ".youtube.com")
            {
                Secure = true,
                HttpOnly = true,
            };

            _cookieContainer.Add([__Secure_1PSIDCC_Cookie, __Secure_3PSIDCC_Cookie]);
        }


        // Will add compression, cookies and Chrome user agent to divide the response time by 8
        private static HttpClient CreateHttpClient()
        {
            _cookieContainer = new();

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                CookieContainer = _cookieContainer,
                UseCookies = true
            };

            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return client;
        }
    }
}
