using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Enums
{
    public enum SearchFilter
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
        internal static string GetParam(this SearchFilter filter) => filter switch
        {
            SearchFilter.Episodes           => "EgWKAQJIAWoSEBAQERADEAQQCRAOEAoQBRAV",
            SearchFilter.Podcasts           => "EgWKAQJQAWoSEBAQERADEAQQCRAOEAoQBRAV",
            SearchFilter.Titles             => "EgWKAQIIAWoSEBAQERADEAQQCRAOEAoQBRAV",
            SearchFilter.Videos             => "EgWKAQIQAWoSEBAQERADEAQQCRAOEAoQBRAV",
            SearchFilter.Albums             => "EgWKAQIYAWoQEAMQBBAJEAoQBRAREBAQFQ%3D%3D",
            SearchFilter.Artists            => "EgWKAQIgAWoSEBAQERADEAQQCRAOEAoQBRAV",
            SearchFilter.Playlists          => "EgeKAQQoADgBahIQEBAREAMQBBAJEA4QChAFEBU%3D",
            SearchFilter.CommunityPlaylists => "EgeKAQQoAEABahIQEBAREAMQBBAJEA4QChAFEBU%3D",
            SearchFilter.Users              => "EgWKAQJYAWoSEAMQBBAJEA4QChAFEBEQEBAV",
            _ => ""
        };
        
    }
}
