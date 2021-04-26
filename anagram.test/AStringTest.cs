using System;
using anagram.name;
using Xunit;

namespace anagram.test
{
    public class AStringTest
    {
        [Theory]
        [InlineData("mary", -1896674357)]
        [InlineData("joe", 82695804)]
        [InlineData("bobby", -383012060)]
        public void test(String source, int expected)
        {
            AString builder = new AString(source);
            
            Assert.Equal(expected, builder.GetHashCode());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData(" eric", "eric")]
        [InlineData("  eric", "eric")]
        [InlineData("   eric", "eric")]
        [InlineData("eric", "eric")]
        [InlineData("eric ", "eric ")]
        [InlineData("eric  ", "eric  ")]
        [InlineData("eric   ", "eric   ")]
        public void trimLeft(String source, String expected)
        {
            AString x = new AString(source);
            x.trimLeft();
            Assert.Equal(expected, x.ToString());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData(" eric", " eric")]
        [InlineData("  eric", "  eric")]
        [InlineData("   eric", "   eric")]
        [InlineData("eric", "eric")]
        [InlineData("eric ", "eric")]
        [InlineData("eric  ", "eric")]
        [InlineData("eric   ", "eric")]
        public void trimRight(String source, String expected)
        {
            AString x = new AString(source);
            x.trimRight();
            Assert.Equal(expected, x.ToString());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData(" eric", "eric")]
        [InlineData("  eric", "eric")]
        [InlineData("   eric", "eric")]
        [InlineData("eric", "eric")]
        [InlineData("eric ", "eric")]
        [InlineData("eric  ", "eric")]
        [InlineData("eric   ", "eric")]
        public void trim(String source, String expected)
        {
            AString x = new AString(source);
            x.trim();
            Assert.Equal(expected, x.ToString());
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData("a ", "a ")]
        [InlineData(" a ", " a ")]
        [InlineData(" a b", " a b")]
        [InlineData(" a  b", " a b")]
        [InlineData(" a   b", " a b")]
        [InlineData("  a  ", " a ")]
        public void removeDuplicates(String source, String expected)
        {
            AString x = new AString(source);
            x.removeDuplicates(' ');
            Assert.Equal(expected, x.ToString());
        }
        
        [Theory]
        [InlineData(0, "eric")]
        [InlineData(1, "ric")]
        [InlineData(2, "ic")]
        [InlineData(3, "c")]
        [InlineData(4, "")]
        public void setStringIndex(int index, String expected)
        {
            AString x = new AString("eric");
            AString y = new AString("    ");
            y.setString(x, index);
            Assert.Equal(expected, y.ToString());
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("eric", "eric", "")]
        [InlineData("a b", "a", "b")]
        [InlineData(" a b", "", "a b")]
        [InlineData("a b ", "a", "b ")]
        public void splitAt(String source, String expectedX, String expectedY)
        {
            AString aSource = new AString(source);
            AString x = new AString(source);
            AString y = new AString(source);
            aSource.splitAt(x, y, ' ');

            Assert.Equal(expectedX, x.ToString());
            Assert.Equal(expectedY, y.ToString());
        }
        
    }
}