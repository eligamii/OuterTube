using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class YoutubeMedia : YoutubeMediaBase
    {
        public static YoutubeMedia FromMusicResponsiveListItemRenderer(dynamic musicResponsiveListItemRenderer)
        {
            YoutubeMedia media = new YoutubeMedia();
            media.Thumbnails = MediaThumbnailCollection.FromJson(musicResponsiveListItemRenderer.thumbnail.musicThumbnailRenderer.thumbnail.thumbnails);

            dynamic flexColums = musicResponsiveListItemRenderer.flexColumns;
            media.Title = flexColums[0].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text;
            foreach(dynamic run in flexColums[1].musicResponsiveListItemFlexColumnRenderer.text.runs) { media.Subtitle += run.text; }

            try { media.Subtitle += " • " + flexColums[2].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text; } catch { }
            media.Id = musicResponsiveListItemRenderer.playlistItemData.videoId;

            return media;
        }


        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public TimeSpan Length { get; set; }
        public List<Chapter>? Chapters { get; set; }
        public List<Chapter>? CommunityChapters { get; set; }
        public List<Comment>? Comments { get; set; }
        public bool IsFamilyFriendly { get; set; } = false;
        public bool IsSong { get; set; } = false;
    }
}
