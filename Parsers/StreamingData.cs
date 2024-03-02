using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Parsers
{
    internal static class StreamingData
    {
        internal static List<string> ParseForAudioOnly(string playerJson) 
        {
            List<string> strings = [];
            dynamic player = JObject.Parse(playerJson);

            foreach(dynamic format in player.streamingData
                                            .adaptiveFormats)
            {
                if (!((string)format.mimeType).Contains("audio")) continue;

                string encodedUrl = ((string)format.signatureCipher).Split("url=")[1];
                strings.Add(WebUtility.UrlDecode(encodedUrl));
            }

            return strings;
        }
    }
}
