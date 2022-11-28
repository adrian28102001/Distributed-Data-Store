using Server2.Models;

namespace Server2.Services.DataService;

public interface IDataStorageService
{
    public Task<KeyValuePair<int, Data>?> GetById(int id);
    public Task<IDictionary<int, Data>?> GetAll();
    public Task<Result> Save(Data entity);
    public Task<Data> Update(int id, Data entity);
    public Task<Result> Delete(int id);
    public Task<bool> DoesKeyExist(int id);
}