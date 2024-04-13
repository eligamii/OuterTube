using Newtonsoft.Json.Linq;
using OuterTube.Enums;
using OuterTube.Exceptions;
using OuterTube.Models;
using OuterTube.Models.Media;
using OuterTube.Models.Media.Collections;
using OuterTube.Models.MediaInformation;
using OuterTube.Models.MediaInformation.Collections;
using System.Globalization;
using System.Text;
using System.Web;


namespace OuterTube
{

    public class YoutubeMusic
    {
        private readonly static RequestTarget _target = RequestTarget.YoutubeMusic;

        /// <summary>
        /// Search on Youtube Music from a suggestion.
        /// </summary>
        /// <remarks>
        /// Will return a SearchResult with only the Suggestion's LinkedYoutubeElement if the Suggestion is not of type SuggestionType.Text.
        /// </remarks>
        /// <param name="suggestion">The suggestion to search from</param>
        /// <param name="filter">The filter of the search. Will always have an effect even when the Suggestion is not of type SuggestionType.Text</param>
        /// <returns>A SearchResult object containing the search results and methods to get more items.</returns>
        public async static Task<SearchResult> SearchAsync(Suggestion suggestion, MusicSearchFilter filter = MusicSearchFilter.Songs)
        {
            switch (suggestion.SuggestionType)
            {
                case SuggestionType.Text: return await SearchAsync(suggestion.Name, filter);
                case SuggestionType.Media:
                    SearchResult res = new();
                    if (filter == MusicSearchFilter.Episodes || filter == MusicSearchFilter.Songs || filter == MusicSearchFilter.Videos || filter == MusicSearchFilter.Podcasts)
                        res.Results = [(YoutubeMedia)suggestion.LinkedYoutubeElement];
                    return res;
                case SuggestionType.Playlist:
                    SearchResult res2 = new();
                    if (filter == MusicSearchFilter.Playlists || filter == MusicSearchFilter.CommunityPlaylists || filter == MusicSearchFilter.Albums)
                        res2.Results = [(YoutubePlaylist)suggestion.LinkedYoutubeElement];
                    return res2;
                case SuggestionType.Author:
                    SearchResult res3 = new();
                    if (filter == MusicSearchFilter.Artists ||filter == MusicSearchFilter.Users)
                        res3.Results = [(Author)suggestion.LinkedYoutubeElement];
                    return res3;

                default: return new();
            }
        }

        /// <summary>
        /// Search on Youtube Music.
        /// </summary>
        /// <param name="query">The query of the search</param>
        /// <param name="filter">The filter of the search. Default is Songs</param>
        /// <returns>A SearchResult object containing the search results and methods to get more items.</returns>
        public async static Task<SearchResult> SearchAsync(string query, MusicSearchFilter filter = MusicSearchFilter.Songs)
        {
            // Create the payload for the request
            dynamic payload = _target.WebClient.BaseClientPayload;
            payload.query = HttpUtility.HtmlEncode(query); // The search parameter
            payload.@params = filter.GetParam();

            string res = await Shared.RequestAsync(payload, _target.WebClient, "search");

            return new SearchResult(res, filter, _target.WebClient);
        }

        /// <summary>
        /// Get the homepage of the Youtube Music website
        /// </summary>
        /// <returns>The homepage as a collection of MusicShelf objects</returns>
        public async static Task<MediaShelfCollection> GetHomeAsync()
        {
            dynamic payload = _target.WebClient.BaseClientPayload;
            payload.browseId = "FEmusic_home";

            string json = await Shared.RequestAsync(payload, _target.WebClient, "browse");

            return MediaShelfCollection.FromJson(json);
        }

        /// <summary>
        /// Get additional information about a playlist.
        /// </summary>
        /// <param name="playlist">The playlist to get infomation from.</param>
        /// <returns>A PLaylistInfromation object containing the additional information of the playlist.</returns>
        public async static Task<PlaylistInformation> GetPlaylistInfoAsync(YoutubePlaylist playlist)
        {
            dynamic payload = _target.WebClient.BaseClientPayload;
            payload.browseId = playlist.Id;

            string json = await Shared.RequestAsync(payload, _target.WebClient, "browse");
            return PlaylistInformation.FromJson(json);
        }

        /// <summary>
        /// Get the playlist assiociated with the given id
        /// </summary>
        /// <param name="id">The videoId of the playlist</param>
        /// <returns></returns>
        public async static Task<YoutubePlaylist> GetPlaylistAsync(string id)
        {
            dynamic payload = _target.WebClient.BaseClientPayload;
            payload.playlistId = id;
            payload.isAudioOnly = true;
            payload.@params = id.StartsWith("RD") ? "wAEB" : "MPREb_XLbJEEU4CpR";

            string json = await Shared.RequestAsync(payload, _target.WebClient, "next");
            return YoutubePlaylist.FromJson(json, id.StartsWith("RD"));
        }

        /// <summary>
        /// Get the player of a media.
        /// </summary>
        /// <param name="video">The media to get the player.</param>
        /// <returns>A Player object containing the list of available formats and additionnal information about the media</returns>
        public async static Task<Player> GetPlayerAsync(YoutubeMedia video) => await GetPlayerAsync(video.Id);

        /// <summary>
        /// Get the player of a media.
        /// </summary>
        /// <param name="videoId">The id of the media to get the player.</param>
        /// <returns>A Player object containing the list of available formats and additionnal information about the media</returns>
        public async static Task<Player> GetPlayerAsync(string videoId)
        {
            dynamic payload = _target.IOSClient.BaseClientPayload;
            payload.videoId = videoId;

            string json = await Shared.RequestAsync(payload, _target.IOSClient, "player");

            return new Player(FormatCollection.FromJson(json), json);
        }

        /// <summary>
        /// Get the suggestions for a query.
        /// </summary>
        /// <param name="query">The query to get suggestions from.</param>
        /// <returns></returns>
        public async static Task<SuggestionCollection> GetSuggestionsAsync(string query)
        {
            dynamic payload = _target.WebClient.BaseClientPayload;
            payload.input = query;

            string json = await Shared.RequestAsync(payload, _target.WebClient, "music/get_search_suggestions");

            return SuggestionCollection.FromYoutubeMusicJson(json);
        }
    }
}
