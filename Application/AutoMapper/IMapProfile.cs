using AutoMapper;

namespace Application.AutoMapper;

public interface IMapProfile<TSource, TDestination>
{
    void Mapping(Profile mapper) => mapper.CreateMap(typeof(TSource), typeof(TDestination));
}
