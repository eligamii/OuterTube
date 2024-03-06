using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class MediaThumbnailCollection
    {
        public static MediaThumbnailCollection FromJson(dynamic thumbnails)
        {
            MediaThumbnailCollection collection = new();
            foreach (dynamic thumbnailJson in thumbnails)
            {
                MediaThumbnail thumbnail = MediaThumbnail.FromJson(thumbnailJson);
                collection._thumbnails.Add(thumbnail);
            }

            return collection;
        }

        private List<MediaThumbnail> _thumbnails { get; set; } = new();
        public MediaThumbnail GetForHeight(int height)
        {
            var ordered = _thumbnails.OrderBy(p => p.Size.Height).ToList();
            var thumb = ordered.Find(p => p.Size.Height <= height) ?? ordered.Last();

            return thumb;
        }

        public MediaThumbnail? SmallestThumbnail => _thumbnails.OrderBy(p => p.Size.Height).ToList().FirstOrDefault();
        public MediaThumbnail? BiggestThumbnail => _thumbnails.OrderBy(p => p.Size.Height).ToList().LastOrDefault();

    }
}
