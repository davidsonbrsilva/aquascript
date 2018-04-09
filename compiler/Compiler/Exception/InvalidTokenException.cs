namespace AquaScript.Compiler
{
    [System.Serializable]
    public class InvalidTokenException : System.Exception
    {
        public InvalidTokenException() : base() { }
        public InvalidTokenException(string message) : base(message) { }
        public InvalidTokenException(string message, System.Exception inner) : base(message, inner) { }
    }
}
