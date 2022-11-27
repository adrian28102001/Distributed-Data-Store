using PartitionLeader.Models;

namespace PartitionLeader.Setting;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.PartitionLeader;
    
    public static readonly bool Leader = true;
    
    public static readonly bool InDocker = false; // set to false when running on localhost
    
    public static readonly string ServerIp = InDocker ? "host.docker.internal" : "localhost";

    public static readonly int LeaderPort = 7112;
    public static readonly int Server1Port = 7173;
    public static readonly int Server2Port = 7156;
    
    public static readonly int Server1TcpSavePort = 8081;
    public static readonly int Server2TcpSavePort = 8082;
    
    public static readonly int ThisPort = LeaderPort;
    
    public static readonly string BaseUrl = $"https://{ServerIp}:"; //local

    public static readonly string ThisServerUrl = $"https://{ServerIp}:{ThisPort}"; //docker

    public static readonly string PartitionLeader = $"http://{ServerIp}:{LeaderPort}"; //local
    public static readonly string Server1 = $"http://{ServerIp}:{Server1Port}"; //local
    public static readonly string Server2 = $"http://{ServerIp}:{Server2Port}"; //local


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