using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaScript
{
    /// <summary>
    /// Structure to store a generated token.
    /// </summary>
    public class Token
    {
        public string Lexeme { get; set; }
        public TokenCode Code { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        /// <summary>
        /// Create a new Token.
        /// </summary>
        /// <param name="lexeme">Lexeme is the basic unit of the lexicon.</param>
        /// <param name="code">The token code to the formed lexeme.</param>
        /// <param name="line">The line location of the token.</param>
        /// <param name="column">The column location of the token.</param>
        public Token(string lexeme, TokenCode code = TokenCode.Id, int line = 1, int column = 0)
        {
            Lexeme = lexeme;
            Code = code;
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return string.Format("Lexeme: {0}\tCode: {1}\tLine: {2}\tColumn: {3}", Lexeme, Code, Line, Column);
        }
    }
}
