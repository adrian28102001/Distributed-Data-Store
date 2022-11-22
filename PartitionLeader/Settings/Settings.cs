using PartitionLeader.Models;

namespace PartitionLeader.Settings;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.PartitionLeader;
    
    public static readonly bool Leader = true;
    public static readonly string ServerIp = "localhost";  
    public static readonly int Port = 7112;  
    
    public static readonly string BaseUrl = $"https://localhost:";
    
    public static readonly string PartitionLeader = $"https://localhost:{7112}";
    public static readonly string Server1 = $"https://localhost:{7173}";
    public static readonly string Server2 = $"https://localhost:{7156}";
}
