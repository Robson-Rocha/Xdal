using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xdal.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IQuery{TResult}"/>
    /// </summary>
    public static class QueryExtensions
    {
        private static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }

        /// <summary>
        /// Executes the query using the properties of the provided object as arguments.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="query">The query to be executed.</param>
        /// <param name="arguments">The object wihose properties will be used as arguments to the query.</param>
        /// <returns>The query results.</returns>
        public static TResult Execute<TResult>(this IQuery<TResult> query, object arguments) 
            => query.Execute(arguments.AsDictionary());
    }
}
