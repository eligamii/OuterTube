using Newtonsoft.Json.Linq;
using System.Collections;

namespace OuterTube.Models.MediaInformation.Collections
{
    /// <summary>
    /// A collection of the temporary availables formats of a media.
    /// </summary>
    public class FormatCollection : IList<Format>
    {
        internal static FormatCollection FromJson(string json)
        {
            FormatCollection collection = new();
            dynamic player = JObject.Parse(json);

            foreach (object format in player.streamingData
                                            .adaptiveFormats)
            {

                collection.Formats.Add(Format.FromAdaptiveFormat(format));
            }

            return collection;
        }

        public int IndexOf(Format item)
        {
            return ((IList<Format>)Formats).IndexOf(item);
        }

        public void Insert(int index, Format item)
        {
            ((IList<Format>)Formats).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Format>)Formats).RemoveAt(index);
        }

        public void Add(Format item)
        {
            ((ICollection<Format>)Formats).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Format>)Formats).Clear();
        }

        public bool Contains(Format item)
        {
            return ((ICollection<Format>)Formats).Contains(item);
        }

        public void CopyTo(Format[] array, int arrayIndex)
        {
            ((ICollection<Format>)Formats).CopyTo(array, arrayIndex);
        }

        public bool Remove(Format item)
        {
            return ((ICollection<Format>)Formats).Remove(item);
        }

        public IEnumerator<Format> GetEnumerator()
        {
            return ((IEnumerable<Format>)Formats).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Formats).GetEnumerator();
        }

        public List<Format> Formats { get; set; } = new();
        public List<Format> AudioOnlyFormats => Formats.Where(p => p.AudioOnly).ToList();
        public List<Format> VideoFormats => Formats.Where(p => !p.AudioOnly).ToList();


        public Format BestAudioOnlyFormat
        {
            get
            {
                var sorted = from p in AudioOnlyFormats orderby (int)p.AudioQuality descending select p;
                return sorted.First();
            }
        }

        public int Count => ((ICollection<Format>)Formats).Count;

        public bool IsReadOnly => ((ICollection<Format>)Formats).IsReadOnly;

        public Format this[int index] { get => ((IList<Format>)Formats)[index]; set => ((IList<Format>)Formats)[index] = value; }
    }
}
