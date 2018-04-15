namespace AquaScript.Compiler
{
    [System.Serializable]
    public class InvalidTokenException : System.Exception
    {
        public InvalidTokenException() { }
        public InvalidTokenException(string message) { }
        public InvalidTokenException(string message, System.Exception inner) : base(message, inner) { }
    }
}
