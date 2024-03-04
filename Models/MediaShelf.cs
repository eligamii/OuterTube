using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class MediaShelf : YoutubeElement
    {
        public static MediaShelf FromMusicCarouselShelfRenderer(dynamic musicCarouselShelfRenderer)
        {
            MediaShelf shelf = new();

            try
            {
                shelf.Title = musicCarouselShelfRenderer.header.musicCarouselShelfBasicHeaderRenderer.title.runs[0].text;
                shelf.Subtitle = musicCarouselShelfRenderer.header.musicCarouselShelfBasicHeaderRenderer.strapline.runs[0].text;
            }
            catch { }

            foreach(dynamic obj in musicCarouselShelfRenderer.contents)
            {
                JObject jObj = (JObject)obj;

                if (jObj.ContainsKey("musicResponsiveListItemRenderer"))
                    shelf.Items.Add(YoutubeMedia.FromMusicResponsiveListItemRenderer(obj.musicResponsiveListItemRenderer));
                else if (jObj.ContainsKey("musicTwoRowItemRenderer"))
                    shelf.Items.Add(YoutubePlaylist.FromMusicTwoRowItemRenderer(obj.musicTwoRowItemRenderer));
            }

            return shelf;
        }

        public List<YoutubeElement> Items { get; set; } = new();
    }
}
