namespace OuterTube.Models
{
    public class YoutubeMedia
    {
        public string Id { get; }
        public string Title { get; }
        public string MinThumbnailUrl { get; }
        public string MaxThumbnailUrl { get; }
        public string Duration { get; }
        public string Author { get; }

        public YoutubeMedia(string id, string title, string minThumbnailUrl, string maxThumbnailUrl, string duration, string author)
        {
            Id = id;
            Title = title;
            MinThumbnailUrl = minThumbnailUrl;
            MaxThumbnailUrl = maxThumbnailUrl;
            Duration = duration;
            Author = author;
        }

    }
}
