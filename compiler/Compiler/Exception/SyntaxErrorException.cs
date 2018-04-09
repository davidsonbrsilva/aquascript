namespace AquaScript.Compiler
{
    [System.Serializable]
    public class SyntaxErrorException : System.Exception
    {
        public SyntaxErrorException() : base() { }
        public SyntaxErrorException(string message) : base(message) { }
        public SyntaxErrorException(string message, System.Exception inner) : base(message, inner) { }
    }
}
