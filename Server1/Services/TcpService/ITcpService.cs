using Server1.Models;

namespace Server1.Services.TcpService;

public interface ITcpService
{
    public Result? TcpSave(Data data, int serverPort);
}