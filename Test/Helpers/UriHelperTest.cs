using Infrastructure.Helpers;

namespace Test.Helpers;

public class UriHelperTest
{
    [Theory]
    [InlineData("s3://fdr-developer-test-22122020/data.txt", true)]
    [InlineData("s3://fdr-developer-test-22122020/", false)]
    [InlineData("s3://fdr-developer-test-22122020", false)]
    [InlineData("s3:/fdr-developer-test-22122020", false)]
    [InlineData("s3:/fdr-developer-test-22122020/asdd", false)]
    public void IsValid(string input, bool expected)
    {
        // Arrange
        // Act
        // Assert
        Assert.Equal(expected, UriHelper.IsValid(input));
    }

    [Fact]
    public void GetBucket()
    {
        // Arrange
        const string input = "s3://fdr-developer-test-22122020/data.txt";
        const string expected = "fdr-developer-test-22122020";

        // Act
        var actual = UriHelper.GetBucket(input);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetKey()
    {
        // Arrange
        const string input = "s3://fdr-developer-test-22122020/data.txt";
        const string expected = "data.txt";

        // Act
        var actual = UriHelper.GetKey(input);

        // Assert
        Assert.Equal(expected, actual);
    }
}