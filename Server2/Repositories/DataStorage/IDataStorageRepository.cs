using Server1.Models;
using Server1.Repositories.GenericRepository;

namespace Server1.Repositories.DataStorage;

public interface IDataStorageRepository : IGenericRepository<Data>
{
    Task<IDictionary<int, Data>> GetAll();
    public Task<bool> DoesKeyExist(int id);
}