using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AquaScript.Compiler
{
    /// <summary>
    /// A list of valid tokens for the project.
    /// </summary>
    public enum TokenCode
    {
        /// <summary>
        /// Keyword for return value.
        /// </summary>
        [Lexeme("return")]  Return,
        /// <summary>
        /// Keyword for read a new data.
        /// </summary>
        [Lexeme("read")]    Read,
        /// <summary>
        /// Keyword for write an expression.
        /// </summary>
        [Lexeme("write")]   Write,
        /// <summary>
        /// Keyword for end of flow control.
        /// </summary>
        [Lexeme("break")]   Break,
        /// <summary>
        /// Plus operator.
        /// </summary>
        [Lexeme("+")]       Plus,
        /// <summary>
        /// Minus operator.
        /// </summary>
        [Lexeme("-")]       Minus,
        /// <summary>
        /// Multiplication operator.
        /// </summary>
        [Lexeme("*")]       Multiplication,
        /// <summary>
        /// Division operator.
        /// </summary>
        [Lexeme("/")]       Division,
        /// <summary>
        /// Module operator.
        /// </summary>
        [Lexeme("%")]       Module,
        /// <summary>
        /// Increment operator.
        /// </summary>
        [Lexeme("++")]      Increment,
        /// <summary>
        /// Decrement operator.
        /// </summary>
        [Lexeme("--")]      Decrement,
        /// <summary>
        /// Keyword for "if" flow control.
        /// </summary>
        [Lexeme("if")]      If,
        /// <summary>
        /// Keyword for "else" flow control.
        /// </summary>
        [Lexeme("else")]    Else,
        /// <summary>
        /// Keyword for "for" flow control.
        /// </summary>
        [Lexeme("for")]     For,
        /// <summary>
        /// Code for left bracket symbol.
        /// </summary>
        [Lexeme("{")]       LeftBracket,
        /// <summary>
        /// Code for right bracket symbol.
        /// </summary>
        [Lexeme("}")]       RightBracket,
        /// <summary>
        /// Code for left parenthesis symbol.
        /// </summary>
        [Lexeme("(")]       LeftParenthesis,
        /// <summary>
        /// Code for right parenthesis symbol.
        /// </summary>
        [Lexeme(")")]       RightParenthesis,
        /// <summary>
        /// Equal operator.
        /// </summary>
        [Lexeme("=")]       Equal,
        /// <summary>
        /// Less operator.
        /// </summary>
        [Lexeme("<=")]      LessOrEqual,
        /// <summary>
        /// Greater or equal operator.
        /// </summary>
        [Lexeme(">=")]      GreaterOrEqual,
        /// <summary>
        /// Less operator.
        /// </summary>
        [Lexeme("<")]       Less,
        /// <summary>
        /// Greater operator.
        /// </summary>
        [Lexeme(">")]       Greater,
        /// <summary>
        /// Different operator.
        /// </summary>
        [Lexeme("!=")]      Different,
        /// <summary>
        /// Logical negation operator.
        /// </summary>
        [Lexeme("!")]       Negation,
        /// <summary>
        /// Logical intersection operator.
        /// </summary>
        [Lexeme("and")]     And,
        /// <summary>
        /// Logical union operator.
        /// </summary>
        [Lexeme("or")]      Or,
        /// <summary>
        /// Boolean value.
        /// </summary>
        [Lexeme("true")]    True,
        /// <summary>
        /// Boolean value.
        /// </summary>
        [Lexeme("false")]   False,
        /// <summary>
        /// Numerical value.
        /// </summary>
        [Lexeme("<number>")]        Number,
        /// <summary>
        /// Textual value.
        /// </summary>
        [Lexeme("<text>")]        Text,
        /// <summary>
        /// Parameters separator.
        /// </summary>
        [Lexeme(",")]       Comma,
        /// <summary>
        /// End of line separator.
        /// </summary>
        [Lexeme(";")]       Semicolon,
        /// <summary>
        /// Attribuition operator.
        /// </summary>
        [Lexeme(":")]       Colon,
        /// <summary>
        /// Code for identification lexeme.
        /// </summary>
        [Lexeme("null")]    Null,
        /// <summary>
        /// Code for identification lexeme.
        /// </summary>
        [Lexeme("<identifier>")]        Id
    }

    public static class TokenCodeExtensions
    {
        public static string GetLexeme(this TokenCode value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (LexemeAttribute)fieldInfo.GetCustomAttribute(typeof(LexemeAttribute));
            return attribute.Text;
        }
    }
}
