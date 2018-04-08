using System;
using System.Collections.Generic;
using System.IO;

namespace AquaScript
{
    /// <summary>
    /// Lexical Analyzer class.
    /// </summary>
    public class LexicalAnalyzer
    {
        // Private attributes

        private StreamReader reader;
        private int line;
        private int column;
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
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Arquivo não encontrado.");
            }

            reader = new StreamReader(path);
            line = 1;
            column = 1;
            nextCharCode = reader.Read();
            nextChar = (char)nextCharCode;
            nextToken = -1;
            Tokens = new List<Token>();

            Tokenize();
        }

        // Public methods

        public Token Read()
        {
            nextToken++;

            if (nextToken < Tokens.Count)
            {
                return Tokens[nextToken];
            }
            return null;
        }

        /// <summary>
        /// Read the font file specified in the constructor and tokenize it.
        /// </summary>
        public void Tokenize()
        {
            try
            {
                Token token = null;

                while (true)
                {
                    token = GetToken();
                    if (token != null)
                    {
                        Tokens.Add(token);
                    }
                }
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Esta foi a lista de tokens gerada:");
            WriteTokensList();
        }

        /// <summary>
        /// Writes the tokens list on the console.
        /// </summary>
        public void WriteTokensList()
        {
            string lexeme = "Lexema:";
            string code = "Código:";
            string line = "Linha:";
            string column = "Coluna:";

            if (Tokens.Count == 0)
            {
                Console.WriteLine("Nenhum token encontrado!");
            }
            else
            {
                Console.WriteLine("{0, -15}{1, -20}{2, 10}{3, 10}", lexeme, code, line, column);

                foreach (Token token in Tokens)
                {
                    if (token.Code == TokenCode.Invalid)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine(" {0,-14} {1,-19}{2,9}{3,10}", token.Lexeme, token.Code, token.Line, token.Column);
                }

                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        // Private methods

        /// <summary>
        /// Get the next char in the read stream.
        /// </summary>
        private void GetNextChar()
        {
            nextCharCode = reader.Read();
            nextChar = (char)nextCharCode;
            UpdateLineAndColumn();
        }

        /// <summary>
        /// Controls the row and column change.
        /// </summary>
        private void UpdateLineAndColumn()
        {
            char nextChar = (char)nextCharCode;

            if (nextChar.Equals('\n'))
            {
                column = 1;
                line += 1;
            }
            else
            {
                column++;
            }
        }

        /// <summary>
        /// Get a token from the file.
        /// </summary>
        /// <returns></returns>
        private Token GetToken()
        {
            Token token = null;
            String lexeme = "";

            while (Char.IsWhiteSpace(nextChar))
            {
                GetNextChar();
            }

            int tokenLine = line;
            int tokenColumn = column;

            if (nextCharCode == -1)
            {
                throw new EndOfStreamException("Arquivo lido com sucesso!");
            }

            if (nextChar == '_' || Char.IsLetter(nextChar))
            {
                lexeme += nextChar;
                GetNextChar();

                while (nextChar.Equals('_') || Char.IsLetterOrDigit(nextChar))
                {
                    lexeme += nextChar;
                    GetNextChar();
                }

                token = new Token(lexeme, TokenCode.Id, tokenLine, tokenColumn);
                LookUp(token);
            }
            else if (Char.IsDigit(nextChar))
            {
                lexeme += nextChar;
                GetNextChar();

                while (Char.IsDigit(nextChar))
                {
                    lexeme += nextChar;
                    GetNextChar();
                }

                token = new Token(lexeme, TokenCode.Number, tokenLine, tokenColumn);

                if (nextChar.Equals('.'))
                {
                    lexeme += nextChar;
                    GetNextChar();

                    if (Char.IsDigit(nextChar))
                    {
                        lexeme += nextChar;
                        GetNextChar();

                        while (Char.IsDigit(nextChar))
                        {
                            lexeme += nextChar;
                            GetNextChar();
                        }

                        token.Lexeme = lexeme;
                    }
                    else
                    {
                        token = new Token(lexeme, TokenCode.Invalid, tokenLine, tokenColumn);
                    }
                }
            }
            else if (nextChar.Equals('"'))
            {
                lexeme += nextChar;
                GetNextChar();

                while (nextChar != '"')
                {
                    lexeme += nextChar;
                    GetNextChar();

                    if (nextCharCode == -1)
                    {
                        throw new EndOfStreamException("Os valores das variáveis do tipo text devem ser envolvidos com aspas.");
                    }
                }

                lexeme += nextChar;
                token = new Token(lexeme, TokenCode.Text, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals('+'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Addition, tokenLine, tokenColumn);
                GetNextChar();

                if (nextChar.Equals('+'))
                {
                    token.Lexeme += nextChar.ToString();
                    GetNextChar();
                }
            }
            else if (nextChar.Equals('-'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Subtraction);
                GetNextChar();

                if (nextChar.Equals('-'))
                {
                    token.Lexeme += nextChar.ToString();
                    token.Code = TokenCode.Decrement;
                    GetNextChar();
                }
            }
            else if (nextChar.Equals('*'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Multiplication, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals('/'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Division, tokenLine, tokenColumn);
                GetNextChar();

                if (nextChar.Equals('/'))
                {
                    token = null;
                    GetNextChar();

                    while (nextChar != '\n' || nextChar != -1)
                    {
                        GetNextChar();
                    }
                }
                else if (nextChar.Equals('*'))
                {
                    token = null;
                    GetNextChar();

                    while (nextChar != '*')
                    {
                        GetNextChar();

                        if (nextCharCode == -1)
                        {
                            throw new EndOfStreamException("É necessário fechar comentários de multiplas linhas com \"*/\".");
                        }

                        if (nextChar.Equals('*'))
                        {
                            GetNextChar();

                            if (nextChar.Equals('/'))
                            {
                                GetNextChar();
                                break;
                            }
                        }
                    }
                }
            }
            else if (nextChar.Equals('<'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Less, tokenLine, tokenColumn);
                GetNextChar();

                if (nextChar.Equals('='))
                {
                    token.Lexeme += nextChar;
                    token.Code = TokenCode.LessOrEqual;
                    GetNextChar();
                }
            }
            else if (nextChar.Equals('>'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Higher, tokenLine, tokenColumn);
                GetNextChar();

                if (nextChar.Equals('='))
                {
                    token.Lexeme += nextChar;
                    token.Code = TokenCode.HigherOrEqual;
                    GetNextChar();
                }
            }
            else if (nextChar.Equals('='))
            {
                token = new Token(nextChar.ToString(), TokenCode.Equal, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals(':'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Attribuition, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals('!'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Negation, tokenLine, tokenColumn);
                GetNextChar();

                if (nextChar.Equals('='))
                {
                    token.Lexeme += nextChar;
                    token.Code = TokenCode.Different;
                    GetNextChar();
                }
            }
            else if (nextChar.Equals('('))
            {
                token = new Token(nextChar.ToString(), TokenCode.OpeningParenthesis, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals(')'))
            {
                token = new Token(nextChar.ToString(), TokenCode.ClosingParenthesis, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals('{'))
            {
                token = new Token(nextChar.ToString(), TokenCode.OpeningBracket, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals('}'))
            {
                token = new Token(nextChar.ToString(), TokenCode.ClosingBracket, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals(';'))
            {
                token = new Token(nextChar.ToString(), TokenCode.Semicolon, tokenLine, tokenColumn);
                GetNextChar();
            }
            else if (nextChar.Equals(','))
            {
                token = new Token(nextChar.ToString(), TokenCode.Colon, tokenLine, tokenColumn);
                GetNextChar();
            }
            else
            {
                token = new Token(nextChar.ToString(), TokenCode.Invalid, tokenLine, tokenColumn);
                GetNextChar();
            }

            return token;
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
    }
}
