using Server1.Repositories;
using Server1.Repositories.DataStorage;
using Server1.Repositories.GenericRepository;
using Server1.Services;
using Server1.Services.DataService;
using Server1.Services.Sync;

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

        services.AddSingleton<ISyncService, SyncService>();
        services.AddSingleton<IDataService, DataService>();
        services.AddSingleton<IDataStorageRepository, DataStorageRepository>();
        // services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
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