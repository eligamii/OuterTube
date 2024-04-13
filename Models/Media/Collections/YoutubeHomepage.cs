using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models.Media.Collections
{
    /// <summary>
    /// The youtube
    /// </summary>
    public class YoutubeHomepage
    {
        public static YoutubeHomepage FromJson(string json)
        {
            YoutubeHomepage homepage = new();

            dynamic root = JObject.Parse(json);
            dynamic contents = root.contents
                                   .twoColumnBrowseResultsRenderer
                                   .tabs[0]
                                   .tabRenderer
                                   .content
                                   .richGridRenderer
                                   .contents;

            foreach(dynamic item in contents)
            {
                dynamic renderer;

                try
                {
                    renderer =item.richItemRenderer.content;
                }
                catch (RuntimeBinderException)
                {
                    continue;
                }
                JObject rendererJson = (JObject)renderer;

                if (rendererJson.ContainsKey("adSlotRenderer")) continue;

                if (rendererJson.ContainsKey("videoRenderer"))
                    homepage.Videos.Add(YoutubeMedia.FromVideoRenderer(renderer.videoRenderer));
                
                // TODO :
                // Shorts, playlists and contination
            }


            return homepage;
        }
        public List<YoutubeMedia> Videos { get; set; } = [];
        public List<YoutubeMedia> Shorts { get; set; } = [];
        public List<YoutubePlaylist> Playlists { get; set; } = [];
        public List<YoutubeMediaBase> Items => [..Playlists, ..Videos, ..Shorts];
        public string ContinuationToken { get; internal set; } = string.Empty;
    }
}
