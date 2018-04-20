using Xunit;

namespace Csi.Helpers.Azure.Tests
{
    public class ByteUnitConverterTest
    {
        private static readonly ByteUnitConverter buc = new ByteUnitConverter();

        [Theory]
        [InlineData(1, 0L)]
        [InlineData(1, 2L)]
        [InlineData(1, 2L << 10)]
        [InlineData(1, 1L << 30)]
        [InlineData(2, (2L << 30) - 1)]
        [InlineData(2, (2L << 30) + 0)]
        [InlineData(3, (2L << 30) + 1)]
        [InlineData(2 << 20, 2L << 50)]
        public void ToGibibyte(int expected, long input) => Assert.Equal(expected, buc.ToGibibyte(input));

        [Theory]
        [InlineData(0L, 0)]
        [InlineData(1L<<30, 1)]
        public void FromGigibyte(long expected, int input) => Assert.Equal(expected, buc.FromGigibyte(input));
    }
}
