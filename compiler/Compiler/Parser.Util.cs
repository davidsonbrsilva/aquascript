using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaScript.Compiler
{
    public partial class Parser
    {
        private void NextToken()
        {
            try
            {
                currentToken = lexicalAnalyzer.Read();
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Token LookAhead(Token current, int index)
        {
            try
            {
                return lexicalAnalyzer.LookAhead(current, index);
            }
            catch (TokenNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return currentToken;
            }
        }

        public void SyntaxError(Token token, TokenCode expected)
        {
            throw new SyntaxErrorException(string.Format("Syntax error at line {0}, column {1}: The token {2} provided is invalid. Expected: {3}", token.Line, token.Column, token.Lexeme, expected.GetLexeme()));
        }

        public void SyntaxError(Token token, TokenCode[] expected)
        {
            string expectedText = "";

            if (expected.Length < 2)
            {
                expectedText += expected[0].GetLexeme();
            }
            else
            {
                expectedText += expected[0].GetLexeme();

                for (int i = 1; i < expected.Length; ++i)
                {
                    if (i == expected.Length - 1)
                    {
                        expectedText = " ou " + expected[i].GetLexeme();
                    }
                    else
                    {
                        expectedText = ", " + expected[i].GetLexeme();
                    }
                }
            }

            throw new SyntaxErrorException(string.Format("Syntax error at line {0}, column {1}: The token {2} provided is invalid. Expected: {3}", token.Line, token.Column, token.Lexeme, expectedText));
        }
    }
}
