using System;
using System.Collections.Generic;

namespace anagram.name
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<AString> firstNames = FileUtil.parseNames(@"C:\dev\anagram.name\first_names.txt");
            HashSet<AString> lastNames = FileUtil.parseNames(@"C:\dev\anagram.name\last_names.txt");
            AnagramName builder = new AnagramName(firstNames, lastNames);

            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter phrase to covert to a name: ");
                String command = Console.ReadLine() ?? "";

                command = command.Trim();
                if (String.IsNullOrEmpty(command))
                    break;
                
                Console.WriteLine("Anagram Names for phrase: " + command);

                builder.setSettingFromPhrase(command);
                builder.allowFirstOrLastName = false;
                builder.maxParts = 0;
                builder.minNameLength = 4;
                
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
                    // foreach (String match in matches)
                    //     Console.WriteLine(match);
                }
            } while (true);
            
            Console.WriteLine("DONE.");
        }
    }
}
