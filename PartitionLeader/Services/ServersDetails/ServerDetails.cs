using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Setting;
using RestSharp;

namespace PartitionLeader.Services.ServersDetails;

public class ServerDetails : IServerDetails
{
    private readonly IDictionary<int, int> _storageDetails;

    public ServerDetails()
    {
        _storageDetails = new Dictionary<int, int>();
    }

    [HttpGet]
    public IDictionary<int, int> GetServersCapacity()
    {
        var client1 = new RestClient(Settings.Server1);
        var client2 = new RestClient(Settings.Server2);

        var response1 = client1.Execute<KeyValuePair<int, int>>(new RestRequest());
        var response2 = client2.Execute<KeyValuePair<int, int>>(new RestRequest());

        _storageDetails[response1.Data.Key] = response1.Data.Value;
        _storageDetails[response2.Data.Key] = response2.Data.Value;

        return _storageDetails;
    }
}