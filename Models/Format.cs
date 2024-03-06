using Newtonsoft.Json.Linq;
using OuterTube.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class Format
    {
        public static Format FromAdaptiveFormat(dynamic adaptiveFormat)
        {
            Format format = new();
            format.Source = adaptiveFormat.url;
            format.ContentType = new ContentType(adaptiveFormat.mimeType.ToString());
            format.Bitrate = adaptiveFormat.bitrate;
            
            format.LastModificationDate = (double)adaptiveFormat.lastModified;
            format.Length = TimeSpan.FromMilliseconds((int)adaptiveFormat.contentLength);
            if(adaptiveFormat.height is not null) format.Size = new Size((int)adaptiveFormat.width, (int)adaptiveFormat.height);
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

        public string Source { get; set; } = string.Empty;
        public ContentType? ContentType { get; set; }
        public int Bitrate { get; set; }
        public Size Size { get; set; }
        public int Height => Size.Height;
        public int Width => Size.Width;
        public double LastModificationDate { get; set; }
        public TimeSpan Length { get; set; }
        public string QualityLabel { get; set; } = string.Empty;
        public int FrameRate { get; set; }
        public AudioQuality AudioQuality { get; set; } = AudioQuality.VideoOrUnknown;
        public bool AudioOnly { get; set; }
    }
}
