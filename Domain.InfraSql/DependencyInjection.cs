using Application.Common;
using Domain.Entity.Repositories;
using Domain.InfraSqlServer.Persistense;
using Domain.InfraSqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.InfraSqlServer;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddContext(configuration)
            .AddRepositories();

    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ProductAppConnectionString")))
            .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>();
    }
}

