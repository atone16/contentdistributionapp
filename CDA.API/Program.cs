using CDA.GraphQL;
using CDA.Managers;
using CDA.RedisCache;
using CDA.Access;
using CDA.Utilities;
using CDA.Mock;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCDAGraphQL();
builder.Services.AddAccess();
builder.Services.AddManagers();
builder.Services.AddHealthChecks();
builder.Services.AddRedisCache(builder.Configuration["RedisConnectionString"]);
builder.Services.AddUtilities();

// Seed Redis
builder.Services.AddSingleton<SeedRedis>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var redisConnectionString = builder.Configuration["RedisConnectionString"];
    return new SeedRedis(redisConnectionString);
});

var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseEndpoints(
    endpoint =>
    {
        endpoint.MapGraphQL("/");
        endpoint.MapHealthChecks("/health");
    }
);

// Seed the Default Tenant and User
var redisService = app.Services.GetRequiredService<SeedRedis>();
redisService.SeedData();

app.Run();
