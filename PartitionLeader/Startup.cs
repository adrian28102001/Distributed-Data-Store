using PartitionLeader.Repositories.DataStorage;
using PartitionLeader.Repositories.GenericRepository;
using PartitionLeader.Services;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.HttpService;
using PartitionLeader.Services.ServersDetails;
using PartitionLeader.Services.Sync;

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
        
        services.AddSingleton<ISyncService, SyncService>();
        services.AddSingleton<IDataService, DataService>();
        services.AddSingleton<IDataStorageRepository, DataStorageRepository>();
        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<IServerDetails, ServerDetails>();
        services.AddSingleton<IStorageStatus, StorageStatus>();
        services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddHostedService<BackgroundTask.BackgroundTask>();;
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