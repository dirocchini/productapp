using Application.AutoMapper;
using Application.Events;
using Domain.Infra.Listeners.Listener;
using Domain.Interfaces.Listeners;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly))
            .AddServices()
            .AddAutoMapperProfile();


    private static IServiceCollection AddAutoMapperProfile(this IServiceCollection services)
        => services.AddAutoMapper((_, config) => config.AddProfile(new MappingProfile(Assembly)), Array.Empty<Assembly>());

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services
                .AddSingleton<IProductEventListener, ProductEventListener>()
                .AddSingleton<ProductEventPublisher>();
}
