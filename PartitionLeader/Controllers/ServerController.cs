using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Helpers;
using PartitionLeader.Helpers.Mappers;
using PartitionLeader.Models;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.DistributionService;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("")]
public class Controller : ControllerBase
{
    private readonly IDistributionService _distributionService;
    private readonly IDataStorageService _dataStorageService;


    public Controller(IDistributionService distributionService, IDataStorageService dataStorageService)
    {
        _distributionService = distributionService;
        _dataStorageService = dataStorageService;
    }

    #region CRUD for partition

    [HttpGet("/check")]
    public Task<bool> CheckStatus()
    {
        return Task.FromResult(true);
    }

    [HttpGet("/summary")]
    public async Task<IList<Result>?> GetSummary()
    {
        return await Task.FromResult(StorageHelper.GetStatusFromServers());
    }

    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _dataStorageService.GetAll();
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>?> GetById([FromRoute] int id)
    {
        return await _dataStorageService.GetById(id);
    }

    [HttpPost]
    public async Task<Result> Save([FromBody] Data data)
    {
        var resultSummaries = new Result();
        try
        {
            resultSummaries = await _dataStorageService.Save(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return resultSummaries;
    }


    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        var data = dataModel.MapData();

        var updateResult = await _distributionService.Update(id, data);

        return updateResult;
    }

    #endregion

    #region CRUD for servers

    [HttpGet("servers/all")]
    public async Task<IDictionary<int, Data>?> GetFromAllServers()
    {
        return await _distributionService.GetAll();
    }

    [HttpGet("/servers/get/{id}")]
    public async Task<KeyValuePair<int, Data>?> GetByIdFromServer([FromRoute] int id)
    {
        return await _distributionService.GetById(id);
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

    #endregion
}