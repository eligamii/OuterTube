using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OuterTube.Enums;

namespace OuterTube
{
    /// <summary>
    /// Objects used by OuterTube to perform requests.
    /// Can be an HttpCLient, a WebView2, or anything else.
    /// </summary>
    public abstract class RequestsClient
    {
        public abstract Task<string> POSTAsync(string endpoint, string payload);
        public abstract void SetReferrer(string refferer);
        public abstract void SetAuthentificationCookies(LoginData? loginData = null, VisitorData? visitorData = null);
    }

    public class HttpClientWrapper : RequestsClient
    {
        private LoginData? loginData = null;
        private VisitorData? visitorData = null;

        private readonly HttpClient _httpClient = CreateHttpClient();
        private static HttpClient CreateHttpClient()
        {
            HttpClientHandler handler = new()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate, // Will divide the response time by ~8
                UseCookies = true
            };

            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive"); // Will also highly decrase the response time
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return client;
        }

        public override async Task<string> POSTAsync(string endpoint, string payload)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, endpoint);
            message.Content = new StringContent(payload, Encoding.UTF8);

            if (loginData != null)
            {
                var cookies = loginData.GetCookiesList().Where(p => p.Value is not null).Select(p => p.ToString());
                message.Headers.Add("Cookie", string.Join(null, cookies));
            }
            else if (visitorData != null) 
            {
                var cookies = visitorData.GetCookiesList().Where(p => p.Value is not null).Select(p => p.ToString());
                message.Headers.Add("Cookie", string.Join(null, cookies));
            }

            var response = await _httpClient.SendAsync(message);
            return await response.Content.ReadAsStringAsync();
        }

        public override void SetAuthentificationCookies(LoginData? loginData = null, VisitorData? visitorData = null)
        {
            this.loginData = loginData;
            this.visitorData = visitorData;
        }

        public override void SetReferrer(string referrer)
        {
            _httpClient.DefaultRequestHeaders.Remove("Referrer");
            _httpClient.DefaultRequestHeaders.Add("Referrer", referrer);
        }
    }
}
