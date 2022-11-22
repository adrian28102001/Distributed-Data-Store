using PartitionLeader.Models;

namespace PartitionLeader.Services.DataService;

public interface IDataService
{
    public IDictionary<int, Data> GetAll();
    public KeyValuePair<int, Data> GetById(int id);
    public Task<Result> Save(int id, Data data);
    public Task<Data> Update(int id, Data data);
    public Task<Result> Delete(int id);
    public Task<bool> DoesKeyExist(int id);
}