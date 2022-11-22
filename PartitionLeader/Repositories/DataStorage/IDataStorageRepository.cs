using PartitionLeader.Models;
using PartitionLeader.Repositories.GenericRepository;

namespace PartitionLeader.Repositories.DataStorage;

public interface IDataStorageRepository : IGenericRepository<Data>
{
    IDictionary<int, Data> GetAll();
    public Task<bool> DoesKeyExist(int id);
}