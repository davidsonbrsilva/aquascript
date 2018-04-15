using System;
using System.Collections.Generic;
using System.IO;

namespace AquaScript.Compiler
{
    public partial class Parser
    {
        private LexicalAnalyzer lexicalAnalyzer;
        private Token currentToken;

        public Parser(LexicalAnalyzer lexicalAnalyzer)
        {
            this.lexicalAnalyzer = lexicalAnalyzer;
            NextToken();

            Program();
        }

        public void Program()
        {
            while (currentToken != null && IsStatement(currentToken))
            {
                Statement();
            }
        }

        private void Statement()
        {
            if (IsIf(currentToken))
            {
                If();
            }
            else if (IsFor(currentToken))
            {
                For();
            }
            else if (IsAttribuition(currentToken))
            {
                Attribuition();
            }
            else if (IsFunctionCall(currentToken))
            {
                FunctionCall();

                if (currentToken.Code == TokenCode.Semicolon)
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Semicolon);
                }
            }
            else if (IsIncrement(currentToken))
            {
                Increment();

                if (currentToken.Code == TokenCode.Semicolon)
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Semicolon);
                }
            }
            else if (IsDecrement(currentToken))
            {
                Decrement();

                if (currentToken.Code == TokenCode.Semicolon)
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Semicolon);
                }
            }
            else if (IsReturn(currentToken))
            {
                Return();

                if (currentToken.Code == TokenCode.Semicolon)
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Semicolon);
                }
            }
            else if (IsRead(currentToken))
            {
                Read();

                if (currentToken.Code == TokenCode.Semicolon)
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Semicolon);
                }
            }
            else if (IsWrite(currentToken))
            {
                Write();

                if (currentToken.Code == TokenCode.Semicolon)
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Semicolon);
                }
            }
            else if (currentToken.Code.Equals(TokenCode.Break))
            {
                NextToken();

                if (currentToken.Code.Equals(TokenCode.Semicolon))
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Semicolon);
                }
            }
            else
            {
                TokenCode[] expected = new TokenCode[]
                {
                    TokenCode.If,
                    TokenCode.For,
                    TokenCode.Id,
                    TokenCode.Return,
                    TokenCode.Read,
                    TokenCode.Write,
                    TokenCode.Break
                };
                SyntaxError(currentToken, expected);
            }
        }

        private void If()
        {
            if (IsIf(currentToken))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.If);
            }

            if (currentToken.Code.Equals(TokenCode.LeftParenthesis))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.LeftParenthesis);
            }

            Expression();

            if (currentToken.Code.Equals(TokenCode.RightParenthesis))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.RightParenthesis);
            }

            Body();

            while (IsElseIf(currentToken))
            {
                if (IsElse(currentToken))
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Else);
                }

                if (IsIf(currentToken))
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.If);
                }

                if (currentToken.Code.Equals(TokenCode.LeftParenthesis))
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.LeftParenthesis);
                }

                Expression();

                if (currentToken.Code.Equals(TokenCode.RightParenthesis))
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.RightParenthesis);
                }

                Body();
            }

            if (IsElse(currentToken))
            {
                NextToken();
                Body();
            }
        }

        private void For()
        {
            if (IsFor(currentToken))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.For);
            }

            if (currentToken.Code.Equals(TokenCode.LeftParenthesis))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.LeftParenthesis);
            }

            VarDecl();

            if (currentToken.Code.Equals(TokenCode.Semicolon))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Semicolon);
            }

            Expression();

            if (currentToken.Code.Equals(TokenCode.Semicolon))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Semicolon);
            }

            if (IsAttribuition(currentToken))
            {
                VarDecl();
            }
            else if (IsIncrement(currentToken))
            {
                Increment();
            }
            else if (IsDecrement(currentToken))
            {
                Decrement();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id); // revisar
            }

            if (currentToken.Code.Equals(TokenCode.RightParenthesis))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.RightParenthesis);
            }

            Body();
        }

        private void Attribuition()
        {
            if (IsFunctionDecl(currentToken))
            {
                FunctionDecl();
            }
            else if (IsVarDecl(currentToken))
            {
                VarDecl();

                if (currentToken.Code.Equals(TokenCode.Semicolon))
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.Semicolon);
                }
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id); // Revisar
            }
        }

        private void VarDecl()
        {

            if (currentToken.Code.Equals(TokenCode.Id))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id);
            }

            if (currentToken.Code.Equals(TokenCode.Colon))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Colon);
            }

            Expression();
        }

        private void FunctionDecl()
        {
            if (currentToken.Code.Equals(TokenCode.Id))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id);
            }

            if (currentToken.Code.Equals(TokenCode.Colon))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Colon);
            }

            Function();
        }

        private void Increment()
        {
            if (currentToken.Code.Equals(TokenCode.Id))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id);
            }

            if (currentToken.Code.Equals(TokenCode.Increment))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Increment);
            }
        }

        private void Decrement()
        {
            if (currentToken.Code.Equals(TokenCode.Id))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id);
            }

            if (currentToken.Code.Equals(TokenCode.Decrement))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Decrement);
            }
        }

        private void Return()
        {
            if (currentToken.Code.Equals(TokenCode.Return))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Return);
            }

            if (IsExpression(currentToken))
            {
                Expression();
            }
        }

        private void Read()
        {
            if (currentToken.Code.Equals(TokenCode.Read))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Read);
            }

            if (currentToken.Code.Equals(TokenCode.Id))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id);
            }
        }

        private void Write()
        {
            if (currentToken.Code.Equals(TokenCode.Write))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Write);
            }

            if (IsExpression(currentToken))
            {
                Expression();
            }
            else // Revisar
            {
                TokenCode[] expected = new TokenCode[]
                {
                    TokenCode.And
                };
                SyntaxError(currentToken, expected);
            }
        }

        private void Function()
        {
            if (currentToken.Code.Equals(TokenCode.LeftParenthesis))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.LeftParenthesis);
            }

            if (IsParam(currentToken))
            {
                Param();
            }

            if (currentToken.Code.Equals(TokenCode.RightParenthesis))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.RightParenthesis);
            }

            Body();
        }

        private void Param()
        {
            if (currentToken.Code.Equals(TokenCode.Id))
            {
                NextToken();

                while (currentToken.Code.Equals(TokenCode.Comma))
                {
                    NextToken();

                    if (currentToken.Code.Equals(TokenCode.Id))
                    {
                        NextToken();
                    }
                    else
                    {
                        SyntaxError(currentToken, TokenCode.Id);
                    }
                }
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id);
            }
        }

        private void Expression()
        {
            Side();

            if (IsConditionalOperator(currentToken))
            {
                NextToken();
                Side();
            }

            while (IsLogicalOperator(currentToken))
            {
                NextToken();
                Expression();
            }
        }

        private void Side()
        {
            Term();

            while (currentToken.Code.Equals(TokenCode.Plus) || currentToken.Code.Equals(TokenCode.Minus))
            {
                NextToken();
                Term();
            }
        }

        private void Term()
        {
            UnaryExpr();

            while (currentToken.Code.Equals(TokenCode.Multiplication) || currentToken.Code.Equals(TokenCode.Division) || currentToken.Code.Equals(TokenCode.Module))
            {
                NextToken();
                UnaryExpr();
            }
        }

        private void UnaryExpr()
        {
            if (currentToken.Code.Equals(TokenCode.Plus) || currentToken.Code.Equals(TokenCode.Minus))
            {
                NextToken();
            }

            Factor();
        }

        private void Factor()
        {
            if (IsFactor(currentToken))
            {
                if (currentToken.Code.Equals(TokenCode.LeftParenthesis))
                {
                    NextToken();
                    Expression();

                    if (currentToken.Code.Equals(TokenCode.RightParenthesis))
                    {
                        NextToken();
                    }
                    else
                    {
                        SyntaxError(currentToken, TokenCode.RightParenthesis);
                    }
                }
                else if (IsFunctionCall(currentToken))
                {
                    FunctionCall();
                }
                else
                {
                    NextToken();
                }
            }
            else
            {
                TokenCode[] expected = new TokenCode[]
                {
                    TokenCode.True,
                    TokenCode.False,
                    TokenCode.Number,
                    TokenCode.Text,
                    TokenCode.Id,
                    TokenCode.Null,
                    TokenCode.LeftParenthesis
                };
                SyntaxError(currentToken, expected);
            }
        }

        private void ParamCall()
        {
            if (IsParamCall(currentToken))
            {
                Expression();

                while (currentToken.Code.Equals(TokenCode.Comma))
                {
                    NextToken();

                    if (IsParamCall(currentToken))
                    {
                        Expression();
                    }
                    else
                    {
                        SyntaxError(currentToken, TokenCode.Id); // revisar
                    }
                }
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id);
            }
        }

        private void FunctionCall()
        {
            if (currentToken.Code.Equals(TokenCode.Id))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.Id);
            }

            if (currentToken.Code.Equals(TokenCode.LeftParenthesis))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.LeftParenthesis);
            }

            if (IsParamCall(currentToken))
            {
                ParamCall();
            }

            if (currentToken.Code.Equals(TokenCode.RightParenthesis))
            {
                NextToken();
            }
            else
            {
                SyntaxError(currentToken, TokenCode.RightParenthesis);
            }
        }

        private void Body()
        {
            if (currentToken.Code.Equals(TokenCode.LeftBracket))
            {
                NextToken();

                while (IsStatement(currentToken))
                {
                    Statement();
                }

                if (currentToken.Code.Equals(TokenCode.RightBracket))
                {
                    NextToken();
                }
                else
                {
                    SyntaxError(currentToken, TokenCode.RightBracket);
                }
            }
            else
            {
                SyntaxError(currentToken, TokenCode.LeftBracket);
            }
        }
    }
}