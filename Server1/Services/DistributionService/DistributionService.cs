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

    public async Task<IDictionary<int, Data>?> GetAll()
    {
        var resultDictionary = await _dataService.GetAll();

        if (Settings.Leader)
        {
            var server2Data = await _httpService.GetAll(Settings.Server2);

            if (resultDictionary != null && server2Data != null)
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

    public async Task<Data> Update(int id, Data data)
    {
        if (Settings.Leader)
        {
            var server2Data = await _httpService.Update(id, data, Settings.Server2);
        }

        return await _dataService.Update(id, data);
    }

    public async Task<IList<Result>> Save(Data data)
    {
        var results = new List<Result>();
        if (Settings.Leader)
        {
            data.Id = IdGenerator.GenerateId();

            var server2Response = _tcpService.TcpSave(data, Settings.Server2TcpSavePort);
            if (server2Response != null)
            {
                server2Response.UpdateServerStatus();
                results.Add(server2Response);
            }
        }

        var result = await _dataService.Save(data);
        result.UpdateServerStatus();

        results.Add(result);

        return results;
    }

    public async Task<IList<Result>> Delete(int id)
    {
        var results = new List<Result>();
        var result = await _dataService.Delete(id);
        result.UpdateServerStatus();

        if (Settings.Leader)
        {
            var server2Result = await _httpService.Delete(id, Settings.Server2);

            if (server2Result != null)
            {
                server2Result.UpdateServerStatus();
                results.Add(server2Result);
            }
        }

        return results;
    }
}