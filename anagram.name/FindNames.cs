using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace anagram.name
{
    public class FindNames
    {
        public static void program()
        {
            HashSet<String> firstNames = FileUtil.parseNamesString(@"C:\dev\anagram.name\first_names_us_baby_since_1900.txt");
            HashSet<String> lastNames = FileUtil.parseNamesString(@"C:\dev\anagram.name\last_names_us_2010_census.txt");
            HashSet<String> dictionary = FileUtil.parseNamesString(@"C:\dev\anagram.name\words_alpha.txt");
            
            String path = @"c:\dev\story.txt";
            HashSet<String> names = findNames(path, firstNames, lastNames, dictionary);
            foreach (String name in names)
                Console.WriteLine(name);
        }
        
        public static HashSet<String> findNames(String path, HashSet<String> firstNames, HashSet<String> lastNames, HashSet<String> dictionary)
        {
            HashSet<String> result = new HashSet<string>();
            StringBuilder name = new StringBuilder();
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    String[] parts = line.Split(new[] {' ', ',', '"', '.', '!', '\'', '?', '-', '(', ')'}, StringSplitOptions.RemoveEmptyEntries);

                    name.Length = 0;
                    foreach (String original in parts)
                    {
                        String toCheck = original.ToLower();
                        if (toCheck.Length <= 4)
                            continue;

                        if (dictionary.Contains(toCheck) || !Char.IsUpper(original[0]))
                            continue;

                        // if (firstNames.Contains(toCheck) || lastNames.Contains(toCheck))
                        // {
                            if (name.Length > 0)
                                name.Append(' ');
                            name.Append(toCheck);
                        // }
                    }

                    if (name.Length > 0)
                        result.Add(name.ToString());
                }
            }

            return result;
        }
        
    }
}