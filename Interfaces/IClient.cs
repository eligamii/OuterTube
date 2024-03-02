using OuterTube.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
