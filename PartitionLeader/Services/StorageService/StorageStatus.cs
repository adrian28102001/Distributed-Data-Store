using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Services.ServersDetails;
using PartitionLeader.Setting;

namespace PartitionLeader.Services.StorageService;

public class StorageStatus : IStorageStatus
{
    private readonly IServerDetails _serverDetails;
    
    private Result _partitionLeaderStatus = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.ThisPort,
        ServerName = Settings.ServerName
    };

    private Result _server1Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };

    private Result _server2Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };

    public StorageStatus(IServerDetails serverDetails)
    {
        _serverDetails = serverDetails;
    }

    public string GetBestServerUrl()
    {
        var serverDetails = _serverDetails.GetServersCapacity();
        var optimalServer = serverDetails.MinBy(pair => pair.Value);

        var url = Settings.GetUrlByServerId(optimalServer.Key);
        ConsoleHelper.Print($"Best server is {url}", ConsoleColor.Yellow);
        
        return url;
    }
    
    public void UpdateStatus(Result result)
    {
        switch (result.ServerName)
        {
            case ServerName.PartitionLeader:
                _partitionLeaderStatus = result;
                break;
            case ServerName.Server1:
                _server1Status = result;
                break;
            case ServerName.Server2:
                _server2Status = result;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}