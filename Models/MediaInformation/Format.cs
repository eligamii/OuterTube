using Newtonsoft.Json.Linq;
using OuterTube.Enums;
using System.Drawing;
using System.Net.Mime;

namespace OuterTube.Models.MediaInformation
{
    /// <remarks>THIS IS A ONE USAGE OBJECT AS THE 'Source' PROPERTY WILL EXPIRE QUICKLY</remarks>
    /// <summary>
    /// One of the available formats of a YoutubeMedia
    /// </summary>
    public class Format
    {
        internal static Format FromAdaptiveFormat(dynamic adaptiveFormat)
        {
            Format format = new();
            format.Source = adaptiveFormat.url;
            format.ContentType = new ContentType(adaptiveFormat.mimeType.ToString());
            format.Bitrate = adaptiveFormat.bitrate;

            format.LastModificationDate = new DateTime((long)adaptiveFormat.lastModified);
            format.Length = TimeSpan.FromMilliseconds((int)adaptiveFormat.contentLength);
            if (adaptiveFormat.height is not null) format.Size = new Size((int)adaptiveFormat.width, (int)adaptiveFormat.height);
            if ((adaptiveFormat as JObject ?? new JObject()).ContainsKey("fps")) format.FrameRate = (int)adaptiveFormat.fps;
            if ((adaptiveFormat as JObject ?? new JObject()).ContainsKey("qualityLabel")) format.QualityLabel = adaptiveFormat.qualityLabel; else format.QualityLabel = adaptiveFormat.quality;
            if ((adaptiveFormat as JObject ?? new JObject()).ContainsKey("audioQuality"))
            {
                format.AudioQuality = adaptiveFormat.audioQuality switch
                {
                    "AUDIO_QUALITY_HIGH" => AudioQuality.High,
                    "AUDIO_QUALITY_MEDIUM" => AudioQuality.Medium,
                    "AUDIO_QUALITY_LOW" => AudioQuality.Low,

                    _ => AudioQuality.VideoOrUnknown
                };

                format.AudioOnly = true;
            }

            return format;
        }

        /// <summary>
        /// The temporary source url of the format. Will expire in 1 hour or so.
        /// </summary>
        public string Source { get; private set; } = string.Empty;
        /// <summary>
        /// The content type of the format. Either audio/... or video/... with the codec used.
        /// </summary>
        public ContentType? ContentType { get; private set; }
        /// <summary>
        /// The bitrate of the format.
        /// </summary>
        public int Bitrate { get; private set; }
        /// <summary>
        /// The size of the video output of the format if one.
        /// </summary>
        public Size? Size { get; private set; }
        /// <summary>
        /// The height of the video output of the format if one. Otherwise 0.
        /// </summary>
        public int Height => Size?.Height ?? 0;
        /// <summary>
        /// The width of the video output of the format if one. Otherwise 0.
        /// </summary>
        public int Width => Size?.Width ?? 0;
        /// <summary>
        /// The last time the format was modified.
        /// </summary>
        public DateTime LastModificationDate { get; private set; }
        /// <summary>
        /// The length of the media.
        /// </summary>
        public TimeSpan Length { get; private set; }
        /// <summary>
        /// The quality label of the format. Ex: "hd1080" or "tiny" (when the format is audio only)
        /// </summary>
        public string QualityLabel { get; private set; } = string.Empty;
        /// <summary>
        /// The framerate of the format if cantaining a video output. Otherwise 0.
        /// </summary>
        public int FrameRate { get; private set; } = 0;
        /// <summary>
        /// The audio quality of the format if audio only. Otherwise AudioQuality.VideoOrUnknown.
        /// </summary>
        public AudioQuality AudioQuality { get; private set; } = AudioQuality.VideoOrUnknown;
        /// <summary>
        /// True if the fomat only contains audio output
        /// </summary>
        public bool AudioOnly { get; private set; }
    }
}
