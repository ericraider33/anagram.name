using System;
using System.Collections.Generic;

namespace anagram.name
{
    public class AnagramName
    {
        private readonly HashSet<AString> firstNames;
        private readonly HashSet<AString> lastNames;
        public bool allowFirstLetterMatch { get; set; }
        public bool lastOnly { get; set; }
        public bool allowFirstOrLastName { get; set; }
        public int maxParts { get; set; }
        public int counter { get; private set; }
        public int minNameLength;

        private HashSet<String> matches;
        private AString firstName;
        private AString middleName;
        private AString lastName;
        private AString text;

        public AnagramName(HashSet<AString> firstNames, HashSet<AString> lastNames)
        {
            this.firstNames = firstNames;
            this.lastNames = lastNames;
            maxParts = 1;
        }

        public void setSettingFromPhrase(String phrase)
        {
            allowFirstLetterMatch = false;
            if (phrase.Length <= 4)
            {
                lastOnly = true;
                allowFirstOrLastName = false;
                maxParts = 0;
            }
            else if (phrase.Length <= 11)
            {
                lastOnly = false;
                allowFirstOrLastName = true;
                maxParts = 1;
            }
            else
            {
                lastOnly = false;
                allowFirstOrLastName = false;
                maxParts = 2;
            }
        }
        
        public HashSet<String> generateNames(String phrase)
        {
            phrase = phrase.Replace(" ", "");               // removes spaces
            for (int i = 0; i < maxParts; i++)
                phrase += " ";
            
            // Recursively generates names
            matches = new HashSet<String>();
            firstName = new AString(phrase);
            middleName = new AString(phrase);
            lastName = new AString(phrase);
            text = new AString(phrase);
            counter = 0;
            
            generateRecursive(new AString(phrase), 0);

            return matches;
        }

        private void generateRecursive(AString phrase, int index)
        {
            if (index + 1 == phrase.length)
            {
                counter++;
                processRecursive(phrase);
                return;
            }

            char original = phrase[index];
            for (int i = index; i < phrase.length; i++)
            {
                char toSwap = phrase[i];

                phrase[index] = toSwap;
                phrase[i] = original;

                generateRecursive(phrase, index+1);
                
                phrase[i] = toSwap;
                phrase[index] = original;
            }
        }

        private void processRecursive(AString phrase)
        {
            text.setString(phrase);
            text.trim();
            
            if (lastOnly)
            {
                if (lastNames.Contains(text))
                    addMatch(text.ToString());

                return;
            }

            text.removeDuplicates(' ');
            int parts = text.countCharacter(' ') + 1;

            if (allowFirstOrLastName)
            {
                if (firstNames.Contains(text) || lastNames.Contains(text))
                    addMatch(text.ToString());
            }
            
            switch (parts)
            {
                case 2:
                {
                    text.splitAt(firstName, lastName, ' ');

                    if (minNameLength > 0 && (firstName.length < minNameLength || lastName.length < minNameLength))
                        return;
                    
                    if ((!allowFirstLetterMatch || firstName.length > 1) &&
                        !firstNames.Contains(firstName))
                        return;

                    if ((!allowFirstLetterMatch || lastName.length > 1) &&
                        !lastNames.Contains(lastName))
                        return;
                    
                    addMatch(String.Concat(firstName, " ", lastName));
                    return;
                }
                
                case 3:
                {
                    text.splitAt(firstName, lastName, ' ');
                    text.setString(lastName);
                    text.splitAt(middleName, lastName, ' ');
                    
                    if (minNameLength > 0 && (firstName.length < minNameLength || lastName.length < minNameLength))
                        return;

                    if ((!allowFirstLetterMatch || firstName.length > 1) &&
                        !firstNames.Contains(firstName))
                        return;
                    
                    if ((!allowFirstLetterMatch || middleName.length > 1) &&
                        !firstNames.Contains(middleName) && 
                        !lastNames.Contains(middleName))
                        return;

                    if ((!allowFirstLetterMatch || lastName.length > 1) &&
                        !lastNames.Contains(lastName))
                        return;
                    
                    addMatch(String.Concat(firstName, " ", middleName, " ", lastName));
                    return;
                }
            }
        }

        private void addMatch(String toAdd)
        {
            if (matches.Contains(toAdd))
                return;

            matches.Add(toAdd);
            Console.WriteLine(toAdd);
        }
    }
}