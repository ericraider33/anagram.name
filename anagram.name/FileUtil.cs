using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace anagram.name
{
    public static class FileUtil
    {
        public static HashSet<AString> parseNames(String fileName)
        {
            HashSet<AString> results = new HashSet<AString>();
            using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.ToLower().Trim();
                    if (line.StartsWith("#") || line == "")
                        continue;

                    results.Add(new AString(line));
                }
            }

            return results;
        }

        public static HashSet<String> parseNamesString(String fileName)
        {
            HashSet<String> results = new HashSet<String>();
            using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.ToLower().Trim();
                    if (line.StartsWith("#") || line == "")
                        continue;

                    results.Add(line);
                }
            }

            return results;
        }
    }
}