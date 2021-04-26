using System;

namespace anagram.name
{
    public class AString
    {
        private readonly char[] characters;
        private readonly int length;

        public AString(String source)
        {
            characters = source.ToCharArray();
            length = characters.Length;
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
    }
}