using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace anagram.name
{
    public static class FileUtil
    {
        public static HashSet<String> parseNames(String fileName)
        {
            HashSet<String> results = new HashSet<String>(StringComparer.CurrentCultureIgnoreCase);
            using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("#") || line == "")
                        continue;

                    results.Add(line);
                }
            }

            return results;
        }
    }
}