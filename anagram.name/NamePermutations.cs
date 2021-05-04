using System;
using System.Collections.Generic;
using System.Text;

namespace anagram.name
{
    public class NamePermutations
    {
        private Dictionary<String, HashSet<String>> firstNameIndex;
        private Dictionary<String, HashSet<String>> lastNameIndex;
        
        public void setNames(HashSet<String> firstNames, HashSet<String> lastNames)
        {
            firstNameIndex = indexNames(firstNames);
            lastNameIndex = indexNames(lastNames);
        }

        private Dictionary<String, HashSet<String>> indexNames(HashSet<String> names)
        {
            Dictionary<String, HashSet<String>> results = new Dictionary<string, HashSet<string>>();
            foreach (String toAdd in names)
            {
                String key = toAdd.sortCharacters();
                HashSet<String> common;
                if (results.ContainsKey(key))
                {
                    common = results[key];
                }
                else
                {
                    common = new HashSet<String>();
                    results.Add(key, common);
                }
                
                common.Add(toAdd);
            }

            return results;
        }

        public HashSet<String> generateNames(String phrase)
        {
            HashSet<String> results = new HashSet<String>();
            phrase = phrase.ToLower().Replace(" ", "");               // removes spaces
            
            String key = phrase.sortCharacters();
            if (firstNameIndex.ContainsKey(key))
                results.addRange(firstNameIndex[key]);
            if (lastNameIndex.ContainsKey(key))
                results.addRange(lastNameIndex[key]);

            StringBuilder firstName = new StringBuilder();
            StringBuilder lastName = new StringBuilder();
            int combinations = 2 << key.Length;
            for (int combo = 0; combo < combinations; combo++)
            {
                firstName.Length = 0;
                lastName.Length = 0;
                for (int charIndex = 0, mask = 1; charIndex < key.Length; charIndex++, mask <<= 1)
                {
                    char c = key[charIndex];
                    if ((combo & mask) > 0)
                        firstName.Append(c);
                    else
                        lastName.Append(c);
                }

                String firstNameKey = firstName.ToString();
                String lastNameKey = lastName.ToString();
                if (!firstNameIndex.ContainsKey(firstNameKey) || !lastNameIndex.ContainsKey(lastNameKey))
                    continue;

                // Combines matches
                permutate(results, firstNameIndex[firstNameKey], lastNameIndex[lastNameKey]);
            }
            
            return results;
        }

        private void permutate(HashSet<String> results,
            HashSet<String> firstMatch,
            HashSet<String> lastMatch
            )
        {
            StringBuilder builder = new StringBuilder();
            foreach (String firstName in firstMatch)
            {
                foreach (String lastName in lastMatch)
                {
                    builder.Length = 0;
                    builder.Append(firstName);
                    builder.Append(' ');
                    builder.Append(lastName);
                    results.Add(builder.ToString());
                }
            }
        }
    }
}