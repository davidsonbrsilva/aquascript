using System;

namespace AquaScript.Compiler
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class LexemeAttribute : Attribute
    {
        public string Text { get; }

        public LexemeAttribute(string text)
        {
            Text = text;
        }
    }
}
