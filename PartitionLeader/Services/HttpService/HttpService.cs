using PartitionLeader.Models;

namespace PartitionLeader.Services.HttpService;

public class HttpService : IHttpService
{
    public async Task<IDictionary<int, Data>?> GetAll(string url)
    {
        var fullUrl = $"{url}/all";
        var deserialized = await GenericHttp.Get(fullUrl);
        return deserialized;
    }

    public async Task<IDictionary<int, Data>?> GetById(int id, string url)
    {
        var fullUrl = $"{url}/get/{id}";
        var deserialized = await GenericHttp.Get(id, fullUrl);
        return deserialized;
    }

    public async Task<Data?> Update(int id, Data data, string url)
    {
        var fullUrl = $"{url}/update/{id}";
        var deserialized = await GenericHttp.Get(id, fullUrl);
        return null;
    }

    public async Task<Result?> Save(Data data, string url)
    {
        var deserialized = await GenericHttp.Get(url);
        return null;
    }

    public Task<Result> Delete(int id, string url)
    {
        throw new NotImplementedException();
    }
}