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
        internal static string CookieHeader { get; set; } = string.Empty;

        private static string s_SAPID = string.Empty;

        internal const string RADIO_PLAYLIST_PREFIX = "RDAMVM";
        internal const string MY_MIX_PLAYLIST = "RDMM";

        public static void SetCookie(string name, string value)
        {
            if (name == "__Secure-3PAPISID") s_SAPID = value;
            if (CookieHeader != string.Empty) CookieHeader += "; "; 
            CookieHeader += $"{name}={value}";
           
        }

        internal static string SetAuthHeader()
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var hashBytes = SHA1.Create() .ComputeHash(Encoding.UTF8.GetBytes($"{timestamp} {s_SAPID} https://www.youtube.com"));
            string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();

            return $"SAPISIDHASH {timestamp}_{hash}";
        }

        // Will add compression, cookies and Chrome user agent to divide the response time by 8
        private static HttpClient CreateHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = true
            };

            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return client;
        }
    }
}
