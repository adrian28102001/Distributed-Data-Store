using Server2.Models;
using Server2.Repositories.GenericRepository;

namespace Server2.Repositories.DataStorage;

public interface IDataStorageRepository : IGenericRepository<Data>
{
    public Task<bool> DoesKeyExist(int id);
}