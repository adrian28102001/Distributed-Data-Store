using Server1.Models;

namespace Server1.Settings;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.PartitionLeader;

    public static readonly bool Leader = false;
    public const int TcpPort = 8081;

    public const bool InDocker = true; // set to false when running on localhost

    public const string ServerIp = InDocker ? "host.docker.internal" : "localhost";

    public const int LeaderPort = InDocker ? 5112 : 7112;
    public const int Server1Port = InDocker ? 5173 : 7173;
    public const int Server2Port = InDocker ? 5156 : 7156;
    
    public const int ThisPort = Server1Port;
    
    public const string BaseUrl = $"https://{ServerIp}:"; //local

    public static readonly string ThisServerUrl = $"https://{ServerIp}:{ThisPort}"; //docker

    public static readonly string PartitionLeader = $"https://{ServerIp}:{LeaderPort}"; //local
    public static readonly string Server1 = $"https://{ServerIp}:{Server1Port}"; //local
    public static readonly string Server2 = $"https://{ServerIp}:{Server2Port}"; //local
    public const int Id = 1;
}