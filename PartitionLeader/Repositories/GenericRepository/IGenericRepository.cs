using PartitionLeader.Models;

namespace PartitionLeader.Repositories.GenericRepository;

public interface IGenericRepository <T> where T : IData
{
    public KeyValuePair<int, T> GetById(int id);
    public Task<Result> Save(int id, T entity);
    public Task<T> Update(int id, T entity);
    public Task<Result> Delete(int id);
}