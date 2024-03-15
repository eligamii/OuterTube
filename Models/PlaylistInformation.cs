using Newtonsoft.Json.Linq;
using OuterTube.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class PlaylistInformation
    {
        internal static PlaylistInformation FromJson(string json)
        {
            dynamic obj = JObject.Parse(json);
            dynamic details = obj.header.musicDetailHeaderRenderer;

            PlaylistInformation info = new();
            try 
            { 
                info.Description = details.description.runs[0].text;
            }
            catch { }
            try
            {
                info.SecondSubtitle = string.Concat(((JArray)details.secondSubtitle.runs).Select(p => ((dynamic)p).text));
            }
            catch { }
            foreach(dynamic item in obj.contents
                                      .singleColumnBrowseResultsRenderer
                                      .tabs[0]
                                      .tabRenderer
                                      .content
                                      .sectionListRenderer
                                      .contents[0]
                                      .musicPlaylistShelfRenderer
                                      .contents)
            {
                info.Medias.Add(YoutubeMedia.FromMusicResponsiveListItemRenderer(item.musicResponsiveListItemRenderer));
            }

            return info;
        }

        public void Update(ref YoutubePlaylist playlist)
        {
            playlist.Description = Description;
            playlist.SecondSubtitle = SecondSubtitle;
            playlist.Items = Medias;
        }

        public string SecondSubtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<YoutubeMedia> Medias { get; set; } = new();

    }
}
