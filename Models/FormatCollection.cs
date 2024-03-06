using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Models
{
    public class FormatCollection
    {
        public static FormatCollection FromJson(string json)
        {
            FormatCollection collection = new();
            dynamic player = JObject.Parse(json);

            foreach (dynamic format in player.streamingData
                                            .adaptiveFormats)
            {

                collection.Formats.Add(Format.FromAdaptiveFormat(format));
            }

            return collection;
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

    }
}
