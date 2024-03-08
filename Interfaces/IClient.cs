using OuterTube.Enums;

namespace OuterTube.Interfaces
{
    public interface IClient
    {
        internal ClientName ClientName { get; }
        internal string Version { get; }
        internal string BaseUrl { get; }
        internal string ApiKey { get; }


    }
}
