using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AquaScript
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
        /// Addition operator.
        /// </summary>
        [Lexeme("+")]       Addition,
        /// <summary>
        /// Subtraction operator.
        /// </summary>
        [Lexeme("-")]       Subtraction,
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
        /// Code for opening bracket symbol.
        /// </summary>
        [Lexeme("{")]       OpeningBracket,
        /// <summary>
        /// Code for closing bracket symbol.
        /// </summary>
        [Lexeme("}")]       ClosingBracket,
        /// <summary>
        /// Code for opening parenthesis symbol.
        /// </summary>
        [Lexeme("(")]       OpeningParenthesis,
        /// <summary>
        /// Code for closing parenthesis symbol.
        /// </summary>
        [Lexeme(")")]       ClosingParenthesis,
        /// <summary>
        /// Equal operator.
        /// </summary>
        [Lexeme("=")]       Equal,
        /// <summary>
        /// Less operator.
        /// </summary>
        [Lexeme("<=")]      LessOrEqual,
        /// <summary>
        /// Higher or equal operator.
        /// </summary>
        [Lexeme(">=")]      HigherOrEqual,
        /// <summary>
        /// Less operator.
        /// </summary>
        [Lexeme("<")]       Less,
        /// <summary>
        /// Higher operator.
        /// </summary>
        [Lexeme(">")]       Higher,
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
        [Lexeme("")]        Number,
        /// <summary>
        /// Textual value.
        /// </summary>
        [Lexeme("")]        Text,
        /// <summary>
        /// Parameters separator.
        /// </summary>
        [Lexeme(",")]       Colon,
        /// <summary>
        /// End of line separator.
        /// </summary>
        [Lexeme(";")]       Semicolon,
        /// <summary>
        /// Invalid token.
        /// </summary>
        [Lexeme("")]        Invalid,
        /// <summary>
        /// Attribuition operator.
        /// </summary>
        [Lexeme(":")]       Attribuition,
        /// <summary>
        /// Code for identification lexeme.
        /// </summary>
        [Lexeme("null")]    Null,
        /// <summary>
        /// Code for identification lexeme.
        /// </summary>
        [Lexeme("")]        Id
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
