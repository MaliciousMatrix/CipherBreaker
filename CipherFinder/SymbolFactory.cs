using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherFinder
{
    static class SymbolFactory
    {
        public static Symbol CreateSymbol(char character)
        {
            CheckSymbolDoesNotExistForCharacter(character);
            Symbol symbol = new Symbol(character);
            _allSymbols.Add(symbol);
            return symbol;
        }

        private static void CheckSymbolDoesNotExistForCharacter(char character)
        {
            foreach (var symbol in AllSymbols)
            {
                if (symbol.IsCharacter(character))
                {
                    throw new SymbolAlreadyCreatedException(character);
                }
            }
        }

        private static List<Symbol> _allSymbols = new List<Symbol>();
        public static Symbol[] AllSymbols
        {
            get
            {
                return _allSymbols.ToArray();
            }
        }

        public static int SymbolCount
        {
            get
            {
                return AllSymbols.Count();
            }
        }
    }
}
