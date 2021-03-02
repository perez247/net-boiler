using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Interfaces.Mapping;

namespace Application.Infrastructure.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Map
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Type Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Type Destination { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MapperProfileHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootAssembly"></param>
        /// <returns></returns>
        public static IList<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                    from type in types
                    from instance in type.GetInterfaces()
                    where
                        instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select new Map
                    {
                        Source = type.GetInterfaces().First().GetGenericArguments().First(),
                        Destination = type
                    }).ToList();

            return mapsFrom;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootAssembly"></param>
        /// <returns></returns>
        public static IList<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                    from type in types
                    from instance in type.GetInterfaces()
                    where
                        typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select (IHaveCustomMapping)Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }
    }
}