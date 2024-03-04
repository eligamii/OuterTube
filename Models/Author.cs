using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class Author : YoutubeElement
    {
        public static Author FromMusicResponsiveListItemRenderer(dynamic musicResponsiveListItemRenderer) 
        {
            Author author = new();
            author.Thumbnails = MediaThumbnailCollection.FromJson(musicResponsiveListItemRenderer.thumbnail.musicThumbnailRenderer.thumbnail.thumbnails);

            dynamic flexColums = musicResponsiveListItemRenderer.flexColumns;
            author.Title = flexColums[0].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text;
            foreach (dynamic run in flexColums[1].musicResponsiveListItemFlexColumnRenderer.text.runs) { author.Subtitle += run.text; }

            author.Id = musicResponsiveListItemRenderer.navigationEndpoint.browseEndpoint.browseId;

            return author;
        }

        public string ChannelDescription { get; set; } = string.Empty;
        
        public string Id { get; set; } = string.Empty;
        public int FollowersCount { get; set; }
    }
}
