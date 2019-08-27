using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CipherFinder
{
    class Cipher
    {
        public string Name { get; set; }

        private readonly string _cipherText = "";
        public string CipherText
        {
            get
            {
                return _cipherText;
            }
        }

        public List<char> CipherTextAsList
        {
            get
            {
                return CipherText.ToCharArray().ToList();
            }
        }

        public Cipher(string encodedText, string name)
        {
            _cipherText = RemoveNotLetterCharacters(encodedText);
            Name = name;
        }

        private static string RemoveNotLetterCharacters(string encodedText)
        {
            encodedText = Regex.Replace(encodedText, @"\r", "");
            encodedText = Regex.Replace(encodedText, @"\n", "");
            encodedText = Regex.Replace(encodedText, @"\t", "");
            return encodedText;
        }

        public static Dictionary<char, char> KnownValues = new Dictionary<char, char>();
        public static Dictionary<char, char> unknownValues = new Dictionary<char, char>();

        private static int nextUnknownSymbolToUse = 0;
        private readonly char[] possibleUnknownSymbols = {'!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '\'',
                                                          '{', '}', '/', '?', '=', '+', '_', '"', ';', ':', '.', '<', '>',
                                                          ',', '`', '~', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                                          '0' };
        public void PrintWithKnownsSymbols()
        {
            foreach(var c in CipherTextAsList)
            {
                if(KnownValues.ContainsKey(c))
                {
                    PrintKnownSymbol(KnownValues[c]);
                }
                else if (unknownValues.ContainsKey(c))
                {
                    PrintUnknownSymbol(unknownValues[c]);
                }
                else
                {
                    unknownValues[c] = possibleUnknownSymbols[nextUnknownSymbolToUse++];
                    PrintUnknownSymbol(unknownValues[c]);
                }
            }
            Console.WriteLine();
            
        }

        private const ConsoleColor defaultConsoleColor = ConsoleColor.White;
        private const ConsoleColor knownSymbolConsoleColor = ConsoleColor.Blue;
        private const ConsoleColor unknownSymbolConsoleColor = ConsoleColor.DarkRed;

        private void PrintUnknownSymbol(char symbol)
        {
            Console.ForegroundColor = unknownSymbolConsoleColor;
            Console.Write(symbol);
            Console.ForegroundColor = defaultConsoleColor;
        }

        private void PrintKnownSymbol(char symbol)
        {
            Console.ForegroundColor = knownSymbolConsoleColor;
            Console.Write(symbol);
            Console.ForegroundColor = defaultConsoleColor;
        }

        public void PrintWithKnowns()
        {
            foreach (var c in CipherTextAsList)
            {
                if (KnownValues.ContainsKey(c))
                {
                    Console.ForegroundColor = knownSymbolConsoleColor;
                    Console.Write(KnownValues[c]);
                    Console.ForegroundColor = defaultConsoleColor;
                }
                else
                {
                    Console.ForegroundColor = unknownSymbolConsoleColor;
                    Console.Write(c);
                    Console.ForegroundColor = defaultConsoleColor;
                }
            }
            Console.WriteLine();
        }

        public void PrintWithDashes()
        {

        }
    }
}
 