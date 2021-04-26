using System;

namespace anagram.name
{
    public class AString
    {
        private readonly char[] characters;
        public int length { get; private set; }

        public AString(String source)
        {
            characters = source.ToCharArray();
            length = characters.Length;
        }

        public AString() : this("")
        {
        }

        public char this[int index]
        {
            get => characters[index];
            set => characters[index] = value;
        }
        
        protected bool Equals(AString other)
        {
            return Equals(characters, other.characters);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            AString other = (AString) obj;
            if (other.length != this.length) return false;
            
            for (int i = 0; i < length; i++)
                if (other.characters[i] != characters[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ characters[i];
                    if (i == length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ characters[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        public override string ToString()
        {
            return new String(characters, 0, length);
        }

        public void setString(String source)
        {
            length = source.Length;
            for (int i = 0; i < length; i++)
                characters[i] = source[i];
        }

        public void setString(AString source)
        {
            length = source.length;
            for (int i = 0; i < length; i++)
                characters[i] = source[i];
        }

        public void setString(AString source, int index)
        {
            if (index >= source.length)
            {
                length = 0;
                return;
            }
            
            for (int from = index, to = 0; from < source.length; from++, to++)
                characters[to] = source[from];

            length = source.length - index;
        }

        public void trim()
        {
            if (length == 0)
                return;

            trimLeft();
            trimRight();
        }

        public void trimLeft()
        {
            if (length == 0)
                return;

            if (characters[0] != ' ') 
                return;
            
            int start = 0;
            while (characters[start] == ' ')
            {
                start++;
                if (start == length)
                {
                    length = 0;                 // entire string is spaces
                    return;
                }
            }

            for (int from = start, to = 0; from < length; from++, to++)
                characters[to] = characters[from];

            length -= start;
        }

        public void trimRight()
        {
            if (length == 0)
                return;

            while (characters[length - 1] == ' ')
            {
                length--;
                if (length == 0)
                    return;                 // entire string is spaces
            }
        }

        public void removeDuplicates(char c)
        {
            if (length <= 1)
                return;
            
            int i = 1;
            char last = characters[0]; 
            while (i < length)
            {
                char current = characters[i];
                if (last == current && current == c)
                {
                    shiftLeft(i);
                }
                else
                {
                    last = current;
                    i++;
                }
            }
        }

        public void shiftLeft(int index)
        {
            if (index >= length || index <= 0)
                throw new IndexOutOfRangeException("Invalid index for shifting");

            for (int i = index; i < length; i++)
                characters[i - 1] = characters[i];

            length--;
        }

        public int countCharacter(char c)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
                if (characters[i] == c)
                    count++;

            return count;
        }

        public void splitAt(AString destA, AString destB, char c)
        {
            for (int i = 0; i < length; i++)
            {
                char x = characters[i];
                if (x == c)
                {
                    destA.setLength(i);
                    destB.setString(this, i + 1);
                    return;
                }
                else
                {
                    destA[i] = x;
                }
            }
            
            destA.setLength(length);
            destB.setLength(0);
        }

        public void setLength(int newLength)
        {
            length = newLength;
        }
    }
}