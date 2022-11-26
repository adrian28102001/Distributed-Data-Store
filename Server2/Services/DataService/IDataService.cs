using Server1.Models;

namespace Server1.Services.DataService;

public interface IDataService
{
    public Task<IDictionary<int, Data>> GetAll();
    public Task<KeyValuePair<int, Data>> GetById(int id);
    public Task<Result> Save(Data data);
    public Task<Data> Update(int id, Data data);
    public Task<Result> Delete(int id);
    public Task<bool> DoesKeyExist(int id);
}