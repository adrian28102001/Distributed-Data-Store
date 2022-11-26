using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Helpers.Mappers;
using PartitionLeader.Models;
using PartitionLeader.Services;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.HttpService;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly IHttpService _httpService;
    private readonly IStorageStatus _storageStatus;

    public ServerController(IDataService dataService, IHttpService httpService, IStorageStatus storageStatus)
    {
        _dataService = dataService;
        _httpService = httpService;
        _storageStatus = storageStatus;
    }

    [HttpGet("/all")]
    public async Task<ICollection<int>> GetAll()
    {
        return await Task.FromResult(_dataService.GetAll().Keys);
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>> GetById([FromRoute] int id)
    {
        return await Task.FromResult(_dataService.GetById(id));
    }

    [HttpPost]
    public async Task<Result> Save([FromForm] DataModel dataModel)
    {
        var data = dataModel.MapData();
        var result = await _dataService.Save(data.Id, data);

        var url = _storageStatus.GetBestServerUrl();
        var server1Result = await _httpService.Save(data, url);

        return result;
    }

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        var data = dataModel.MapData();
        return await _dataService.Update(id, data);
    }

    [HttpDelete("/delete/{id}")]
    public async Task<Result> Delete([FromRoute] int id)
    {
        return await _dataService.Delete(id);
    }
}