using Newtonsoft.Json.Linq;
using OuterTube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Parsers
{
    internal class Media
    {

        internal static YoutubeMedia Parse(JObject videoJson)
        {
            JObject renderer = videoJson["musicResponsiveListItemRenderer"]["overlay"]["musicItemThumbnailOverlayRenderer"].Value<JObject>();
            string videoId = renderer["content"]["musicPlayButtonRenderer"]["playNavigationEndpoint"]["watchEndpoint"]["videoId"].Value<string>();

            string title = renderer["content"]["musicPlayButtonRenderer"]["accessibilityPauseData"]["accessibilityData"]["label"].Value<string>();
            List<string> titleList = title.Split().ToList();
            titleList.RemoveAt(0);
            title = string.Join(" ", titleList.ToArray());

            JArray flexColumns = videoJson["musicResponsiveListItemRenderer"]["flexColumns"][1]["musicResponsiveListItemFlexColumnRenderer"]["text"]["runs"].Value<JArray>();
            var element = (JObject)flexColumns.First();
            string author = element["text"].Value<string>();

            element = (JObject)flexColumns.Last();
            string duration = element["text"].Value<string>();

            string minQualityThumbnail = videoJson["musicResponsiveListItemRenderer"]["thumbnail"]["musicThumbnailRenderer"]["thumbnail"]["thumbnails"][0]["url"].Value<string>();
            string maxQualityThumbnail = videoJson["musicResponsiveListItemRenderer"]["thumbnail"]["musicThumbnailRenderer"]["thumbnail"]["thumbnails"][1]["url"].Value<string>();

            var video = new YoutubeMedia() { Id = videoId, Title = title };
            return video;
        }
    }
}
