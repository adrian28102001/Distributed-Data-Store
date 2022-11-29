using PartitionLeader.Services.Sync;

namespace PartitionLeader.BackgroundTask;

public class Sync : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Sync(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000, stoppingToken);
        using var scope = _serviceScopeFactory.CreateScope();
        var scoped = scope.ServiceProvider.GetRequiredService<ISyncService>();
        await scoped.SyncData(stoppingToken);
    }
}