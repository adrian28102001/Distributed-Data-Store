using Server1.Models;

namespace Server1.Services.TcpService;

public interface ITcpService
{
    public Task Run();
    public Result? TcpSave(Data data, int serverPort);
}