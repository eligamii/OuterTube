using System.Drawing;

namespace OuterTube.Models.MediaInformation
{
    /// <summary>
    /// The thumbnail of a media or the profile picture of an author / Youtube channel
    /// </summary>
    public class MediaThumbnail
    {
        /// <summary>
        /// The permanant url of the thumbnail
        /// </summary>
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// The size of the thumbnail
        /// </summary>
        public Size Size { get; set; }

        internal static MediaThumbnail FromJson(dynamic thumbnailJson)
        {
            string url = thumbnailJson.url;
            Size size = new((int)thumbnailJson.width, (int)thumbnailJson.height);

            return new MediaThumbnail()
            {
                Size = size,
                Source = url
            };
        }
    }
}
