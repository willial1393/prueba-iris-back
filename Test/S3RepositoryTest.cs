using Amazon.S3;
using Infrastructure.Repositories;

namespace Test;

public class S3HelperTest
{
    [Theory]
    [InlineData("s3://prueba-iris/data.txt",
        "totalContactoClientes=250motivoReclamo=25motivoGarantia=10motivoDuda=100motivoCompra=100motivoFelicitaciones=7motivoCambio=8hash=2f941516446dce09bc2841da60bf811f")]
    public async Task GetFileFromUri(string uri, string expected)
    {
        // Arrange
        var s3 = new S3Repository(new AmazonS3Client());
        // Act
        var actual = await s3.GetFileFromUri(uri);
        // Assert
        Assert.Equal(expected, actual.Trim().Replace("\r", "").Replace("\n", ""));
    }
}