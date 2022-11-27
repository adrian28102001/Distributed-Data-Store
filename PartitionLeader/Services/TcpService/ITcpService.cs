using PartitionLeader.Models;

namespace PartitionLeader.Services.TcpService;

public interface ITcpService
{
    public string TcpSave(Data data);
}