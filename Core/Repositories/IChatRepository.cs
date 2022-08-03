using Core.Entities;

namespace Core.Repositories;

public interface IChatRepository
{
    Task<IChat> ProcessFile(string uri);
}