using Server1.Services;
using Server1.Services.Sync;
using Server1.Services.TcpService;

namespace Server1.BackgroundTask;

public class BackgroundTask : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ITcpService _tcpService;

    public BackgroundTask(IServiceScopeFactory serviceScopeFactory, ITcpService tcpService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _tcpService = tcpService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000, stoppingToken);
        using var scope = _serviceScopeFactory.CreateScope();
        var scoped = scope.ServiceProvider.GetRequiredService<ISyncService>();
       
        _tcpService.RunTcp();
        scoped.SyncData(stoppingToken);
    }
}