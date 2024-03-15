using Newtonsoft.Json.Linq;
using OuterTube.Models.MediaInformation;
using OuterTube.Models.MediaInformation.Collections;

namespace OuterTube.Models.Media
{
    public class YoutubeMedia : YoutubeMediaBase
    {
        public static List<YoutubeMedia> ListFromJson(string json)
        {
            List<YoutubeMedia> collection = new();

            dynamic obj = JObject.Parse(json);
            foreach (dynamic item in obj.contents
                                       .singleColumnBrowseResultsRenderer
                                       .tabs[0]
                                       .tabRenderer
                                       .content
                                       .sectionListRenderer
                                       .contents)
            {
                dynamic contents = item.musicShelfRenderer.contents;

                foreach (dynamic subItem in contents)
                {
                    collection.Add(YoutubeMedia.FromMusicResponsiveListItemRenderer(subItem.musicResponsiveListItemRenderer));
                }

            }

            return collection;
        }

        public static YoutubeMedia FromMusicResponsiveListItemRenderer(dynamic musicResponsiveListItemRenderer)
        {
            YoutubeMedia media = new YoutubeMedia();
            media.Thumbnails = MediaThumbnailCollection.FromJson(musicResponsiveListItemRenderer.thumbnail.musicThumbnailRenderer.thumbnail.thumbnails);

            dynamic flexColums = musicResponsiveListItemRenderer.flexColumns;
            media.Name = flexColums[0].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text;
            foreach (dynamic run in flexColums[1].musicResponsiveListItemFlexColumnRenderer.text.runs) { media.Subtitle += run.text; }

            try { media.Subtitle += " • " + flexColums[2].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text; } catch { }
            if(((JObject)musicResponsiveListItemRenderer).ContainsKey("playlistItemData")) media.Id = musicResponsiveListItemRenderer.playlistItemData.videoId;
            else media.BrowseId = musicResponsiveListItemRenderer.navigationEndpoint.browseEndpoint.browseId;

            return media;
        }

        /// <summary>
        /// The browse id of the media if the playlistItemData is not available
        /// </summary>
        public string BrowseId { get; set; } = string.Empty;

        /// <summary>
        /// The number of likes of the media.
        /// </summary>
        public int Likes { get; set; }
        /// <summary>
        /// The number of dislikes of the media. (Powered by Return Youtube Dislikes)
        /// </summary>
        public int Dislikes { get; set; }
        /// <summary>
        /// The length of le media.
        /// </summary>
        public TimeSpan Length { get; set; }
        /// <summary>
        /// The Youtube chapters of the media. Will always return null if song.
        /// </summary>
        public List<Chapter>? Chapters { get; set; }
        /// <summary>
        /// The SponsorBlock chapters of the media.
        /// </summary>
        public List<Chapter>? CommunityChapters { get; set; }
        /// <summary>
        /// The comments of the media.
        /// </summary>
        public List<Comment>? Comments { get; set; }
        /// <summary>
        /// Return true if this comment aim an audience of kids. (Always false if song)
        /// </summary>
        public bool IsFamilyFriendly { get; set; } = false;
        /// <summary>
        /// True if the media is a song.
        /// </summary>
        public bool IsSong { get; set; } = false;
    }
}
