using Server2.Repositories.DataStorage;
using Server2.Repositories.GenericRepository;
using Server2.Services.DataService;
using Server2.Services.DistributionService;
using Server2.Services.HealthService;
using Server2.Services.HttpService;
using Server2.Services.Sync;
using Server2.Services.TcpService;

namespace Server2;

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
        services.AddSingleton<IHealthService, HealthService>();
        services.AddSingleton<ITcpService, TcpService>();

        services.AddHostedService<BackgroundTask.BackgroundTask>();
        services.AddHostedService<BackgroundTask.HealthCheck>();
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
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.Run();
    }
}