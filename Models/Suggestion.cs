using Newtonsoft.Json.Linq;
using OuterTube.Models.Media;
using OuterTube.Models.MediaInformation;
using OuterTube.Models.MediaInformation.Collections;

namespace OuterTube.Models
{
    /// <summary>
    /// A Youtube or YT Music suggestion.
    /// </summary>
    public class Suggestion : YoutubeElement
    {
        public static Suggestion FromSearchSuggestionRenderer(dynamic searchSuggestionRenderer)
        {
            Suggestion suggestion = new();

            suggestion.Name = string.Concat(((JArray)searchSuggestionRenderer.suggestion
                                                                             .runs)
                                                                             .Select(p => ((dynamic)p).text));

            suggestion.SuggestionType = SuggestionType.Text;
            
            return suggestion;
        }

        public static Suggestion FromMusicResponsiveListItemRenderer(dynamic musicResponsiveListItemRenderer)
        {
            Suggestion suggestion = new();
            YoutubeElement linkedElement;

            JObject jobj = (JObject)musicResponsiveListItemRenderer;
            JObject secondFlexColumnText = (JObject)musicResponsiveListItemRenderer.flexColumns[1].musicResponsiveListItemFlexColumnRenderer.text;

            if (jobj.ContainsKey("playlistItemData"))
            {
                linkedElement = YoutubeMedia.FromMusicResponsiveListItemRenderer(musicResponsiveListItemRenderer);
                suggestion.SuggestionType = SuggestionType.Media;
            }
            else if (secondFlexColumnText.ContainsKey("runs"))
            {
                linkedElement = YoutubePlaylist.FromMusicResponsiveListItemRenderer(musicResponsiveListItemRenderer);
                suggestion.SuggestionType = SuggestionType.Playlist;
            }
            else
            {
                linkedElement = Author.FromMusicResponsiveListItemRenderer(musicResponsiveListItemRenderer);
                suggestion.SuggestionType = SuggestionType.Author;
            }

            suggestion.LinkedYoutubeElement = linkedElement;

            suggestion.Name = linkedElement.Name;
            suggestion.Subtitle = linkedElement.Subtitle;

            return suggestion;
        }

        /// <summary>
        /// The type of the suggestion
        /// </summary>
        public SuggestionType SuggestionType { get; set; }

        /// <summary>
        /// The browseId, playlistId or videoId of the media if the suggestion is of type Media, PLaylist, Author.<br/>
        /// You will have to use this value differently based of the SuggestionType value.
        /// </summary>
        public string? BrowseId { get; set; }

        /// <summary>
        /// The YoutubeElement (YoutubeMedia, YoutubePlaylist or Author) linked with this suggestion.
        /// </summary>
        public YoutubeElement? LinkedYoutubeElement { get; set; }

    }

    /// <summary>
    /// The type of a suggestion.
    /// </summary>
    public enum SuggestionType
    {
        Text,
        Media,
        Playlist,
        Author
    }
}
