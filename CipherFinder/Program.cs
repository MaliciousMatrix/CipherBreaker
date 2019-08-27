using System;
using System.Collections.Generic;
using System.Linq;

namespace CipherFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Cipher[] ciphers = new Cipher[]
            {
                new Cipher(@"baAEcEjfcuEgtECrdlEhgnlBakE", "Tall Top"),
                new Cipher(@"CrdlEBkcdpEJEaex", "Tall Bottom"),
                new Cipher(@"raGFcFwaGGFcquFraFuvayqC", "One"),
                new Cipher(@"zqvKFKrvFraFdGFHaZ", "Two"),
                new Cipher(@"svCFYdDCYaFuvFKaFzqvL", "Three"),
                new Cipher(@"CraFGDcXI", "Four"),
                new Cipher(@"GvFKaYevwaFrawMKdDr", "Five"),
                new Cipher(@"vWaqFcXwG", "Six"),
            };

            Symbol[] Symbols = InitializeAllSymbolList();
            char[] cipherCharacters = GetAllCharactersFromCiphers(ciphers);
            CountSymbols(Symbols, cipherCharacters);

            PrintSymbolPercentages(Symbols, cipherCharacters);

            Dictionary<char, char> knowns = new Dictionary<char, char>()
            {
                {'E', ' '},
                {'F', ' '},
                {'a', 'e'},
                {'r', 'h'},
                {'c', 'a'},
                {'d', 'i'},
                {'G', 's'},
                {'w', 'm'},
                {'q', 'n'},
                {'u', 'd'},
                {'C', 't'},
                {'v', 'o'},
                {'y', 's'},
                {'H', 'y'},
                {'Z', 't'},
                {'z', 'k'},
                {'K', 'w'},
                {'Y', 'l'},
                {'e', 'c'},
                {'D', 't'},
                {'W', 'p'},
                {'X', 'r'},
                {'L', 'w'},
                {'I', 's'},
                {'l', 's'},
                {'M', ' '},
                {'s', 'b'},
                {'b', 'g'},
                {'j', 'l'},
                {'f', 'o'},
                {'g', 'o'},
                {'B', 't'},
                {'k', 'r'},
                {'p', 'n'},
                {'J', 'r'},
                {'x', 'k'},
                {'A', 't'},
                {'t', 'f'},
                {'h', 'm'},
                {'n', 'n'},

            };
            Cipher.KnownValues = knowns;

            PrintCiphersWithKnownValues(ciphers);

            Console.ReadKey();
        }

        private static void PrintCiphersWithKnownValues(Cipher[] ciphers)
        {
            foreach (var c in ciphers)
            {
                Console.WriteLine(c.CipherText);

                c.PrintWithKnowns();
                c.PrintWithKnownsSymbols();

                Console.WriteLine();
            }
        }

        private static void PrintSymbolPercentages(Symbol[] Symbols, char[] cipherCharacters)
        {
            var orderdSymbols = Symbols.OrderBy(x => x.Percentage).Where(x => x.Percentage > 0).Reverse();

            foreach (var symbol in orderdSymbols)
            {
                Console.WriteLine(symbol.GetUsageData());
            }

            Console.WriteLine("-------------------------------------------------------------------");

            foreach (var symbol in orderdSymbols)
            {
                Console.WriteLine(symbol.GetCommonFollowingCharacters(3, cipherCharacters));
            }

            Console.WriteLine("-------------------------------------------------------------------");

        }

        private static void CountSymbols(Symbol[] Symbols, char[] cipherCharacters)
        {
            foreach (var c in cipherCharacters)
            {
                foreach (var s in Symbols)
                {
                    if (s.IsCharacter(c))
                    {
                        s.Count++;
                    }
                }
                Symbol.TotalSymbolCount++;
            }
        }

        private static char[] GetAllCharactersFromCiphers(Cipher[] ciphers)
        {
            List<char> cipherCharacters = new List<char>();
            foreach (var c in ciphers)
            {
                cipherCharacters.AddRange(c.CipherText.ToArray());
            }

            return cipherCharacters.ToArray();
        }

        private static Symbol[] InitializeAllSymbolList()
        {
            char[] availableCharacters = new char[]
                        {
                'a','b','c','d','e','f','g','h','i','j','k','l','m',
                'n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A','B','C','D','E','F','G','H','I','J','K','L','M',
                'W','X','Y','Z',
                        };

            Symbol[] Symbols = new Symbol[availableCharacters.Length];

            for(int i = 0; i < availableCharacters.Length; i++)
            {
                Symbols[i] = new Symbol(availableCharacters[i]);
            }

            return Symbols;
        }
    }
}