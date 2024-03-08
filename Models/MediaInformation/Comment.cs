namespace OuterTube.Models.MediaInformation
{
    /// <summary>
    /// A comment of a Youtube video
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// The number of likes of the comment
        /// </summary>
        public int Likes { get; set; }
        /// <summary>
        /// The text of the comment
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// The author of the comment
        /// </summary>
        public Author? Author { get; set; }
        /// <summary>
        /// The children of the comment (the replies of the comment)
        /// </summary>
        public List<Comment> Children { get; set; } = new();
        /// <summary>
        /// The date when the comment was published.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

    }
}
