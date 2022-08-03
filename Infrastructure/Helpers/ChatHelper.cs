using System.Globalization;
using Core.Entities;
using Infrastructure.Data.DynamoDb;

namespace Infrastructure.Helpers;

public static class ChatHelper
{
    public static IChat FromText(string text)
    {
        var rows = text.Replace("\r", "").Split("\n");

        int GetValueFromRow(string key) =>
            int.Parse(rows.First(x => x.Contains(key)).Split("=")[1]);

        string GetHashFromRow() =>
            rows.First(x => x.Contains("hash")).Split("=")[1];

        var chat = new ChatDynamoDb
        {
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
            ReasonChange = GetValueFromRow("motivoCambio"),
            ReasonClaim = GetValueFromRow("motivoReclamo"),
            ReasonCongratulations = GetValueFromRow("motivoFelicitaciones"),
            ReasonDoubt = GetValueFromRow("motivoDuda"),
            ReasonPurchase = GetValueFromRow("motivoCompra"),
            WarrantyReason = GetValueFromRow("motivoGarantia"),
            TotalContactClients = GetValueFromRow("totalContactoClientes"),
            Hash = GetHashFromRow()
        };
        return chat;
    }

    public static bool IsValidData(IChat chat)
    {
        var hash = HashHelper.MD5Hash(chat.ToString()!);
        return chat.Hash.Equals(hash);
    }
}