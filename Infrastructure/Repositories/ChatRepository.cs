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

    public async Task<IChat> ProcessFile(string file)
    {
        var chat = ChatHelper.FromText(file);
        if (!ChatHelper.IsValidData(chat))
            throw new Exception($"Invalid file data:\n{file}");

        var res = await _dbContext.LoadAsync<ChatDynamoDb>(chat.Timestamp);
        if (res != null)
            throw new Exception($"Chat with timestamp {res.Timestamp} already exists");

        await _dbContext.SaveAsync(chat as ChatDynamoDb);
        return chat;
    }
}