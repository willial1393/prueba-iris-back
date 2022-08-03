using System.Globalization;
using Amazon.DynamoDBv2.DataModel;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Data.DynamoDb;
using Infrastructure.Helpers;

namespace Infrastructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly IDynamoDBContext _dbContext;

    public ChatRepository(IDynamoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IChat> ProcessFile(string uri)
    {
        var chat = new ChatDynamoDb
        {
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
            ReasonChange = 1,
            ReasonClaim = 234,
            ReasonCongratulations = 32,
            ReasonDoubt = 35,
            ReasonPurchase = 5467,
            WarrantyReason = 4345,
            TotalContactClients = 32
        };
        chat.Hash = HashHelper.MD5Hash(chat.ToString());

        var res = await _dbContext.LoadAsync<ChatDynamoDb>(DateTime.Now.Millisecond.ToString());
        if (res != null)
        {
            throw new InvalidOperationException($"Student with Id {res.Timestamp} Already Exists");
        }

        await _dbContext.SaveAsync(chat);
        return chat;
    }
}