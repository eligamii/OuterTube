using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class SearchResult
    {
        public SearchResult(IEnumerable<YoutubeMedia> items, string continuationToken)
        {
            Items = items;
            ContinuationToken = continuationToken;
        }

        public IEnumerable<YoutubeMedia> Items { get; }
        internal string ContinuationToken { get; }
    }
}