using Infrastructure.Helpers;

namespace Test;

public class HashHelperTest
{
    [Theory]
    [InlineData("250~25~10~100~100~7~8", "2f941516446dce09bc2841da60bf811f")]
    [InlineData("32~32~46~64~7~2~34", "867751f17c4fc2a24d41832bd80ee1d2")]
    public void MD5Hash(string input, string expected)
    {
        // Arrange
        // Act
        var actual = HashHelper.MD5Hash(input);
        // Assert
        Assert.Equal(expected, actual);
    }
}