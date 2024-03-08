using Newtonsoft.Json.Linq;
using OuterTube.Enums;
using OuterTube.Models.MediaInformation;
using System.Collections;
using System.Text;

namespace OuterTube.Models.Media.Collections
{
    public class MusicSearchResult : IList<YoutubeElement>
    {
        internal MusicSearchResult(string json, SearchFilter filter)
        {
            Filter = filter;
            dynamic res = JObject.Parse(json);

            JArray contents = res.contents
                                 .tabbedSearchResultsRenderer
                                 .tabs[0]
                                 .tabRenderer
                                 .content
                                 .sectionListRenderer
                                 .contents as JArray ?? [];

            dynamic musicShelfRenderer = ((dynamic)contents.Where(p => ((JObject)p).ContainsKey("musicShelfRenderer")).First()).musicShelfRenderer;

            if(((JObject)musicShelfRenderer).ContainsKey("continuations")) ContinuationToken = musicShelfRenderer.continuations[0].nextContinuationData.continuation;

            if (contents.IndexOf(musicShelfRenderer) == 1)
            {
                DidYouMeanTitle = ((dynamic)contents[0]).itemSectionRenderer.contents[0].didYouMeanRenderer.didYouMean.runs[0].text;
            }

            try { Title = musicShelfRenderer.title.runs[0].text; } catch { }

            foreach (dynamic item in musicShelfRenderer.contents)
            {
                if ((int)filter <= 3) // Video
                    Results.Add(YoutubeMedia.FromMusicResponsiveListItemRenderer(item.musicResponsiveListItemRenderer));

                else if ((int)filter <= 5) // Author
                    Results.Add(Author.FromMusicResponsiveListItemRenderer(item.musicResponsiveListItemRenderer));

                else // Playlists
                    Results.Add(YoutubePlaylist.FromMusicResponsiveListItemRenderer(item.musicResponsiveListItemRenderer));

            }
        }


        public async Task<List<YoutubeElement>> LoadMoreItemsAsync()
        {
            if (HasMoreItems)
            {
                dynamic payload = YoutubeMusicClient.BaseWebPayload;

                string payloadString = payload.ToString();
                StringContent content = new(payloadString, Encoding.UTF8);

                // Create the request url
                string requestUrl = YoutubeMusicClient._baseUrl + "/search?key=" + YoutubeMusicClient._webApiKey + "&ctoken=" + ContinuationToken + "&continuation=" + ContinuationToken + "&type=next" + "&prettyPrint=false";

                Shared.HttpClient.DefaultRequestHeaders.Remove("Referrer");
                Shared.HttpClient.DefaultRequestHeaders.Add("Referrer", "music.youtube.com");

                // Make the actual request
                var response = await Shared.HttpClient.PostAsync(requestUrl, content);

                string json = await response.Content.ReadAsStringAsync();
                dynamic continuation = JObject.Parse(json);

                List<YoutubeElement> newElements = [];

                foreach (dynamic item in continuation.continuationContents.musicShelfContinuation.contents)
                {
                    if ((int)Filter <= 3) // Video
                        newElements.Add(YoutubeMedia.FromMusicResponsiveListItemRenderer(item.musicResponsiveListItemRenderer));

                    else if ((int)Filter <= 5) // Author
                        newElements.Add(Author.FromMusicResponsiveListItemRenderer(item.musicResponsiveListItemRenderer));

                    else // Playlists
                        newElements.Add(YoutubePlaylist.FromMusicResponsiveListItemRenderer(item.musicResponsiveListItemRenderer));
                }

                Results.AddRange(newElements);

                try
                {
                    ContinuationToken = continuation.continuationContents.musicShelfContinuation.continuations[0].nextContinuationData.continuation;
                }
                catch
                {
                    ContinuationToken = string.Empty;
                }

                return newElements;
            }
            else
            {
                return new List<YoutubeElement>();
            }
        }

        public bool HasMoreItems => !string.IsNullOrEmpty(ContinuationToken);

        public int IndexOf(YoutubeElement item)
        {
            return ((IList<YoutubeElement>)Results).IndexOf(item);
        }

        public void Insert(int index, YoutubeElement item)
        {
            ((IList<YoutubeElement>)Results).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<YoutubeElement>)Results).RemoveAt(index);
        }

        public void Add(YoutubeElement item)
        {
            ((ICollection<YoutubeElement>)Results).Add(item);
        }

        public void Clear()
        {
            ((ICollection<YoutubeElement>)Results).Clear();
        }

        public bool Contains(YoutubeElement item)
        {
            return ((ICollection<YoutubeElement>)Results).Contains(item);
        }

        public void CopyTo(YoutubeElement[] array, int arrayIndex)
        {
            ((ICollection<YoutubeElement>)Results).CopyTo(array, arrayIndex);
        }

        public bool Remove(YoutubeElement item)
        {
            return ((ICollection<YoutubeElement>)Results).Remove(item);
        }

        public IEnumerator<YoutubeElement> GetEnumerator()
        {
            return ((IEnumerable<YoutubeElement>)Results).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Results).GetEnumerator();
        }

        internal string ContinuationToken { get; set; } = string.Empty;
        public string DidYouMeanTitle { get; set; } = string.Empty;
        public string DidYouMeanText { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public SearchFilter Filter { get; set; }
        /// <summary>
        /// The list of search results for the giver query and filter. 
        /// </summary>
        public List<YoutubeElement> Results { get; set; } = [];

        public int Count => ((ICollection<YoutubeElement>)Results).Count;

        public bool IsReadOnly => ((ICollection<YoutubeElement>)Results).IsReadOnly;

        public YoutubeElement this[int index] { get => ((IList<YoutubeElement>)Results)[index]; set => ((IList<YoutubeElement>)Results)[index] = value; }
    }
}