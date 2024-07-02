using CDA.GraphQL;
using CDA.Managers;
using CDA.RedisCache;
using CDA.Access;
using CDA.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCDAGraphQL();
builder.Services.AddAccess();
builder.Services.AddManagers();
builder.Services.AddHealthChecks();
builder.Services.AddRedisCache(builder.Configuration["RedisConnectionString"]);
builder.Services.AddUtilities();
builder.Services.AddCors();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(
       options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
   );
app.UseEndpoints(
    endpoint => {
        endpoint.MapGraphQL("/");
        endpoint.MapHealthChecks("/health");
    }
);
app.Run();
