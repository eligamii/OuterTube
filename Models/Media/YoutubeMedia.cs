using Newtonsoft.Json.Linq;
using OuterTube.Models.MediaInformation;
using OuterTube.Models.MediaInformation.Collections;

namespace OuterTube.Models.Media
{
    public class YoutubeMedia : YoutubeMediaBase
    {
        public static YoutubeMedia FromVideoRenderer(dynamic videoRenderer)
        {
            YoutubeMedia media = new();

            Author author = new();
            author.Id = videoRenderer.longBylineText.runs[0].navigationEndpoint.browseEndpoint.browseId;
            author.Name = videoRenderer.longBylineText.runs[0].text;

            if (((JObject)videoRenderer).ContainsKey("ownerBadges"))
            {
                if (videoRenderer.ownerBadges[0].metadataBadgeRenderer.style == "BARGE_STYLE_CIRCLE_VERIFIED")
                    author.IsCertified = true;
                else
                    author.IsMusicChannel = true;
            }

            media.Author = author;
            media.Id = videoRenderer.videoId;
            media.Name = videoRenderer.title.runs[0].text;
            media.Thumbnails = MediaThumbnailCollection.FromThumbnails(videoRenderer.thumbnail.thumbnails);
            media.ShortDescription = videoRenderer.descriptionSnippet.runs[0].text;
            media.Views = int.TryParse(string.Concat(((string)videoRenderer.viewCountText.simpleText).Where(char.IsDigit).ToList()), out int res) ? res : 0;

            media.Subtitle = videoRenderer.lengthText.simpleText + " • " +
                             videoRenderer.publicshedTimeText.simpleText + " • " +
                             videoRenderer.viewCountText.simpleText;

            media.LengthText = videoRenderer.lengthText.simpleText;

            return media;
        }

        public static YoutubeMedia FromMusicResponsiveListItemRenderer(dynamic musicResponsiveListItemRenderer)
        {
            YoutubeMedia media = new YoutubeMedia();
            media.Thumbnails = MediaThumbnailCollection.FromThumbnails(musicResponsiveListItemRenderer.thumbnail.musicThumbnailRenderer.thumbnail.thumbnails);

            dynamic flexColums = musicResponsiveListItemRenderer.flexColumns;
            media.Name = flexColums[0].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text;
            foreach (dynamic run in flexColums[1].musicResponsiveListItemFlexColumnRenderer.text.runs) { media.Subtitle += run.text; }

            try { media.Subtitle += " • " + flexColums[2].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text; } catch { }
            if(((JObject)musicResponsiveListItemRenderer).ContainsKey("playlistItemData")) media.Id = musicResponsiveListItemRenderer.playlistItemData.videoId;
            else media.BrowseId = musicResponsiveListItemRenderer.navigationEndpoint.browseEndpoint.browseId;

            return media;
        }

        /// <summary>
        /// The short version of the description. Always available with YoutubeClient, never with YoutubeMusicClient
        /// </summary>
        public string ShortDescription { get; set; } = string.Empty;

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
        /// The string representation of the length.
        /// </summary>
        public string LengthText { get; set; } = string.Empty;
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
        /// <summary>
        /// True if the media is a short.
        /// </summary>
        public bool IsShort { get; set; } = false;
    }
}
