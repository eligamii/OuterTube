using Newtonsoft.Json.Linq;
using System.Collections;

namespace OuterTube.Models.Media.Collections
{
    /// <summary>
    /// The representation of the Youtube Music homepage.
    /// </summary>
    public class MediaShelfCollection : IList<MediaShelf>
    {
        public static MediaShelfCollection FromJson(string json)
        {
            MediaShelfCollection collection = new();
            dynamic jsonObj = JObject.Parse(json);

            foreach (dynamic item in jsonObj.contents
                                              .singleColumnBrowseResultsRenderer
                                              .tabs[0]
                                              .tabRenderer
                                              .content
                                              .sectionListRenderer
                                              .contents)
            {
                if (((JObject)item).ContainsKey("musicCarouselShelfRenderer"))
                    collection.MediaShelves.Add(MediaShelf.FromMusicCarouselShelfRenderer(item.musicCarouselShelfRenderer));
            }

            return collection;
        }

        public int IndexOf(MediaShelf item)
        {
            return ((IList<MediaShelf>)MediaShelves).IndexOf(item);
        }

        public void Insert(int index, MediaShelf item)
        {
            ((IList<MediaShelf>)MediaShelves).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<MediaShelf>)MediaShelves).RemoveAt(index);
        }

        public void Add(MediaShelf item)
        {
            ((ICollection<MediaShelf>)MediaShelves).Add(item);
        }

        public void Clear()
        {
            ((ICollection<MediaShelf>)MediaShelves).Clear();
        }

        public bool Contains(MediaShelf item)
        {
            return ((ICollection<MediaShelf>)MediaShelves).Contains(item);
        }

        public void CopyTo(MediaShelf[] array, int arrayIndex)
        {
            ((ICollection<MediaShelf>)MediaShelves).CopyTo(array, arrayIndex);
        }

        public bool Remove(MediaShelf item)
        {
            return ((ICollection<MediaShelf>)MediaShelves).Remove(item);
        }

        public IEnumerator<MediaShelf> GetEnumerator()
        {
            return ((IEnumerable<MediaShelf>)MediaShelves).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)MediaShelves).GetEnumerator();
        }

        public List<MediaShelf> MediaShelves { get; set; } = new();
        public string ContinuationToken { get; set; } = string.Empty;

        public int Count => ((ICollection<MediaShelf>)MediaShelves).Count;

        public bool IsReadOnly => ((ICollection<MediaShelf>)MediaShelves).IsReadOnly;

        public MediaShelf this[int index] { get => ((IList<MediaShelf>)MediaShelves)[index]; set => ((IList<MediaShelf>)MediaShelves)[index] = value; }
    }
}
