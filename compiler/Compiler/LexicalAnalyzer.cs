using System;
using System.Collections.Generic;
using System.IO;

namespace AquaScript.Compiler
{
    /// <summary>
    /// Lexical Analyzer class.
    /// </summary>
    public class LexicalAnalyzer
    {
        // Private attributes

        private StreamReader reader;
        private int lexicalCounter;
        private int currentLine;
        private int currentColumn;
        private int nextCharCode;
        private char nextChar;
        private int nextToken;

        // Properties

        /// <summary>
        /// The list of tokens generated from the source file.
        /// </summary>
        public List<Token> Tokens { get; private set; }

        // Constructors

        /// <summary>
        /// Create a Lexical Analyzer object.
        /// </summary>
        /// <param name="path">Path of font file.</param>
        public LexicalAnalyzer(string path)
        {
            reader = new StreamReader(path);
            lexicalCounter = 0;
            currentLine = 1;
            currentColumn = 1;
            nextCharCode = reader.Read();
            nextChar = (char)nextCharCode;
            nextToken = -1;
            Tokens = new List<Token>();

            Tokenize();
        }

        // Public methods

        public Token LookAhead(Token current, int index)
        {
            index = Tokens.IndexOf(current) + index;

            if (index >= Tokens.Count)
            {
                throw new TokenNotFoundException(string.Format("Attempting to read the parser to remove ambiguity after token {0} in line {1}, column {2}. No token was found.", Tokens[nextToken].Lexeme, Tokens[nextToken].Line, Tokens[nextToken].Column));
            }

            return Tokens[index];
        }

        public Token Read()
        {
            nextToken++;

            if (nextToken >= Tokens.Count)
            {
                throw new EndOfStreamException("End of parser reading.");
            }

            return Tokens[nextToken];
        }

        /// <summary>
        /// Read the font file specified in the constructor and tokenize it.
        /// </summary>
        public void Tokenize()
        {
            while (nextCharCode != -1)
            {
                try
                {
                    Token token = NextToken();
                    Tokens.Add(token);
                }
                catch (InvalidTokenException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (EndOfStreamException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //Console.WriteLine("Esta foi a lista de tokens gerada:");
            //WriteTokensList();
        }

        /// <summary>
        /// Writes the tokens list on the console.
        /// </summary>
        public void WriteTokensList()
        {
            string lexeme = "Lexeme:";
            string code = "Code:";
            string line = "Line:";
            string column = "Column:";

            if (Tokens.Count == 0)
            {
                Console.WriteLine("No token found.");
            }
            else
            {
                Console.WriteLine("{0, -15}{1, -20}{2, 10}{3, 10}", lexeme, code, line, column);

                foreach (Token token in Tokens)
                {
                    Console.WriteLine(" {0,-14} {1,-19}{2,9}{3,10}", token.Lexeme, token.Code, token.Line, token.Column);
                }
            }
        }

        // Private methods

        /// <summary>
        /// Get the next char in the read stream.
        /// </summary>
        private void NextChar()
        {
            nextCharCode = reader.Read();
            nextChar = (char)nextCharCode;
            UpdatePosition();
        }

        /// <summary>
        /// Controls the row and column change.
        /// </summary>
        private void UpdatePosition()
        {
            char nextChar = (char)nextCharCode;

            if (nextChar.Equals('\n'))
            {
                currentColumn = 1;
                currentLine += 1;
            }
            else
            {
                currentColumn++;
            }
        }

        /// <summary>
        /// Get a token from the file.
        /// </summary>
        /// <returns></returns>
        public Token NextToken()
        {
            Token token = null;
            String lexeme = "";

            while (Char.IsWhiteSpace(nextChar))
            {
                NextChar();
            }

            if (nextCharCode == -1)
            {
                throw new EndOfStreamException("End of file reading.");
            }

            int tokenLine = currentLine;
            int tokenColumn = currentColumn;

            if (nextChar == '_' || Char.IsLetter(nextChar))
            {
                lexeme += nextChar;
                NextChar();

                while (nextChar.Equals('_') || Char.IsLetterOrDigit(nextChar))
                {
                    lexeme += nextChar;
                    NextChar();
                }

                token = new Token(lexeme, TokenCode.Id, tokenLine, tokenColumn);
                LookUp(token);
            }
            else if (Char.IsDigit(nextChar))
            {
                lexeme += nextChar;
                NextChar();

                while (Char.IsDigit(nextChar))
                {
                    lexeme += nextChar;
                    NextChar();
                }

                if (nextChar.Equals('.'))
                {
                    lexeme += nextChar;
                    NextChar();

                    if (Char.IsDigit(nextChar))
                    {
                        lexeme += nextChar;
                        NextChar();

                        while (Char.IsDigit(nextChar))
                        {
                            lexeme += nextChar;
                            NextChar();
                        }
                    }
                    else
                    {
                        lexicalCounter++;
                        InvalidTokenError("Invalid token \"" + lexeme + "\".", tokenLine, tokenColumn);
                    }
                }

                token = new Token(lexeme, TokenCode.Number, tokenLine, tokenColumn);
            }
            else if (nextChar.Equals('"'))
            {
                lexeme += nextChar;
                NextChar();

                while (nextChar != '"')
                {
                    lexeme += nextChar;
                    NextChar();

                    if (nextCharCode == -1)
                    {
                        lexicalCounter++;
                        throw new EndOfStreamException("Lexical error at line " + tokenLine + ", column " + tokenColumn + ": Variables of text type must be envolved with quotation marks (\").");
                    }
                }

                lexeme += nextChar;
                token = new Token(lexeme, TokenCode.Text, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals('+'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Plus, tokenLine, tokenColumn);
                NextChar();

                if (nextChar.Equals('+'))
                {
                    token.Lexeme += nextChar.ToString();
                    token.Code = TokenCode.Increment;
                    NextChar();
                }
            }
            else if (nextChar.Equals('-'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Minus, tokenLine, tokenColumn);
                NextChar();

                if (nextChar.Equals('-'))
                {
                    token.Lexeme += nextChar.ToString();
                    token.Code = TokenCode.Decrement;
                    NextChar();
                }
            }
            else if (nextChar.Equals('*'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Multiplication, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals('/'))
            {
                NextChar();

                if (nextChar.Equals('/'))
                {
                    NextChar();

                    while (nextChar != '\n' && nextCharCode != -1)
                    {
                        NextChar();
                    }

                    token = NextToken();
                }
                else if (nextChar.Equals('*'))
                {
                    NextChar();

                    while (true)
                    {
                        while (nextChar != '*' && nextCharCode != -1)
                        {
                            NextChar();
                        }

                        if (nextCharCode == -1)
                        {
                            lexicalCounter++;
                            throw new EndOfStreamException("Lexical error at line " + tokenLine + ", column " + tokenColumn + ": Multiline comments must be finished with \"*/\".");
                        }

                        if (nextChar.Equals('*'))
                        {
                            NextChar();

                            while (nextChar.Equals('*') && nextCharCode != -1)
                            {
                                NextChar();
                            }

                            if (nextCharCode == -1)
                            {
                                lexicalCounter++;
                                throw new EndOfStreamException("Lexical error at line " + tokenLine + ", column " + tokenColumn + ": Multiline comments must be finished with \"*/\".");
                            }

                            if (nextChar.Equals('/'))
                            {
                                NextChar();
                                token = NextToken();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    token = new Token(nextChar.ToString(), TokenCode.Division, tokenLine, tokenColumn);
                }
            }
            else if (nextChar.Equals('%'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Module, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals('<'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Less, tokenLine, tokenColumn);
                NextChar();

                if (nextChar.Equals('='))
                {
                    token.Lexeme += nextChar;
                    token.Code = TokenCode.LessOrEqual;
                    NextChar();
                }
            }
            else if (nextChar.Equals('>'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Greater, tokenLine, tokenColumn);
                NextChar();

                if (nextChar.Equals('='))
                {
                    token.Lexeme += nextChar;
                    token.Code = TokenCode.GreaterOrEqual;
                    NextChar();
                }
            }
            else if (nextChar.Equals('='))
            {
                token = new Token(nextChar.ToString(), TokenCode.Equal, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals(':'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Colon, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals('!'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Negation, tokenLine, tokenColumn);
                NextChar();

                if (nextChar.Equals('='))
                {
                    token.Lexeme += nextChar;
                    token.Code = TokenCode.Different;
                    NextChar();
                }
            }
            else if (nextChar.Equals('('))
            {
                token = new Token(nextChar.ToString(), TokenCode.LeftParenthesis, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals(')'))
            {
                token = new Token(nextChar.ToString(), TokenCode.RightParenthesis, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals('{'))
            {
                token = new Token(nextChar.ToString(), TokenCode.LeftBracket, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals('}'))
            {
                token = new Token(nextChar.ToString(), TokenCode.RightBracket, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals(';'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Semicolon, tokenLine, tokenColumn);
                NextChar();
            }
            else if (nextChar.Equals(','))
            {
                token = new Token(nextChar.ToString(), TokenCode.Comma, tokenLine, tokenColumn);
                NextChar();
            }
            else
            {
                lexeme += nextChar;
                NextChar();

                while (IsInvalid(nextCharCode))
                {
                    lexeme += nextChar;
                    NextChar();
                }

                lexicalCounter++;
                InvalidTokenError("Invalid token \"" + lexeme + "\".", tokenLine, tokenColumn);
            }

            return token;
        }

        private bool IsInvalid(int charactereCode)
        {
            char charactere = (char)charactereCode;

            if (Char.IsLetterOrDigit(charactere)
                || Char.IsWhiteSpace(charactere)
                || charactereCode == -1
                || charactere == '_'
                || charactere == '/'
                || charactere == '*'
                || charactere == '+'
                || charactere == '-'
                || charactere == ','
                || charactere == ';'
                || charactere == '('
                || charactere == ')'
                || charactere == '{'
                || charactere == '}'
                || charactere == ':'
                || charactere == '!'
                || charactere == '='
                || charactere == '<'
                || charactere == '>'
                || charactere == '"'
                )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if the found lexeme is a Keyword or an Identifier.
        /// </summary>
        /// <param name="token">The built token.</param>
        private void LookUp(Token token)
        {
            foreach (TokenCode tokenCode in Enum.GetValues(typeof(TokenCode)))
            {
                if (token.Lexeme.Equals(tokenCode.GetLexeme()))
                {
                    token.Code = tokenCode;
                    return;
                }
            }
        }

        private void InvalidTokenError(string message, int line, int column)
        {
            throw new InvalidTokenException(string.Format("Lexical error at line {0}, column {1}: {2}", line, column, message));
        }
    }
}
