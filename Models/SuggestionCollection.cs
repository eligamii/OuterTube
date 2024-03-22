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

            if(!((JObject)obj).ContainsKey("contents")) return collection;

            foreach (dynamic suggestion in obj.contents[0]
                                              .searchSuggestionsSectionRenderer
                                              .contents)
            {
                collection.TextSuggestions.Add(Suggestion.FromSearchSuggestionRenderer(suggestion.searchSuggestionRenderer));
            }

            if (((JArray)obj.contents).Count() > 1)
            {
                foreach (dynamic suggestion in obj.contents[1]
                                                 .searchSuggestionsSectionRenderer
                                                 .contents)
                {
                    collection.MediaSuggestions.Add(Suggestion.FromMusicResponsiveListItemRenderer(suggestion.musicResponsiveListItemRenderer));
                }
            }

            return collection;
        }

        public List<Suggestion> TextSuggestions { get; set; } = [];
        public List<Suggestion> MediaSuggestions { get; set; } = [];
        public List<Suggestion> Items => [.. TextSuggestions, .. MediaSuggestions];
    }
}
