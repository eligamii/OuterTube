namespace OuterTube.Exceptions
{
    [System.Serializable]
    public class ProtectedDataException : Exception
    {
        public ProtectedDataException() { }
        public ProtectedDataException(string message) : base(message) { }
    }
}
