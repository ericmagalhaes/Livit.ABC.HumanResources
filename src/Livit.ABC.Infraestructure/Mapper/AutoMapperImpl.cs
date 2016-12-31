using AutoMapper.Configuration;

namespace Livit.ABC.Infraestructure.Mapper
{
    /// <summary>
    /// AutoMapper implementation for the framework mapper
    /// </summary>
    public class AutoMapperImpl : IMapper
    {
        public AutoMapperImpl(MapperConfigurationExpression config)
        {
            AutoMapper.Mapper.Initialize(config);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
        public TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
    }
}