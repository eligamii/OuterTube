using Newtonsoft.Json.Linq;
using OuterTube.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Parsers
{
    internal static class Playlists
    {
        public static Playlist Parse(string json, bool isInfinite)
        {
            Playlist playlist = new();
            dynamic playlistJson = JObject.Parse(json);
            
            foreach(dynamic video in playlistJson.contents
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
                string minQualityThumbnail = video.playlistPanelVideoRenderer.thumbnail.thumbnails[0].url;
                string? maxQualityThumbnail = (video.playlistPanelVideoRenderer.thumbnail.thumbnails as JArray).Last().Value<JObject>()["url"].Value<string>();
                string length = video.playlistPanelVideoRenderer.lengthText.runs[0].text;
                string videoId = video.playlistPanelVideoRenderer.videoId;

                YoutubeMedia media = new(videoId, title, minQualityThumbnail, maxQualityThumbnail, length, "");
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
    }
}
