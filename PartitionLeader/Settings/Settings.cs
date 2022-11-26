using PartitionLeader.Models;

namespace PartitionLeader.Settings;

public static class Settings
{
    public const ServerName ServerName = Models.ServerName.PartitionLeader;

    private const string ServerIp = "localhost";

    // private const string ServerIp = "host.docker.internal";
    // public const string BaseUrl = $"https://{ServerIp}:";

    public const int Port = 5112;

    public static readonly string BaseUrl = $"https://localhost:"; //local
    public static readonly string PartitionLeader = $"https://{ServerIp}:{7112}";
    public static readonly string Server1 = $"https://{ServerIp}:{7173}";
    public static readonly string Server2 = $"https://{ServerIp}:{7156}";


    public static string GetUrlByServerId(int id)
    {
        return id switch
        {
            1 => Server1,
            2 => Server2,
            _ => null!
        };
    }
}