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
    
    public Task<IDictionary<int, Data>> GetAll()
    {
        return Task.FromResult(_storage);
    }

    public Task<KeyValuePair<int, Data>> GetById(int id)
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

    public async Task<bool> DoesKeyExist(int id)
    {
        var dictionary = await _genericRepository.GetAll();
        return await Task.FromResult(dictionary.ContainsKey(id));
    }
}