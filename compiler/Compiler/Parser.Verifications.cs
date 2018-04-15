using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaScript.Compiler
{
    public partial class Parser
    {
        private bool IsStatement(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.If:
                case TokenCode.For:
                case TokenCode.Id:
                case TokenCode.Return:
                case TokenCode.Write:
                case TokenCode.Read:
                case TokenCode.Break:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsIf(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.If:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsElse(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Else:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsElseIf(Token token)
        {
            if (currentToken.Code.Equals(TokenCode.Else))
            {
                if (LookAhead(token, 1).Code.Equals(TokenCode.If))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsFor(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.For:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsAttribuition(Token token)
        {
            if (token.Code.Equals(TokenCode.Id))
            {
                if (LookAhead(token, 1).Code.Equals(TokenCode.Colon))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsIncrement(Token token)
        {
            if (token.Code.Equals(TokenCode.Id))
            {
                if (LookAhead(token, 1).Code.Equals(TokenCode.Increment))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsDecrement(Token token)
        {
            if (token.Code.Equals(TokenCode.Id))
            {
                if (LookAhead(token, 1).Code.Equals(TokenCode.Decrement))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsReturn(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Return:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsRead(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Read:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsWrite(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Write:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsExpression(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Id:
                case TokenCode.Plus:
                case TokenCode.Minus:
                case TokenCode.True:
                case TokenCode.False:
                case TokenCode.Number:
                case TokenCode.Text:
                case TokenCode.Null:
                case TokenCode.LeftParenthesis:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsBodyFirst(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.LeftBracket:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsVarDecl(Token token)
        {
            if (IsExpression(LookAhead(token, 2)))
            {
                return true;
            }

            return false;
        }

        private bool IsFunctionDecl(Token token)
        {
            if (IsFunction(LookAhead(token, 2)))
            {
                return true;
            }

            return false;
        }

        private bool IsFunction(Token token)
        {
            if (token.Code.Equals(TokenCode.LeftParenthesis))
            {
                if (LookAhead(token, 2).Code.Equals(TokenCode.RightParenthesis) || LookAhead(token, 2).Code.Equals(TokenCode.Comma) || LookAhead(token, 3).Code.Equals(TokenCode.LeftBracket))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsSide(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Id:
                case TokenCode.Plus:
                case TokenCode.Minus:
                case TokenCode.True:
                case TokenCode.False:
                case TokenCode.Number:
                case TokenCode.Text:
                case TokenCode.Null:
                case TokenCode.LeftParenthesis:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsParam(Token token)
        {
            switch(token.Code)
            {
                case TokenCode.Id:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsParamCall(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Id:
                case TokenCode.Plus:
                case TokenCode.Minus:
                case TokenCode.True:
                case TokenCode.False:
                case TokenCode.Number:
                case TokenCode.Text:
                case TokenCode.Null:
                case TokenCode.LeftParenthesis:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsTerm(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Id:
                case TokenCode.Plus:
                case TokenCode.Minus:
                case TokenCode.True:
                case TokenCode.False:
                case TokenCode.Number:
                case TokenCode.Text:
                case TokenCode.Null:
                case TokenCode.LeftParenthesis:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsUnaryExprFirst(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Id:
                case TokenCode.Plus:
                case TokenCode.Minus:
                case TokenCode.True:
                case TokenCode.False:
                case TokenCode.Number:
                case TokenCode.Text:
                case TokenCode.Null:
                case TokenCode.LeftParenthesis:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsFactor(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.Id:
                case TokenCode.True:
                case TokenCode.False:
                case TokenCode.Number:
                case TokenCode.Text:
                case TokenCode.Null:
                case TokenCode.LeftParenthesis:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsBool(Token token)
        {
            switch (token.Code)
            {
                case TokenCode.True:
                case TokenCode.False:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsFunctionCall(Token token)
        {
            if (token.Code.Equals(TokenCode.Id))
            {
                if (LookAhead(token, 1).Code.Equals(TokenCode.LeftParenthesis))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsConditionalOperator(Token token)
        {
            switch(token.Code)
            {
                case TokenCode.Equal:
                case TokenCode.Different:
                case TokenCode.Less:
                case TokenCode.Greater:
                case TokenCode.LessOrEqual:
                case TokenCode.GreaterOrEqual:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsLogicalOperator(Token token)
        {
            switch(token.Code)
            {
                case TokenCode.And:
                case TokenCode.Or:
                    return true;
                default:
                    return false;
            }
        }
    }
}
