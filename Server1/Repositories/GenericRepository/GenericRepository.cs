using Server1.Models;

namespace Server1.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : IData
{
    private readonly IDictionary<int, T> _storage;

    public GenericRepository(IDictionary<int, T> storage)
    {
        _storage = storage;
    }

    public Task<KeyValuePair<int, T>> GetById(int id)
    {
        return Task.FromResult(_storage.FirstOrDefault(s => s.Key == id));
    }

    public Task<Result> Save(int id, T entity)
    {
        _storage.Add(id, entity);
        
        return Task.FromResult(new Result
        {
            StorageCount = _storage.Count,
            LastProcessedId = id
        });
    }

    public Task<T> Update(int id, T entity)
    {
        return Task.FromResult(_storage[id] = entity);
    }

    public Task<Result> Delete(int id)
    {
        _storage.Remove(id);
        return Task.FromResult(new Result()
        {
            StorageCount = _storage.Count,
            LastProcessedId = id
        });
    }
}