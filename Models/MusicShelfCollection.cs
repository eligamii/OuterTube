using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class MediaShelfCollection
    {
        public static MediaShelfCollection FromJson(string json)
        {
            MediaShelfCollection collection = new();
            dynamic jsonObj = JObject.Parse(json);

            foreach(dynamic item in jsonObj.contents
                                              .singleColumnBrowseResultsRenderer
                                              .tabs[0]
                                              .tabRenderer
                                              .content
                                              .sectionListRenderer
                                              .contents)
            {
                if(((JObject)item).ContainsKey("musicCarouselShelfRenderer"))
                    collection.MediaShelves.Add(MediaShelf.FromMusicCarouselShelfRenderer(item.musicCarouselShelfRenderer));
            }

            return collection;
        }

        public List<MediaShelf> MediaShelves { get; set; } = new();
        public string ContinuationToken { get; set; } = string.Empty;
    }
}
