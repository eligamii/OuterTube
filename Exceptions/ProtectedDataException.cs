using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube.Exceptions
{
    [System.Serializable]
    public class ProtectedDataException : Exception
    {
        public ProtectedDataException() { }
        public ProtectedDataException(string message) : base(message) { }
    }
}
