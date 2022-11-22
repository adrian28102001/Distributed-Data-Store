using PartitionLeader.Models;
using PartitionLeader.Repositories.GenericRepository;

namespace PartitionLeader.Repositories.DataStorage;

public class DataStorageRepository : IDataStorageRepository
{
    private readonly IGenericRepository<Data> _genericRepository;
    private readonly IDictionary<int, Data> _storage;
    
    public DataStorageRepository()
    {
        _storage = new Dictionary<int, Data>();
        _genericRepository = new GenericRepository<Data>(_storage);
    }
    
    public IDictionary<int, Data> GetAll()
    {
        return _storage;
    }

    public KeyValuePair<int, Data> GetById(int id)
    {
        return _genericRepository.GetById(id);
    }

    public async Task<Result> Save(int id, Data dataModel)
    {
        return await _genericRepository.Save(id, dataModel);
    }

    public async Task<Data> Update(int id, Data dataModel)
    {
        return await _genericRepository.Update(id, dataModel);
    }

    public async Task<Result> Delete(int id)
    {
        return await _genericRepository.Delete(id);
    }

    public Task<bool> DoesKeyExist(int id)
    {
        return Task.FromResult(GetAll().ContainsKey(id));
    }
}