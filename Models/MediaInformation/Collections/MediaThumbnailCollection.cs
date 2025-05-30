﻿namespace OuterTube.Models.MediaInformation.Collections
{
    public class MediaThumbnailCollection
    {
        public static MediaThumbnailCollection FromThumbnails(dynamic thumbnails)
        {
            MediaThumbnailCollection collection = new();
            foreach (object thumbnailJson in thumbnails)
            {
                MediaThumbnail thumbnail = MediaThumbnail.FromJson(thumbnailJson);
                collection.Thumbnails.Add(thumbnail);
            }

            return collection;
        }

        public List<MediaThumbnail> Thumbnails { get; set; } = new();
        public MediaThumbnail GetForHeight(int height)
        {
            var ordered = Thumbnails.OrderBy(p => p.Size.Height).ToList();
            var thumb = ordered.Find(p => p.Size.Height <= height) ?? ordered.Last();

            return thumb;
        }

        public MediaThumbnail? SmallestThumbnail => Thumbnails.OrderBy(p => p.Size.Height).ToList().FirstOrDefault();
        public MediaThumbnail? BiggestThumbnail => Thumbnails.OrderBy(p => p.Size.Height).ToList().LastOrDefault();

    }
}
