﻿using System;
using System.Collections.Generic;
using System.Text;

namespace anagram.name
{
    public class AnagramName
    {
        private readonly HashSet<String> firstNames;
        private readonly HashSet<String> lastNames;
        public bool allowFirstLetterMatch { get; set; }
        public bool lastOnly { get; set; }
        public bool allowFirstOrLastName { get; set; }
        public int maxParts { get; set; }
        private int counter;

        private HashSet<String> matches;

        public AnagramName(HashSet<string> firstNames, HashSet<string> lastNames)
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
            counter = 0;
            
            generateRecursive(new StringBuilder(phrase), 0);

            return matches;
        }

        private void generateRecursive(StringBuilder phrase, int index)
        {
            if (index + 1 == phrase.Length)
            {
                counter++;
                processRecursive(phrase.ToString());
                return;
            }

            char original = phrase[index];
            for (int i = index; i < phrase.Length; i++)
            {
                char toSwap = phrase[i];

                phrase[index] = toSwap;
                phrase[i] = original;

                generateRecursive(phrase, index+1);
                
                phrase[i] = toSwap;
                phrase[index] = original;
            }
        }

        private void processRecursive(String text)
        {
            if (lastOnly)
            {
                String subText = text.Trim();
                if (lastNames.Contains(subText))
                    addMatch(subText);

                return;
            }

            String[] parts = text.Split(' ');
            if (allowFirstOrLastName || parts.Length == 1)
            {
                String subText = text.Trim();
                if (firstNames.Contains(subText) || lastNames.Contains(subText))
                    addMatch(subText);
            }

            switch (parts.Length)
            {
                case 2:
                {
                    String firstName = parts[0];
                    String lastName = parts[1];

                    if ((!allowFirstLetterMatch || firstName.Length > 1) &&
                        !firstNames.Contains(firstName))
                        return;

                    if ((!allowFirstLetterMatch || lastName.Length > 1) &&
                        !lastNames.Contains(lastName))
                        return;
                    
                    addMatch(String.Concat(firstName, " ", lastName));
                    return;
                }

                case 3:
                {
                    String firstName = parts[0];
                    String middleName = parts[1];
                    String lastName = parts[2];

                    if ((!allowFirstLetterMatch || firstName.Length > 1) &&
                        !firstNames.Contains(firstName))
                        return;
                    
                    if ((!allowFirstLetterMatch || middleName.Length > 1) &&
                        !firstNames.Contains(middleName) && 
                        !lastNames.Contains(middleName))
                        return;

                    if ((!allowFirstLetterMatch || lastName.Length > 1) &&
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