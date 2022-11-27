using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Helpers;
using PartitionLeader.Helpers.Mappers;
using PartitionLeader.Models;
using PartitionLeader.Services;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.DistributionService;
using PartitionLeader.Services.HttpService;
using PartitionLeader.Services.StorageService;
using PartitionLeader.Services.TcpService;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDistributionService _distributionService;

    public ServerController(IDistributionService distributionService)
    {
        _distributionService = distributionService;
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>?> GetById([FromRoute] int id)
    {
        return await _distributionService.GetById(id);
    }

    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _distributionService.GetAll();
    }

    [HttpGet("/summary")]
    public async Task<Result?> GetSummary()
    {
        return await Task.FromResult(StorageHelper.GetStatus());
    }

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        var data = dataModel.MapData();

        var updateResult = await _distributionService.Update(id, data);

        return updateResult;
    }

    [HttpPost]
    public async Task<IList<Result>> Save([FromForm] DataModel dataModel)
    {
        var data = dataModel.MapData();

        IList<Result> resultSummaries = new List<Result>();
        try
        {
            resultSummaries = await _distributionService.Save(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return resultSummaries;
    }

    [HttpDelete("/delete/{id}")]
    public async Task<IList<Result>> Delete([FromRoute] int id)
    {
        return await _distributionService.Delete(id);
    }
}