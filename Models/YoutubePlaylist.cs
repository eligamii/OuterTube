using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class YoutubePlaylist : YoutubeMediaBase
    {
        public static YoutubePlaylist FromMusicResponsiveListItemRenderer(dynamic musicResponsiveListItemRenderer)
        {
            YoutubePlaylist playlist = new();
            playlist.Thumbnails = MediaThumbnailCollection.FromJson(musicResponsiveListItemRenderer.thumbnail.musicThumbnailRenderer.thumbnail.thumbnails);

            dynamic flexColums = musicResponsiveListItemRenderer.flexColumns;
            playlist.Title = flexColums[0].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text;
            foreach (dynamic run in flexColums[1].musicResponsiveListItemFlexColumnRenderer.text.runs) { playlist.Subtitle += run.text; }

            playlist.Id = musicResponsiveListItemRenderer.navigationEndpoint.browseEndpoint.browseId;

            return playlist;
        }

        public static YoutubePlaylist FromMusicTwoRowItemRenderer(dynamic musicTwoRowItemRenderer)
        {
            YoutubePlaylist playlist = new();
            playlist.Thumbnails = MediaThumbnailCollection.FromJson(musicTwoRowItemRenderer.thumbnailRenderer.musicThumbnailRenderer.thumbnail.thumbnails);

            playlist.Title = musicTwoRowItemRenderer.title.runs[0].text;
            foreach (dynamic run in musicTwoRowItemRenderer.subtitle.runs) { playlist.Subtitle += run.text; }

            playlist.Id = musicTwoRowItemRenderer.navigationEndpoint.browseEndpoint.browseId;

            return playlist;
        }

        public List<YoutubeMedia> Items { get; set; } = new();
        internal string ContinuationToken { get; set; } = string.Empty;
    }
}
