using PartitionLeader.Models;

namespace PartitionLeader.Settings;

public static class Settings
{
    public const ServerName ServerName = Models.ServerName.PartitionLeader;

    // public static readonly string ServerIP = "localhost";
    private const string ServerIp = "host.docker.internal";

    public const int Port = 5112;

    // public static readonly string BaseUrl = $"https://localhost:"; //local
    public const string BaseUrl = $"https://{ServerIp}:";
    public static readonly string ThisServerUrl = $"https://{ServerIp}:{Port}";
    public static readonly string PartitionLeader = $"https://{ServerIp}:{7112}";
    public static readonly string Server1 = $"https://{ServerIp}:{7173}";
    public static readonly string Server2 = $"https://{ServerIp}:{7156}";
}
