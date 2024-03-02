using Newtonsoft.Json.Linq;
using OuterTube.Models;

namespace OuterTube.Parsers
{
    internal class Trending
    {
        public static List<MusicShelf> ParseData(string json)
        {
            List<MusicShelf> musicShelves = [];
            dynamic jObject = JObject.Parse(json);

            foreach(dynamic section in jObject
                                       .contents
                                       .singleColumnBrowseResultsRenderer
                                       .tabs[0]
                                       .tabRenderer
                                       .content
                                       .sectionListRenderer
                                       .contents)
            {
                try
                {
                    MusicShelf shelf = new();

                    shelf.Title = section
                                   .musicCarouselShelfRenderer
                                   .header
                                   .musicCarouselShelfBasicHeaderRenderer
                                   .title
                                   .runs[0]
                                   .text;

                    shelf.Subtitle = section
                                     .musicCarouselShelfRenderer
                                     .header
                                     .musicCarouselShelfBasicHeaderRenderer
                                     .strapline
                                     .runs[0]
                                     .text;

                    foreach (JObject item in section
                                            .musicCarouselShelfRenderer
                                            .contents)
                    {
                        if (item.ContainsKey("musicResponsiveListItemRenderer"))
                        {
                            shelf.Items.Add(Media.Parse(item));

                        }
                    }

                    musicShelves.Add(shelf);
                }
                catch { }
            }

            return musicShelves;
        }
    }
}
