﻿using Microsoft.AspNetCore.Mvc;
using Server2.Helpers;
using Server2.Helpers.Mappers;
using Server2.Models;
using Server2.Services.DistributionService;
using Server2.Setting;

namespace Server2.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDistributionService _distributionService;

    public ServerController(IDistributionService distributionService)
    {
        _distributionService = distributionService;
    }

    [HttpGet("/summary")]
    public async Task<IList<Result>?> GetSummary()
    {
        return await Task.FromResult(StorageHelper.GetStatusFromServers());
    }

    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _distributionService.GetAll();
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>?> GetById([FromRoute] int id)
    {
        return await _distributionService.GetById(id);
    }

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromBody] Data data)
    {
        return await _distributionService.Update(id, data);
    }
    
    [HttpPost]
    public async Task<IList<Result>> Save([FromBody] Data data)
    {
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


    [HttpPut("/form/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        if (!Settings.Leader)
        {
            return null;
        }
        
        var data = dataModel.MapData();

        var updateResult = await _distributionService.Update(id, data);

        return updateResult;
    }

    [HttpPost("/form/save")]
    public async Task<IList<Result>> Save([FromForm] DataModel dataModel)
    {
        if (!Settings.Leader)
        {
            return null;
        }
        
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

}