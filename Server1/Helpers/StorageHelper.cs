using Server1.Models;
using Server1.Setting;

namespace Server1.Helpers;

public static class StorageHelper
{
     public static Result PartitionLeaderStatus = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.ThisPort,
        ServerName = Settings.ServerName
    };

    public static Result Server1Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.Server1Port,
        ServerName = ServerName.Server1
    };

    public static Result Server2Status = new()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.Server2Port,
        ServerName = ServerName.Server2
    };

    public static void SetServerStatus(this Result result, bool status)
    {
        result.IsAlive = status;
    }
    

    public static string GetOptimalServerUrl()
    {
        var optimalServer = Server1Status;

        if (optimalServer.StorageCount > Server2Status.StorageCount)
        {
            optimalServer = Server2Status;
        }

        return $"{Settings.BaseUrl}{optimalServer.Port}";
    }

    public static List<ServerName> GetOptimalServers()
    {
        var servers = new List<ServerName>();

        var optimalServer1 = PartitionLeaderStatus;
        var optimalServer2 = Server1Status;

        if (Server2Status.StorageCount < optimalServer2.StorageCount)
        {
            optimalServer2 = Server2Status;
        }
        else if (Server2Status.StorageCount < optimalServer1.StorageCount)
        {
            optimalServer1 = Server2Status;
        }

        servers.Add(optimalServer1.ServerName);
        servers.Add(optimalServer2.ServerName);

        return servers;
    }

    public static void UpdateServerStatus(this Result? summary)
    {
        if (summary != null)
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
            }
        }
    }

    public static Result? GetStatus()
    {
        return PartitionLeaderStatus;
    }

    public static IList<Result> GetStatusFromServers()
    {
        var statuses = new List<Result>
        {
            Server1Status,
            Server2Status,
            PartitionLeaderStatus
        };
        return statuses;
    }
}