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
        public static YoutubePlaylist Parse(string json, bool isInfinite)
        {
            YoutubePlaylist playlist = new();
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
                string videoId = video.playlistPanelVideoRenderer.videoId;

                YoutubeMedia media = new()
                {
                    Title = title, Id = videoId
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
    }
}
