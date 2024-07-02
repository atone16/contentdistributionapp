using CDA.GraphQL;
using CDA.Managers;
using CDA.RedisCache;
using CDA.Access;
using CDA.Utilities;
using HotChocolate.AspNetCore.Voyager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCDAGraphQL();
builder.Services.AddAccess();
builder.Services.AddManagers();
builder.Services.AddHealthChecks();
builder.Services.AddRedisCache(builder.Configuration["RedisConnectionString"]);
builder.Services.AddUtilities();

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

app.Run();
