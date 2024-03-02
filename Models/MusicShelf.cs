namespace OuterTube.Models
{
    /// <summary>
    /// A object that can contains musics or playlists. This is the equavalient of the main elements of the Youtube Music homepage
    /// </summary>
    public class MusicShelf
    {
        /// <summary>
        /// The title of the shelf.
        /// </summary>
        public string Title { get; set; } 
        /// <summary>
        /// The subtitle of the shelf, usually written in UPPERCASE. (the text above of the title in the Youtube Music shelves)
        /// </summary>
        public string Subtitle { get; set; } = string.Empty;
        /// <summary>
        /// The list of media of the shelf. Can be playlists, videos or songs
        /// </summary>
        public List<YoutubeMedia> Items { get; set; } = [];
    }
}
