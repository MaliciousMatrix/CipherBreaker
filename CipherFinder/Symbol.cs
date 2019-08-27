using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherFinder
{
    class Symbol
    {
        public Symbol(char c)
        {
            Character = c;
        }
        private char _character;

        public char Character
        {
            get { return _character; }
            set { _character = value; }
        }

        public int Count { get; set; } = 0;
        public static int TotalSymbolCount = 0;

        public double Percentage
        {
            get
            {
                return (double)Count / (double)TotalSymbolCount * 100;
            }
        }

        public bool IsCharacter(char character)
        {
            return character == this.Character;
        }

        public string GetUsageData()
        {
            return Character + " " + Math.Round(Percentage, 2).ToString();
        }

        public string GetCommonFollowingCharacters(int topCount, char[] allText)
        {
            List<Tuple<char, int>> followers = GetFollowingCharacters(topCount, allText);
            StringBuilder stringBuilder = new StringBuilder(this.Character + " :");
            foreach(var f in followers)
            {
                stringBuilder.Append(" (" + f.Item1 + ", " + f.Item2 + ")");
            }
            return stringBuilder.ToString();

        }

        private List<Tuple<char, int>> GetFollowingCharacters(int topCount, char[] allText)
        {
            List<Tuple<char, int>> followers = new List<Tuple<char, int>>();
            var CommonCharacterCount = GetFollowingCharacters(allText).OrderBy(x => x.Value).Reverse();
            for (int i = 0; i < topCount && i < CommonCharacterCount.Where(x => x.Value > 0).Count(); i++)
            {
                var kvp = CommonCharacterCount.ElementAt(i);
                followers.Add(new Tuple<char, int>(kvp.Key, kvp.Value));
            }

            return followers;
        }

        private Dictionary<char, int> commonFollowingCharacters = null;
        public Dictionary<char, int> GetFollowingCharacters(char[] allText)
        {
            if (commonFollowingCharacters == null)
            {
                commonFollowingCharacters = new Dictionary<char, int>();
                for(int i = 0; i < allText.Count() - 1; i++)
                {
                    if(allText[i] != this.Character)
                    {
                        // We only care about the characters imediatley following this simbols. If it is not
                        // this one we continue. 
                        continue;
                    }
                    var nextCharacter = allText[i+1];
                    if(commonFollowingCharacters.ContainsKey(nextCharacter))
                    {
                        commonFollowingCharacters[nextCharacter]++;
                    }
                    else
                    {
                        // If we do not have the next character already in the dictionary add it with a starting value
                        // of one. 
                        commonFollowingCharacters.Add(nextCharacter, 1);
                    }
                }
            }
            return commonFollowingCharacters;
        }
    }
}
