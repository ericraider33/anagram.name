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
        
    }
}