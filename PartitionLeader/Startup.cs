using PartitionLeader.Repositories.DataStorage;
using PartitionLeader.Repositories.GenericRepository;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.DistributionService;
using PartitionLeader.Services.HealthService;
using PartitionLeader.Services.HttpService;
using PartitionLeader.Services.ServersDetails;
using PartitionLeader.Services.Sync;
using PartitionLeader.Services.TcpService;

namespace PartitionLeader;
public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddLogging(config => config.ClearProviders());

        services.AddSingleton<IDataStorageRepository, DataStorageRepository>();
        services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddSingleton<ISyncService, SyncService>();
        services.AddSingleton<IDataStorageService, DataStorageService>();
        services.AddSingleton<IDistributionService, DistributionService>();

        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<ITcpService, TcpService>();
        services.AddSingleton<IHealthService, HealthCheck>();

        services.AddSingleton<IServerDetails, ServerDetails>();

        services.AddHostedService<BackgroundTask.HealthCheck>();;
        services.AddHostedService<BackgroundTask.Sync>();;
    }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}