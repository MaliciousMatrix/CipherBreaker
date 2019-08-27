using System;

namespace CipherFinder
{
    public class SymbolAlreadyCreatedException : Exception
    {
        public SymbolAlreadyCreatedException(char character) : 
            base("The symbol for the character " + character + " has already been created. Cannot create again.")
        { }
    }
}
