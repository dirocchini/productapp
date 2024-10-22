using Application;
using Domain.InfraSqlServer;
using Domain.InfraSqlServer.Persistense;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Filter;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        builder.Services.AddCors(options =>
         {
             options.AddPolicy("AllowAllOrigins",
             builder =>
             {
                 builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
             });
         });

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.ConfigureServices((hostContext, services) =>
        {
            hostContext.Configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .Build();
        });

        var app = builder.Build();

        ApplyMigrations(app);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAllOrigins");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
    private static void ApplyMigrations(IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();
            var dbContext = services.GetRequiredService<ApplicationDbContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
                dbContext.Database.Migrate();
        }
    }
}