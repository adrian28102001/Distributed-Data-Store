using PartitionLeader.Models;

namespace PartitionLeader.Services.TcpService;

public interface ITcpService
{
    public Result? TcpSave(Data data, int serverPort);
}