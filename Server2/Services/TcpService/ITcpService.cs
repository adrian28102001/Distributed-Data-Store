using Server2.Models;

namespace Server2.Services.TcpService;

public interface ITcpService
{
    public Result? TcpSave(Data data, int serverPort);
}