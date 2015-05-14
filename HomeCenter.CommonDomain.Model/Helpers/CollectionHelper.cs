using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCenter.CommonDomain.Model
{
    public static class CollectionHelpers
    {
        public static void AddRange<T>(this IDbSet<T> destination,
                                       IEnumerable<T> source) where T : class
        {
            foreach(T item in source)
            {
                destination.Add(item);
            }
        }
    }
}
