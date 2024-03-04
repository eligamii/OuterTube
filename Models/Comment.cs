using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class Comment
    {
        public int Likes { get; set; }
        public string Text { get; set; } = string.Empty;
        public Author? Author { get; set; }
        public List<Comment> Children { get; set; } = new();
        public TimeSpan ReleaseDate { get; set; }

    }
}
