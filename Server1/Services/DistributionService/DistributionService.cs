using Server1.Helpers;
using Server1.Models;
using Server1.Services.DataService;
using Server1.Services.HttpService;
using Server1.Services.TcpService;
using Server1.Setting;

namespace Server1.Services.DistributionService;

public class DistributionService : IDistributionService
{
    private readonly IDataStorageService _dataService;
    private readonly IHttpService _httpService;
    private readonly ITcpService _tcpService;

    public DistributionService(IDataStorageService dataService, IHttpService httpService, ITcpService tcpService)
    {
        _dataService = dataService;
        _httpService = httpService;
        _tcpService = tcpService;
    }

    public async Task<IDictionary<int, Data>?> GetAll()
    {
        var resultDictionary = await _dataService.GetAll();

        var server1Data = await _httpService.GetAll(Settings.Server1);

        if (resultDictionary != null && server1Data != null)
        {
            foreach (var data in server1Data)
            {
                if (!resultDictionary.ContainsKey(data.Key))
                {
                    resultDictionary.Add(data);
                }
            }
        }
        else if (resultDictionary != null)
        {
            var server2Data = await _httpService.GetAll(Settings.Server2);

            if (server2Data != null)
            {
                foreach (var data in server2Data)
                {
                    if (!resultDictionary.ContainsKey(data.Key))
                    {
                        resultDictionary.Add(data);
                    }
                }
            }
        }

        return resultDictionary;
    }

    public async Task<KeyValuePair<int, Data>?> GetById(int id)
    {
        var data = await _dataService.GetById(id);
        if (data?.Value == null)
        {
            data = await _httpService.GetById(id, Settings.Server1);
        }

        if (data?.Value == null)
        {
            data = await _httpService.GetById(id, Settings.Server2);
        }

        return data;
    }

    public async Task<IList<Result>> Save(Data data)
    {
        var results = new List<Result>();

        var optimalServerNames = StorageHelper.GetOptimalServers();

        if (optimalServerNames.Contains(Settings.ServerName))
        {
            var result = await _dataService.Save(data);
            result.UpdateServerStatus();

            results.Add(result);
        }

        if (optimalServerNames.Contains(ServerName.Server1))
        {
            var server1Response = _tcpService.TcpSave(data, Settings.Server1TcpSavePort);
            if (server1Response != null)
            {
                server1Response.UpdateServerStatus();
                results.Add(server1Response);
            }
        }

        if (optimalServerNames.Contains(ServerName.Server2))
        {
            var server2Response = _tcpService.TcpSave(data, Settings.Server2TcpSavePort);
            if (server2Response != null)
            {
                server2Response.UpdateServerStatus();
                results.Add(server2Response);
            }
        }

        return results;
    }

    public async Task<Data> Update(int id, Data data)
    {
        await _httpService.Update(id, data, Settings.Server1);
        await _httpService.Update(id, data, Settings.Server2);

        return await _dataService.Update(id, data);
    }

    public async Task<IList<Result>> Delete(int id)
    {
        var results = new List<Result>();
        var result = await _dataService.Delete(id);
        result.UpdateServerStatus();

        var server1Result = await _httpService.Delete(id, Settings.Server1);
        var server2Result = await _httpService.Delete(id, Settings.Server2);

        if (server1Result != null)
        {
            foreach (var resultSummary in server1Result)
            {
                resultSummary.UpdateServerStatus();
                results.Add(resultSummary);
            }
        }

        if (server2Result != null)
        {
            foreach (var resultSummary in server2Result)
            {
                resultSummary.UpdateServerStatus();
                results.Add(resultSummary);
            }
        }

        return results;
    }
}