using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Helpers.Mappers;
using PartitionLeader.Models;
using PartitionLeader.Services.DataService;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDataService _dataService;

    public ServerController(IDataService dataService)
    {
        _dataService = dataService;
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