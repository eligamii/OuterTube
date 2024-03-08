using OuterTube.Models.MediaInformation;

namespace OuterTube.Models.Media
{
    /// <summary>
    /// The base object of the YoutubeMedia and YoutubePlaylist objects
    /// </summary>
    public class YoutubeMediaBase : YoutubeElement
    {
        /// <summary>
        /// The number of view of the media.
        /// </summary>
        public int Views { get; set; }
        /// <summary>
        /// The videoId / playlistId of the media
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// the author of the media
        /// </summary>
        public Author? Author { get; set; }
        /// <summary>
        /// The date the media was published.
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// The description of the media.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
