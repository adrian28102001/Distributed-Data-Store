using Microsoft.AspNetCore.Mvc;
using Server1.Models;

namespace Server1.Services.HttpService;

public interface IHttpService
{
    public Task<IDictionary<int, Data>?> GetById(int id, string url);
    public Task<IDictionary<int, Data>?> GetAll(string url);
    public Task<Data?> Update(int id, [FromForm] Data data, string url);
    public Task<Result?> Save([FromForm] Data data, string url);
    public Task<Result> Delete([FromRoute] int id, string url);
}