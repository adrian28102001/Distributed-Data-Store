using PartitionLeader.Models;

namespace PartitionLeader.Settings;

public static class Settings
{
    public const ServerName ServerName = Models.ServerName.PartitionLeader;

    private const bool InDocker = false; // set to false when running on localhost

    private const string ServerIp = InDocker ? "host.docker.internal" : "localhost";

    public const int TcpPort = 8081;
    public const int Port = 7112;

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