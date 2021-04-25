using System;
using System.Collections.Generic;
using System.Net;

namespace anagram.name
{
    public class AnagramName
    {
        private readonly HashSet<String> firstNames;
        private readonly HashSet<String> lastNames;
        public bool allowFirstLetterMatch { get; set; }
        public bool lastOnly { get; set; }
        public bool allowFirstOrLastName { get; set; }

        public AnagramName(HashSet<string> firstNames, HashSet<string> lastNames)
        {
            this.firstNames = firstNames;
            this.lastNames = lastNames;
        }

        public HashSet<String> generateNames(String phrase)
        {
            phrase = phrase.Replace(" ", "");               // removes spaces
            HashSet<String> matches = new HashSet<String>();

            // Recursively generates names
            generateRecursive(matches, phrase, "");

            return matches;
        }

        private void generateRecursive(HashSet<String> matches, String phrase, String text)
        {
            if (phrase.Length > 0)
            {
                for (int i = 0; i < phrase.Length; i++)
                {
                    char c = phrase[i];

                    String subPhrase = phrase.removeAtIndex(i);
                    String subText = text + c;
                    generateRecursive(matches, subPhrase, subText);
                }
            }
            else
            {
                if (lastOnly)
                {
                    if (lastNames.Contains(text))
                        matches.Add(text);

                    return;
                }

                if (allowFirstOrLastName)
                {
                    if (firstNames.Contains(text))
                        matches.Add(text);
                    else if (lastNames.Contains(text))
                        matches.Add(text);
                }

                // Checks for name matches by moving space throughout 
                for (int j = 1; j < text.Length - 1; j++)
                {
                    (String firstName, String lastName) = text.splitAtIndex(j);
                    
                    // Checks for firstname and lastname matches
                    if (allowFirstLetterMatch)
                    {
                        if ((firstName.Length == 1 || firstNames.Contains(firstName)) &&
                            (lastName.Length == 1 || lastNames.Contains(lastName))
                            )
                                matches.Add(String.Concat(firstName, " ", lastName));
                    }
                    else
                    {
                        if (firstNames.Contains(firstName) && lastNames.Contains(lastName))
                            matches.Add(String.Concat(firstName, " ", lastName));
                    }
                }
            }
        }
    }
}