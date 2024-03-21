using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class SuggestionCollection
    {
        public static SuggestionCollection FromYoutubeMusicJson(string json)
        {
            SuggestionCollection collection = new();

            dynamic obj = JObject.Parse(json);

            foreach (dynamic suggestion in obj.contents[0]
                                              .searchSuggestionSectionRenderer
                                              .contents)
            {
                collection.TextSuggestions.Add(Suggestion.FromSearchSuggestionRenderer(suggestion));
            }

            if (((JArray)obj.contents).Count() > 1)
            {
                foreach (dynamic suggestion in obj.contents[1]
                                                 .searchSuggestionSectionRenderer
                                                 .contents)
                {
                    collection.MediaSuggestions.Add(Suggestion.FromMusicResponsiveListItemRenderer(suggestion));
                }
            }

            return collection;
        }

        public List<Suggestion> TextSuggestions { get; set; } = [];
        public List<Suggestion> MediaSuggestions { get; set; } = [];
        public List<Suggestion> Items => [.. TextSuggestions, .. MediaSuggestions];
    }
}
