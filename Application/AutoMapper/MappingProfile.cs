using AutoMapper;
using System.Reflection;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile(Assembly assembly)
    {
        var types = assembly
            .GetExportedTypes()
            .Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapProfile<,>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            const string mappingMethodName = "Mapping";
            var methodInfo = type.GetMethod(mappingMethodName) ?? type.GetInterface("IMapProfile`2")?.GetMethod(mappingMethodName);

            methodInfo?.Invoke(instance, [this]);
        }
    }
}
