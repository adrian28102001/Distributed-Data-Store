using PartitionLeader.Helpers;
using PartitionLeader.Repositories.DataStorage;

namespace PartitionLeader.Services.Data;

public class DataService : IDataService
{
    private readonly IDataStorage _dataStorage;

    public DataService(IDataStorage dataStorage)
    {
        _dataStorage = dataStorage;
    }

    public KeyValuePair<int, Models.Data> GetById(int id)
    {
        return _dataStorage.GetById(id);
    }

    public IDictionary<int, Models.Data> GetAll()
    {
        return _dataStorage.GetAll();
    }

    public void Save(Models.Data data)
    {
        var id = IdGenerator.GenerateId();
        _dataStorage.Save(id, data);
    }

    public Models.Data Update(int id, Models.Data data)
    {
        return _dataStorage.Update(id, data);
    }

    public void Delete(int id)
    {
        _dataStorage.Delete(id);
    }

    public bool DoesKeyExist(int id)
    {
        return _dataStorage.DoesKeyExist(id);
    }
}