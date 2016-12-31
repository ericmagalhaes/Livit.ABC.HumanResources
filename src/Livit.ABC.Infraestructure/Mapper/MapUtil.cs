using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.ABC.Infraestructure.Mapper
{
    public class MapUtil
    {
        public static IMapper DefaultMapper = null;

        public MapUtil(IMapper mapper)
        {
            DefaultMapper = mapper;
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return DefaultMapper.Map<TSource, TDestination>(source);
        }
        public static TDestination Map<TDestination>(object source)
        {
            return DefaultMapper.Map<TDestination>(source);
        }
    }
}
