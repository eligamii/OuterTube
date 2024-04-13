using OuterTube.Enums;
using OuterTube.Models.Media;
using OuterTube.Models;
using OuterTube.Models.Media.Collections;
using OuterTube.Models.MediaInformation.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube
{
    public class Youtube
    {
        private readonly static RequestTarget _target = RequestTarget.Youtube;

        public static async Task<YoutubeHomepage> GetHomepageAsync()
        {
            dynamic payload = _target.WebClient.BaseClientPayload;
            payload.browseId = "FEwhat_to_watch";

            string json = await Shared.RequestAsync(payload, _target.WebClient, "browse");

            return YoutubeHomepage.FromJson(json);
        }

        /// <summary>
        /// Get the player of a media.
        /// </summary>
        /// <param name="video">The media to get the player.</param>
        /// <returns>A Player object containing the list of available formats and additionnal information about the media</returns>
        public static async Task<Player> GetPlayerAsync(YoutubeMedia video) => await GetPlayerAsync(video.Id);

        /// <summary>
        /// Get the player of a media.
        /// </summary>
        /// <param name="videoId">The id of the media to get the player.</param>
        /// <returns>A Player object containing the list of available formats and additionnal information about the media</returns>
        public static async Task<Player> GetPlayerAsync(string videoId)
        {
            dynamic payload = _target.IOSClient.BaseClientPayload;
            payload.videoId = videoId;

            string json = await Shared.RequestAsync(payload, _target.IOSClient, "player");

            return new Player(FormatCollection.FromJson(json), json);
        }
    }
}
