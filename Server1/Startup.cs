using Server1.Repositories.DataStorage;
using Server1.Repositories.GenericRepository;
using Server1.Services.DataService;
using Server1.Services.DistributionService;
using Server1.Services.HealthService;
using Server1.Services.HttpService;
using Server1.Services.Sync;
using Server1.Services.TcpService;

namespace Server1;

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