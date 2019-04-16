using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Xdal
{
    /// <summary>
    /// Represents a reusable query. This is an anchor interface, and should not be used directly. Use <see cref="IQuery{TResult}"/> instead.
    /// </summary>
    public interface IQuery
    { }

    /// <summary>
    /// Represents a reusable query that consumes an <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TResult">The result type of the query.</typeparam>
    public interface IQuery<out TResult> : IQuery
    {
        /// <summary>
        /// Gets or sets the <see cref="IUnitOfWork"/> that should be used to execute the query.
        /// </summary>
        IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Execute the query against the defined <see cref="IUnitOfWork"/> using the given arguments.
        /// </summary>
        /// <param name="arguments">Arguments to be used by the query.</param>
        /// <returns>Instance of TResult containing the query results.</returns>
        TResult Execute(IDictionary<string, object> arguments);
    }
}