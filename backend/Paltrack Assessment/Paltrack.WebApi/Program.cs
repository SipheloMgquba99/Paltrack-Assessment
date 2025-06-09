using Microsoft.EntityFrameworkCore;
using Paltrack.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnections");

    try
    {
        using var testConnection = new Npgsql.NpgsqlConnection(connectionString);
        testConnection.Open();

        options.UseNpgsql(connectionString);
    }
    catch
    {
        options.UseInMemoryDatabase("FallbackDb");
    }
});


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Paltrack_";
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection(); 
}

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var env = services.GetRequiredService<IHostEnvironment>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        context.Database.Migrate();

        if (!context.LogisticsPartners.Any())
        {
            SeedData.SeedPartnerOrganizations(context, env);
            logger.LogInformation("Database seeding completed successfully.");
        }
        else
        {
            logger.LogInformation("Database already contains seeded data. Skipping seeding.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.Run();
