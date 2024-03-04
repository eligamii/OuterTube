using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class YoutubeMediaBase : YoutubeElement
    {
        public int Views { get; set; }
        public string Id { get; set; } = string.Empty;
        public Author? Author { get; set; } 
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set;} = string.Empty;
    }
}
