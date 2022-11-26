using Microsoft.AspNetCore.Mvc;
using Server1.Helpers;
using Server1.Models;
using Server1.Services;
using Server1.Services.DataService;
using Server1.Services.HttpService;

namespace Server1.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDataService _dataService;

    public ServerController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public Task<KeyValuePair<int, int>> GetStorageDetails()
    {
        const int serverId = Settings.Settings.Id;
        var serverSize = _dataService.GetAll().Result.Count;
        return Task.FromResult(new KeyValuePair<int, int>(serverId, serverSize));
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>?> GetById([FromRoute] int id)
    {
        return await _dataService.GetById(id);
    }

    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _dataService.GetAll();
    }

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromBody] Data data)
    {
        return await _dataService.Update(id, data);
    }

    [HttpPost]
    public async Task<Result> Save([FromBody] Data data)
    {
        var result = await _dataService.Save(data);
        result.UpdateStatus();
        return result;
    }


    [HttpDelete("/delete/{id}")]
    public async Task<Result> Delete([FromRoute] int id)
    {
        var result = await _dataService.Delete(id);
        result.UpdateStatus();
        return result;
    }
}