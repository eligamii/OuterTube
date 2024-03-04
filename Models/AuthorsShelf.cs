using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class AuthorsShelf : MediaShelf
    {
        public new List<Author> Items { get; set; } = [];
    }
}
