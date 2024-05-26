using DesignDocu.Common.Api.Configuration;
using DesignDocu.Common.Api.ErrorHandling;
using DesignDocu.Common.Module;
using DesignDocu.Infrastructure.Auth.Authorization.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFileConfiguration(builder.Environment);

builder.Services.AddIdentityAuthorization(builder.Configuration.GetSection("Authorization"));
builder.Services.AddModules(builder.Configuration.GetSection("Modules"));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseErrorHandling();
app.UseIdentityAuthorization();

app.UseModules();

app.Run();