using OuterTube.Enums;
using OuterTube.Models.Media.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube
{
    public class YoutubeClient
    {
        private readonly static RequestTarget _target = RequestTarget.YoutubeMusic;

        public async Task<SearchResult> GetHomepageAsync(string query)
        {
            dynamic payload = _target.WebClient.BaseClientPayload;
            payload.browseId = "FEwhat_to_watch";

            string json = await Shared.RequestAsync(payload, _target.WebClient, "browse");

            return null;
        }
    }
}
