namespace OuterTube.Enums
{
    public enum MusicSearchFilter
    {
        Episodes,
        Podcasts,
        Titles,
        Videos,
        Users,
        Artists,
        Albums,
        Playlists,
        CommunityPlaylists
    }

    internal static class SearchFilters
    {
        internal static string GetParam(this MusicSearchFilter filter) => filter switch
        {
            MusicSearchFilter.Episodes => "EgWKAQJIAWoSEBAQERADEAQQCRAOEAoQBRAV",
            MusicSearchFilter.Podcasts => "EgWKAQJQAWoSEBAQERADEAQQCRAOEAoQBRAV",
            MusicSearchFilter.Titles => "EgWKAQIIAWoSEBAQERADEAQQCRAOEAoQBRAV",
            MusicSearchFilter.Videos => "EgWKAQIQAWoSEBAQERADEAQQCRAOEAoQBRAV",
            MusicSearchFilter.Albums => "EgWKAQIYAWoQEAMQBBAJEAoQBRAREBAQFQ%3D%3D",
            MusicSearchFilter.Artists => "EgWKAQIgAWoSEBAQERADEAQQCRAOEAoQBRAV",
            MusicSearchFilter.Playlists => "EgeKAQQoADgBahIQEBAREAMQBBAJEA4QChAFEBU%3D",
            MusicSearchFilter.CommunityPlaylists => "EgeKAQQoAEABahIQEBAREAMQBBAJEA4QChAFEBU%3D",
            MusicSearchFilter.Users => "EgWKAQJYAWoSEAMQBBAJEA4QChAFEBEQEBAV",
            _ => ""
        };

    }
}
