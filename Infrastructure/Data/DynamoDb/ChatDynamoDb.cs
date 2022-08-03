using Amazon.DynamoDBv2.DataModel;
using Core.Entities;

namespace Infrastructure.Data.DynamoDb;

[DynamoDBTable("fdr-table-22122020")]
public class ChatDynamoDb : IChat
{
    [DynamoDBHashKey("timestamp")]
    public string Timestamp { get; set; }

    [DynamoDBProperty("totalContactoClientes")]
    public int TotalContactClients { get; set; }

    [DynamoDBProperty("motivoReclamo")]
    public int ReasonClaim { get; set; }

    [DynamoDBProperty("motivoGarantia")]
    public int WarrantyReason { get; set; }

    [DynamoDBProperty("motivoDuda")]
    public int ReasonDoubt { get; set; }

    [DynamoDBProperty("motivoCompra")]
    public int ReasonPurchase { get; set; }

    [DynamoDBProperty("motivoFelicitaciones")]
    public int ReasonCongratulations { get; set; }

    [DynamoDBProperty("motivoCambio")]
    public int ReasonChange { get; set; }

    [DynamoDBProperty("hash")]
    public string Hash { get; set; }

    public override string ToString()
    {
        return
            $"{TotalContactClients}~{ReasonClaim}~{WarrantyReason}~{ReasonDoubt}~{ReasonPurchase}~{ReasonCongratulations}~{ReasonChange}";
    }
}