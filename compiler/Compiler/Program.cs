using System;
using System.IO;

namespace AquaScript.Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "";

            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].ToLower().EndsWith(".aqua"))
                {
                    source = args[i];
                }
            }

            Header();

            try
            {
                LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(source);

                Console.WriteLine("Reading from file " + Path.GetFullPath(source));

                Parser parser = new Parser(lexicalAnalyzer);
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("File " + Path.GetFullPath(source) + " not found.");
            }
            catch(DirectoryNotFoundException)
            {
                Console.WriteLine("Part of the path " + Path.GetFullPath(source) + " was not found.");
            }
            catch(ArgumentNullException)
            {
                Console.WriteLine("Value can't be null. Enter a valid filename.");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Filename can not be empty. Enter a valid filename.");
            }
        }

        static void GetNextToken(LexicalAnalyzer lexicalAnalyzer)
        {
            try
            {
                
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

        static void Header()
        {
            Console.WriteLine("AquaScript Compiler v0.1.0");
            Console.WriteLine("Copyright (c) 2018");
        }
    }
}
