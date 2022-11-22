using PartitionLeader.Models;

namespace PartitionLeader.Services;

public static class StorageStatus
{
    public static Result PartitionLeaderStatus = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.Settings.Port,
        ServerName = Settings.Settings.ServerName
    };

    public static Result Server1Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };

    public static Result Server2Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };

    public static string GetBestServerUrl()
    {
        var optimalServer = PartitionLeaderStatus;
        if (optimalServer.StorageCount > Server1Status.StorageCount)
        {
            optimalServer = Server1Status;
        }

        if (optimalServer.StorageCount > Server2Status.StorageCount)
        {
            optimalServer = Server2Status;
        }

        return $"{Settings.Settings.BaseUrl}{optimalServer.Port}";
    }
    
    public static void UpdateServerStatus(Result summary)
    {
        switch (summary.ServerName)
        {
            case ServerName.PartitionLeader:
                PartitionLeaderStatus = summary;
                break;
            case ServerName.Server1:
                Server1Status = summary;
                break;
            case ServerName.Server2:
                Server2Status = summary;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}