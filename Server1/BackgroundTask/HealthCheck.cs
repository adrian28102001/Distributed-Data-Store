using Server1.Services.HealthService;
using static System.Threading.Tasks.Task;

namespace Server1.BackgroundTask;

public class HealthCheck : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HealthCheck(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Delay(5000, stoppingToken);
        using var scope = _serviceScopeFactory.CreateScope();
        var scoped = scope.ServiceProvider.GetRequiredService<IHealthService>();
        await scoped.CheckHealth();
    }
}