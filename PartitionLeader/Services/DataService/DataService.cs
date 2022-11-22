using PartitionLeader.Models;
using PartitionLeader.Repositories.DataStorage;

namespace PartitionLeader.Services.DataService;

public class DataService : IDataService
{
    private readonly IDataStorageRepository _dataStorageRepository;

    public DataService(IDataStorageRepository dataStorageRepository)
    {
        _dataStorageRepository = dataStorageRepository;
    }

    public KeyValuePair<int, Data> GetById(int id)
    {
        return _dataStorageRepository.GetById(id);
    }

    public IDictionary<int, Data> GetAll()
    {
        return _dataStorageRepository.GetAll();
    }

    public async Task<Result> Save(int id, Data data)
    {
        return await _dataStorageRepository.Save(id, data);
    }

    public Task<Data> Update(int id, Data data)
    {
        return _dataStorageRepository.Update(id, data);
    }

    public async Task<Result> Delete(int id)
    {
        return await _dataStorageRepository.Delete(id);
    }

    public Task<bool> DoesKeyExist(int id)
    {
        return _dataStorageRepository.DoesKeyExist(id);
    }
}