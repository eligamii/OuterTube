namespace OuterTube.Models.MediaInformation
{
    /// <summary>
    /// A Youtube or SponsorBlock chapter for a media
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// The start of the chapter
        /// </summary>
        public TimeSpan Start { get; set; }
        /// <summary>
        /// The end of the chapter
        /// </summary>
        public TimeSpan End { get; set; }
        /// <summary>
        /// The name of the chapter or the SponsorBlock label of it.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
