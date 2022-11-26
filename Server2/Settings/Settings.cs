using Server1.Models;

namespace Server1.Settings;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.PartitionLeader;

    public static readonly bool Leader = true;

    // public static readonly string ServerIP = "localhost";
    private const string ServerIp = "host.docker.internal";

    public const int Port = 5112;

    // public static readonly string BaseUrl = $"https://localhost:"; //local
    public const string BaseUrl = $"https://{ServerIp}:";
    public static readonly string ThisServerUrl = $"https://{ServerIp}:{Port}"; //docker
    public static readonly string PartitionLeader = $"https://{ServerIp}:{7112}"; //local
    public static readonly string Server1 = $"https://{ServerIp}:{7173}"; //local
    public static readonly string Server2 = $"https://{ServerIp}:{7156}"; //local
    
    // public static readonly string PartitionLeader = $"https://localhost:{7112}"; //local
    // public static readonly string Server1 = $"https://localhost:{7173}"; //local
    // public static readonly string Server2 = $"https://localhost:{7156}"; //local
    public const int Id = 2;
}