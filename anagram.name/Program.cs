using System;
using System.Collections.Generic;
using System.IO;

namespace anagram.name
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<String> firstNames = FileUtil.parseNamesString(@"C:\dev\anagram.name\first_names_us_baby_since_1900.txt");
            HashSet<String> lastNames = FileUtil.parseNamesString(@"C:\dev\anagram.name\last_names_us_2010_census.txt");
            NamePermutations builder = new NamePermutations();
            builder.setNames(firstNames, lastNames);

            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter phrase to covert to a name: ");
                String command = Console.ReadLine() ?? "";

                command = command.Trim();
                if (String.IsNullOrEmpty(command))
                    break;
                
                Console.WriteLine("Anagram Names for phrase: " + command);

                // builder.setSettingFromPhrase(command);
                // builder.allowFirstOrLastName = false;
                // builder.lastOnly = false;
                // builder.maxParts = 1;
                // builder.minNameLength = 0;
                
                DateTime marker = DateTime.Now;
                HashSet<String> matches = builder.generateNames(command);
                TimeSpan duration = DateTime.Now - marker;
                
                Console.WriteLine($"Took {duration}");
                if (matches.Count == 0)
                {
                    Console.WriteLine("NO MATCHES");
                }
                else
                {
                    using (Stream file = new FileStream(@"c:\dev\ee.txt", FileMode.Create, FileAccess.Write))
                    {
                        using (TextWriter output = new StreamWriter(file))
                        {
                            output.WriteLine("Anagrams for phrase: " + command);
                            foreach (String match in matches)
                            {
                                output.WriteLine(match);
                                Console.WriteLine(match);                                
                            }
                        }
                    }
                }
            } while (true);
            
            Console.WriteLine("DONE.");
        }
    }
}
