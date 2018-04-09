using System;
using System.IO;

namespace AquaScript.Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer("source.aqua");
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
