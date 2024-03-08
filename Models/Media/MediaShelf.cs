using Newtonsoft.Json.Linq;
using System.Collections;

namespace OuterTube.Models.Media
{
    /// <summary>
    /// A representation of the main Youtube Music homepage's element in the form of a list with a Title and a Subtitle.
    /// </summary>
    public class MediaShelf : YoutubeElement, IList<YoutubeElement>
    {
        internal static MediaShelf FromMusicCarouselShelfRenderer(dynamic musicCarouselShelfRenderer)
        {
            MediaShelf shelf = new();

            try
            {
                shelf.Name = musicCarouselShelfRenderer.header.musicCarouselShelfBasicHeaderRenderer.title.runs[0].text;
                shelf.Subtitle = musicCarouselShelfRenderer.header.musicCarouselShelfBasicHeaderRenderer.strapline.runs[0].text;
            }
            catch { }

            foreach (dynamic obj in musicCarouselShelfRenderer.contents)
            {
                JObject jObj = (JObject)obj;

                if (jObj.ContainsKey("musicResponsiveListItemRenderer"))
                    shelf.Items.Add(YoutubeMedia.FromMusicResponsiveListItemRenderer(obj.musicResponsiveListItemRenderer));
                else if (jObj.ContainsKey("musicTwoRowItemRenderer"))
                    shelf.Items.Add(YoutubePlaylist.FromMusicTwoRowItemRenderer(obj.musicTwoRowItemRenderer));
            }

            return shelf;
        }

        public int IndexOf(YoutubeElement item)
        {
            return ((IList<YoutubeElement>)Items).IndexOf(item);
        }

        public void Insert(int index, YoutubeElement item)
        {
            ((IList<YoutubeElement>)Items).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<YoutubeElement>)Items).RemoveAt(index);
        }

        public void Add(YoutubeElement item)
        {
            ((ICollection<YoutubeElement>)Items).Add(item);
        }

        public void Clear()
        {
            ((ICollection<YoutubeElement>)Items).Clear();
        }

        public bool Contains(YoutubeElement item)
        {
            return ((ICollection<YoutubeElement>)Items).Contains(item);
        }

        public void CopyTo(YoutubeElement[] array, int arrayIndex)
        {
            ((ICollection<YoutubeElement>)Items).CopyTo(array, arrayIndex);
        }

        public bool Remove(YoutubeElement item)
        {
            return ((ICollection<YoutubeElement>)Items).Remove(item);
        }

        public IEnumerator<YoutubeElement> GetEnumerator()
        {
            return ((IEnumerable<YoutubeElement>)Items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Items).GetEnumerator();
        }

        public List<YoutubeElement> Items { get; set; } = new();

        public int Count => ((ICollection<YoutubeElement>)Items).Count;

        public bool IsReadOnly => ((ICollection<YoutubeElement>)Items).IsReadOnly;

        public YoutubeElement this[int index] { get => ((IList<YoutubeElement>)Items)[index]; set => ((IList<YoutubeElement>)Items)[index] = value; }
    }
}
