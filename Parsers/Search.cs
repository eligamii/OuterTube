using Newtonsoft.Json.Linq;
using OuterTube.Models;

namespace OuterTube.Parsers
{
    internal static class Search
    {
        // This entire code is from the YoutubeSearchApi 
        internal static List<YoutubeMedia> Parse(string pageContent)
        {
            var videos = new List<YoutubeMedia>();

            dynamic jsonObject = JObject.Parse(pageContent);
            dynamic contents = jsonObject.contents.tabbedSearchResultsRenderer.tabs[0].tabRenderer.content.sectionListRenderer.contents[0];

            if (contents.ContainsKey("musicShelfRenderer"))
            {
                foreach (JObject videoJson in contents.musicShelfRenderer.contents)
                {
                    videos.Add(Media.Parse(videoJson));
                }
            }

            return videos;
        }
    }
}
