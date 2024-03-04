using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class MediaThumbnail
    {
        public string Source { get; set; } = string.Empty;
        public Size Size { get; set; }

        public static MediaThumbnail FromJson(dynamic thumbnailJson)
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
