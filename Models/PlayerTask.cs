﻿using Newtonsoft.Json.Linq;
using OuterTube.Models.Media;
using OuterTube.Models.MediaInformation;
using OuterTube.Models.MediaInformation.Collections;

namespace OuterTube.Models
{
    public class Player
    {
        public FormatCollection Collection { get; set; }
        private string _json;
        internal Player(FormatCollection collection, string json) => (Collection, _json) = (collection, json);

        /// <summary>
        /// Add additionnal data 
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public Player Update(ref YoutubeMedia media)
        {
            dynamic player = JObject.Parse(_json);
            dynamic videoDetails = player.videoDetails;

            Author author = new();
            author.Name = videoDetails.author;
            author.Id = videoDetails.channelId;

            media.Author = author;

            media.Views = (int)videoDetails.viewCount;
            media.Length = TimeSpan.FromSeconds((int)videoDetails.lengthSeconds);

            return this;
        }
    }
}
