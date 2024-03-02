using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class Playlist
    {
        /// <summary>
        /// True if the playlist id starts with RD (is a radio/mix with kinda random content)
        /// </summary>
        public bool IsInfinite { get; set; }
        public string? ContinuationToken { get; set; }
        public List<YoutubeMedia> Items { get; set; } = [];
    }
}
