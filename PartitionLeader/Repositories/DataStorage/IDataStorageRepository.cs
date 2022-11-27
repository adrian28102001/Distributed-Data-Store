using PartitionLeader.Models;
using PartitionLeader.Repositories.GenericRepository;

namespace PartitionLeader.Repositories.DataStorage;

public interface IDataStorageRepository : IGenericRepository<Data>
{
    public Task<bool> DoesKeyExist(int id);
}