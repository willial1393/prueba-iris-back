using Core.Entities;
using Infrastructure.Data.DynamoDb;
using Infrastructure.Helpers;
using Newtonsoft.Json;

namespace Test.Helpers;

public class ChatHelperTest
{
    [Fact]
    public void FromText()
    {
        // Arrange
        const string file =
            "totalContactoClientes=250\r\nmotivoReclamo=25\r\nmotivoGarantia=10\r\nmotivoDuda=100\r\nmotivoCompra=100\r\nmotivoFelicitaciones=7\r\nmotivoCambio=8\r\nhash=2f941516446dce09bc2841da60bf811f";
        IChat expected = new ChatDynamoDb
        {
            TotalContactClients = 250,
            ReasonClaim = 25,
            WarrantyReason = 10,
            ReasonDoubt = 100,
            ReasonPurchase = 100,
            ReasonCongratulations = 7,
            ReasonChange = 8,
            Hash = "2f941516446dce09bc2841da60bf811f"
        };

        // Act
        var actual = ChatHelper.FromText(file);
        expected.Timestamp = actual.Timestamp;

        // Assert
        Assert.Equal(
            JsonConvert.SerializeObject(expected),
            JsonConvert.SerializeObject(actual)
        );
    }

    [Theory]
    [InlineData("2f941516446dce09bc2841da60bf811f", true)]
    [InlineData("2f941516446dce09bc2841da60bf811f23", false)]
    public void IsValidData(string hash, bool expected)
    {
        // Arrange
        IChat chat = new ChatDynamoDb
        {
            TotalContactClients = 250,
            ReasonClaim = 25,
            WarrantyReason = 10,
            ReasonDoubt = 100,
            ReasonPurchase = 100,
            ReasonCongratulations = 7,
            ReasonChange = 8,
            Hash = hash
        };

        // Act
        var actual = ChatHelper.IsValidData(chat);

        // Assert
        Assert.Equal(expected, actual);
    }
}