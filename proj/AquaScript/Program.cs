using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaScript
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
