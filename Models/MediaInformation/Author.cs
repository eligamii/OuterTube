using OuterTube.Models.MediaInformation.Collections;

namespace OuterTube.Models.MediaInformation
{
    public class Author : YoutubeElement
    {
        internal static Author FromMusicResponsiveListItemRenderer(dynamic musicResponsiveListItemRenderer)
        {
            Author author = new();
            author.Thumbnails = MediaThumbnailCollection.FromThumbnails(musicResponsiveListItemRenderer.thumbnail.musicThumbnailRenderer.thumbnail.thumbnails);

            dynamic flexColums = musicResponsiveListItemRenderer.flexColumns;
            author.Name = flexColums[0].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text;

            author.Id = musicResponsiveListItemRenderer.navigationEndpoint.browseEndpoint.browseId;

            return author;
        }
        /// <summary>
        /// The descrpition of the channel / author.
        /// </summary>
        public string ChannelDescription { get; set; } = string.Empty;
        /// <summary>
        /// The Youtube id of the channel / author.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The number of followers this channel / author has.
        /// </summary>
        public int FollowersCount { get; set; }
        /// <summary>
        /// True if the author is certified. (From YoutubeClient only)
        /// </summary>
        public bool IsCertified { get; set; } = false;
        /// <summary>
        /// True if the author (the channel) is a music channel.
        /// </summary>
        public bool IsMusicChannel { get; set; } = true;
    }
}
