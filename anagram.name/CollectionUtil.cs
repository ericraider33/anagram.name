using System.Collections.Generic;

namespace anagram.name
{
    public static class CollectionUtil
    {
        public static void addRange<T>(this ISet<T> dest, IEnumerable<T> source)
        {
            foreach (T toAdd in source)
                dest.Add(toAdd);
        }
    }
}