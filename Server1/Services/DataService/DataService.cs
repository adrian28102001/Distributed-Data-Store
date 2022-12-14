using Server1.Helpers;
using Server1.Models;
using Server1.Repositories.DataStorage;

namespace Server1.Services.DataService;

public class DataService : IDataService
{
    private readonly IDataStorageRepository _dataStorageRepository;

    public DataService(IDataStorageRepository dataStorageRepository)
    {
        _dataStorageRepository = dataStorageRepository;
    }

    public Task<KeyValuePair<int, Data>> GetById(int id)
    {
        return _dataStorageRepository.GetById(id);
    }

    public async Task<IDictionary<int, Data>> GetAll()
    {
        return await _dataStorageRepository.GetAll();
    }

    public async Task<Result> Save(Data data)
    {
        var id = IdGenerator.GenerateId();
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