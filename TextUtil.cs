using System;

namespace anagram.name
{
    public static class TextUtil
    {
        public static String removeAtIndex(this String input, int index)
        {
            if (string.IsNullOrEmpty(input) || index < 0)
                return null;

            if (input.Length <= index)
                return input;

            if (input.Length - 1 == index)
                return input.Substring(0, index);
            
            return input.Substring(0, index) + input.Substring(index + 1);
        }
        
        public static (string, string) splitAtIndex(this String input, int index)
        {
            if (string.IsNullOrEmpty(input) || index < 0)
                return (null, null);

            if (input.Length < index)
                return (input, "");

            return (input.Substring(0, index), input.Substring(index));
        }
    }
}