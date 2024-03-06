using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class YoutubePlaylist : YoutubeMediaBase
    {
        public static YoutubePlaylist FromJson(string json, bool isInfinite)
        {
            YoutubePlaylist playlist = new();
            dynamic playlistJson = JObject.Parse(json);

            foreach (dynamic video in playlistJson.contents
                                                 .singleColumnMusicWatchNextResultsRenderer
                                                 .tabbedRenderer
                                                 .watchNextTabbedResultsRenderer
                                                 .tabs[0]
                                                 .tabRenderer
                                                 .content
                                                 .musicQueueRenderer
                                                 .content
                                                 .playlistPanelRenderer
                                                 .contents)
            {
                string title = video.playlistPanelVideoRenderer.title.runs[0].text;
                string videoId = video.playlistPanelVideoRenderer.videoId;

                YoutubeMedia media = new()
                {
                    Title = title,
                    Id = videoId
                };

                playlist.Items.Add(media);
            }

            playlist.ContinuationToken = playlistJson.contents
                                                     .singleColumnMusicWatchNextResultsRenderer
                                                     .tabbedRenderer
                                                     .watchNextTabbedResultsRenderer
                                                     .tabs[0]
                                                     .tabRenderer
                                                     .content
                                                     .musicQueueRenderer
                                                     .content
                                                     .playlistPanelRenderer
                                                     .continuations[0]
                                                     .nextRadioContinuationData
                                                     .continuation;

            return playlist;

        }


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
