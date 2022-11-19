using Server1;
using Server1.Settings;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
Startup.Configure(app, builder.Environment);