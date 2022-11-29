using Server2.Models;

namespace Server2.Repositories.GenericRepository;

public interface IGenericRepository <T> where T : Entity
{
    public Task<IDictionary<int, T>> GetAll();
    public Task<KeyValuePair<int, T>> GetById(int id);
    public Task<Result> Save(int id, T entity);
    public Task<T> Update(int id, T entity);
    public Task<Result> Delete(int id);
}