using OuterTube.Models.MediaInformation.Collections;

namespace OuterTube.Models
{
    /// <summary>
    /// The base class for most objects
    /// </summary>
    public class YoutubeElement
    {
        /// <summary>
        /// The title / name of the element 
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The subtitle of the element
        /// </summary>
        public string Subtitle { get; set; } = string.Empty;
        /// <summary>
        /// The thumbnails images of the element
        /// </summary>
        public MediaThumbnailCollection? Thumbnails { get; set; }
        /// <summary>
        /// A property to store whatever you want in it. (ex: The id of a playlist in where the media is linked)
        /// </summary>
        public object? Tag { get; set; }
    }
}
