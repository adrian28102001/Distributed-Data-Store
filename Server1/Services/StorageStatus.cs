using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Server1.Models;

namespace Server1.Services;

public static class StorageStatus
{
    private static Result _partitionLeaderStatus = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.Settings.ThisPort,
        ServerName = Settings.Settings.ServerName
    };

    private static Result _server1Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };

    private static Result _server2Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };

    
    public static string GetBestServerUrl()
    {
        var optimalServer = _partitionLeaderStatus;
        if (optimalServer.StorageCount > _server1Status.StorageCount)
        {
            optimalServer = _server1Status;
        }

        if (optimalServer.StorageCount > _server2Status.StorageCount)
        {
            optimalServer = _server2Status;
        }

        return $"{Settings.Settings.BaseUrl}{optimalServer.Port}";
    }
    
    public static void UpdateStatus(this Result result)
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