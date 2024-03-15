namespace OuterTube.Exceptions
{
    [System.Serializable]
    public class NotSignedInException : Exception
    {
        public NotSignedInException() { }
        public NotSignedInException(string message) : base(message) { }
    }
}
