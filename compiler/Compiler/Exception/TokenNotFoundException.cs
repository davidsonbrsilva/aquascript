namespace AquaScript.Compiler
{
    [System.Serializable]
    public class TokenNotFoundException : System.Exception
    {
        public TokenNotFoundException() { }
        public TokenNotFoundException(string message) { }
        public TokenNotFoundException(string message, System.Exception inner) : base(message, inner) { }
    }
}
