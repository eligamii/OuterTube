using Newtonsoft.Json.Linq;
using OuterTube.Models.MediaInformation.Collections;
using System.Collections;

namespace OuterTube.Models.Media
{
    /// <summary>
    /// A playlist / album in the form of a list of medias
    /// </summary>
    public class YoutubePlaylist : YoutubeMediaBase, IList<YoutubeMedia>
    {
        internal static YoutubePlaylist FromJson(string json, bool isInfinite)
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
                    Name = title,
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


        internal static YoutubePlaylist FromMusicResponsiveListItemRenderer(dynamic musicResponsiveListItemRenderer)
        {
            YoutubePlaylist playlist = new();
            playlist.Thumbnails = MediaThumbnailCollection.FromJson(musicResponsiveListItemRenderer.thumbnail.musicThumbnailRenderer.thumbnail.thumbnails);

            dynamic flexColums = musicResponsiveListItemRenderer.flexColumns;
            playlist.Name = flexColums[0].musicResponsiveListItemFlexColumnRenderer.text.runs[0].text;
            foreach (dynamic run in flexColums[1].musicResponsiveListItemFlexColumnRenderer.text.runs) { playlist.Subtitle += run.text; }

            playlist.Id = musicResponsiveListItemRenderer.navigationEndpoint.browseEndpoint.browseId;

            return playlist;
        }

        internal static YoutubePlaylist FromMusicTwoRowItemRenderer(dynamic musicTwoRowItemRenderer)
        {
            YoutubePlaylist playlist = new();
            playlist.Thumbnails = MediaThumbnailCollection.FromJson(musicTwoRowItemRenderer.thumbnailRenderer.musicThumbnailRenderer.thumbnail.thumbnails);

            playlist.Name = musicTwoRowItemRenderer.title.runs[0].text;
            foreach (dynamic run in musicTwoRowItemRenderer.subtitle.runs) { playlist.Subtitle += run.text; }

            playlist.Id = musicTwoRowItemRenderer.navigationEndpoint.browseEndpoint.browseId;

            return playlist;
        }

        public int IndexOf(YoutubeMedia item)
        {
            return ((IList<YoutubeMedia>)Items).IndexOf(item);
        }

        public void Insert(int index, YoutubeMedia item)
        {
            ((IList<YoutubeMedia>)Items).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<YoutubeMedia>)Items).RemoveAt(index);
        }

        public void Add(YoutubeMedia item)
        {
            ((ICollection<YoutubeMedia>)Items).Add(item);
        }

        public void Clear()
        {
            ((ICollection<YoutubeMedia>)Items).Clear();
        }

        public bool Contains(YoutubeMedia item)
        {
            return ((ICollection<YoutubeMedia>)Items).Contains(item);
        }

        public void CopyTo(YoutubeMedia[] array, int arrayIndex)
        {
            ((ICollection<YoutubeMedia>)Items).CopyTo(array, arrayIndex);
        }

        public bool Remove(YoutubeMedia item)
        {
            return ((ICollection<YoutubeMedia>)Items).Remove(item);
        }

        public IEnumerator<YoutubeMedia> GetEnumerator()
        {
            return ((IEnumerable<YoutubeMedia>)Items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Items).GetEnumerator();
        }

        /// <summary>
        /// The list of medias of the playlist
        /// </summary>
        public List<YoutubeMedia> Items { get; set; } = new();
        /// <summary>
        /// The continuation token of the playlist. Do not modify this value.
        /// </summary>
        public string ContinuationToken { get; set; } = string.Empty;

        public int Count => ((ICollection<YoutubeMedia>)Items).Count;

        public bool IsReadOnly => ((ICollection<YoutubeMedia>)Items).IsReadOnly;

        public YoutubeMedia this[int index] { get => ((IList<YoutubeMedia>)Items)[index]; set => ((IList<YoutubeMedia>)Items)[index] = value; }
    }
}
