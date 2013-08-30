//Explicação em: http://www.brunokenj.net/blog/index.php/2009/10/21/ordenacao-dinamica-com-linq/
//l = lista.OrderByDescending(c => c.Descricao).ToList();
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Web.Util
{
    public static class EnumerableExtensions
    {

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> items, string propertyName)
        {

            return OrderByPropertyName<T>(items, propertyName, true);

        }



        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> items, string propertyName)
        {

            return OrderByPropertyName<T>(items, propertyName, false);

        }



        private static IEnumerable<T> OrderByPropertyName<T>(this IEnumerable<T> source, string propertyName, bool ascending)
        {

            if (source == null)
            {

                throw new ArgumentNullException("source");

            }



            if (String.IsNullOrEmpty(propertyName))
            {

                return source;

            }



            var Object = Expression.Parameter(typeof(T), "Object");

            var EnumeratedObject = Expression.Parameter(typeof(IEnumerable<T>), "EnumeratedObject");

            var Property = Expression.Property(Object, propertyName);

            var Lamda = Expression.Lambda(Property, Object);

            var Method = Expression.Call(typeof(Enumerable), ascending ? "OrderBy" : "OrderByDescending", new[] { typeof(T), Lamda.Body.Type }, EnumeratedObject, Lamda);

            var SortedLamda = Expression.Lambda<Func<IEnumerable<T>, IOrderedEnumerable<T>>>(Method, EnumeratedObject).Compile();

            return SortedLamda(source);

        }

    }

}
