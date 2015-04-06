using System.Collections.Generic;
using AutoMapper;

namespace DataBusinessService
{
    public static class AutoMapExtensions
    {
        public static IList<TD> MapTo<TS, TD>(this IList<TS> items)
        {
            return Mapper.Map<IList<TD>>(items); //TODO: ???            
        }

        public static T MapTo<T>(this object obj)
        {
            if (obj == null)
            {
                return default(T);
            }

            return Mapper.Map<T>(obj);
        }

        public static IEnumerable<TD> MapToList<TD>(this IEnumerable<object> items)
        {
            return Mapper.Map<IEnumerable<TD>>(items);
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}