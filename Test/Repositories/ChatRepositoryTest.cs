using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Core.Entities;
using Infrastructure.Data.DynamoDb;
using Infrastructure.Repositories;
using Newtonsoft.Json;

namespace Test.Repositories;

public class ChatRepositoryTest
{
    [Fact]
    public async Task ProcessFile()
    {
        // Arrange
        var chatRepository = new ChatRepository(new DynamoDBContext(new AmazonDynamoDBClient()));
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
        var actual = await chatRepository.ProcessFile(file);
        expected.Timestamp = actual.Timestamp;

        // Assets
        Assert.Equal(
            JsonConvert.SerializeObject(expected),
            JsonConvert.SerializeObject(actual)
        );
    }
}