using DesignDocu.Auth.Api;
using DesignDocu.Authorization;
using DesignDocu.Common.Module;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddModules(builder.Configuration.GetSection("Modules"));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuth();
app.MapAuthEndpoints();

app.UseModules();

app.Run();