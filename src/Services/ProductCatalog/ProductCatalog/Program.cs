using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ApplicationDbContext>(connectionName: "productsdb");

builder.Services.AddControllers();

builder.Host.UseWolverine(options =>
{
    options.Durability.Mode = DurabilityMode.MediatorOnly;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication()
    .AddKeycloakJwtBearer("keycloak", realm: "event-sourcing", options =>
    {
        options.RequireHttpsMetadata = false;
        options.Audience = "products-api";
    });

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();  // applies any pending migrations at startup
}

app.MapHealthChecks("/health");

app.UseAuthentication();

app.UseAuthorization();

app.Run();
public partial class Program{}