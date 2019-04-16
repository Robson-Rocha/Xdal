using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Xdal
{
    /// <summary>
    /// Provides methods for querying an <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntity{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Type of the primary key of the TEntity</typeparam>
    public interface IReadOnlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Gets the <see cref="IUnitOfWork"/> that will be used by the repository.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> which can be used to build LINQ queries against the data store.
        /// </summary>
        IQueryable<TEntity> All { get; }

        /// <summary>
        /// Gets an <see cref="IList{T}"/> which contains the names of the navigation properties which should be eagerly loaded on all the query results.
        /// </summary>
        IList<string> Includes { get; }

        /// <summary>
        /// Specifies the names of the navigation properties which should be eagerly loaded only in the subsequent query results.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties to be eagerly loaded in the query results.</param>
        /// <returns>An <see cref="IReadOnlyRepository{TEntity, TKey}"/> containing the navigation properties specified in ths method and in the <see cref="Includes"/> property.</returns>
        IReadOnlyRepository<TEntity, TKey> Include(params string[] navigationProperties);

        /// <summary>
        /// Queries the <see cref="IUnitOfWork"/> for an entity identified with the supplied Id.
        /// </summary>
        /// <param name="id">That Id that should be queried for.</param>
        /// <returns>An <c><see cref="IEntity"/></c> representing the found record if it is found in the <see cref="IUnitOfWork"/>, <c>null</c> otherwise.</returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Executes the provided <see cref="IQueryable{TResult}"/> against this <see cref="IRepository{TEntity}"/>, and return the results.
        /// </summary>
        /// <typeparam name="TResult">The type of the results.</typeparam>
        /// <param name="query">The <see cref="IQueryable{TResult}"/> to be executed against this <see cref="IRepository{TEntity}"/>.</param>
        /// <returns>An instance of <c>TResult</c> containing the results.</returns>
        TResult Get<TResult>(IQueryable<TResult> query);

        /// <summary>
        /// Executes the provided lambda expression, passing an <see cref="IQueryable{TEntity}"/> equivalent to the <see cref="All"/> property, then executes the <see cref="IQueryable{TResult}"/> against this <see cref="IRepository{TEntity}"/>, and return the results<see cref="IQueryable{TResult}"/> returned by the lambda expression against this <see cref="IRepository{TEntity}"/>, and return the results.
        /// </summary>
        /// <typeparam name="TResult">The type of the results.</typeparam>
        /// <param name="query">A lambda expression which should produce the <see cref="IQueryable{TResult}"/> to be executed against this <see cref="IRepository{TEntity}"/>.</param>
        /// <returns>An instance of <c>TResult</c> containing the results.</returns>
        TResult Get<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query);

        /// <summary>
        /// Queries asynchronously the <see cref="IUnitOfWork"/> for an entity identified with the supplied Id.
        /// </summary>
        /// <param name="id">That Id that should be queried for.</param>
        /// <returns>An <see cref="Task{TEntity}"/> which should return the record if it is found in the <see cref="IUnitOfWork"/>, <c>null</c> otherwise.</returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Executes asynchronously the provided <see cref="IQueryable{TResult}"/> against this <see cref="IRepository{TEntity}"/>, and return the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the results.</typeparam>
        /// <param name="query">The <see cref="IQueryable{TResult}"/> to be executed against this <see cref="IRepository{TEntity}"/>.</param>
        /// <returns>An instance of <see cref="Task{TResult}"/> which should return the result.</returns>
        Task<TResult> GetAsync<TResult>(IQueryable<TResult> query);

        /// <summary>
        /// Executes asynchronously the provided lambda expression, passing an <see cref="IQueryable{TEntity}"/> equivalent to the <see cref="All"/> property, then executes the <see cref="IQueryable{TResult}"/> against this <see cref="IRepository{TEntity}"/>, and return the results<see cref="IQueryable{TResult}"/> returned by the lambda expression against this <see cref="IRepository{TEntity}"/>, and return the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the results.</typeparam>
        /// <param name="query">A lambda expression which should produce the <see cref="IQueryable{TResult}"/> to be executed against this <see cref="IRepository{TEntity}"/>.</param>
        /// <returns>An instance of <see cref="Task{TResult}"/> which should return the results.</returns>
        Task<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query);

        /// <summary>
        /// Executes asynchronously the provided <see cref="IQueryable{TResult}"/> against this <see cref="IRepository{TEntity}"/>, and return the results list.
        /// </summary>
        /// <typeparam name="TResult">The type of the results.</typeparam>
        /// <param name="query">The <see cref="IQueryable{TResult}"/> to be executed against this <see cref="IRepository{TEntity}"/>.</param>
        /// <returns>An instance of <see><cref>Task{List{TResult}}</cref></see> which should return the results list.</returns>
        Task<List<TResult>> GetListAsync<TResult>(IQueryable<TResult> query);

        /// <summary>
        /// Executes asynchronously the provided lambda expression, passing an <see cref="IQueryable{TEntity}"/> equivalent to the <see cref="All"/> property, then executes the <see cref="IQueryable{TResult}"/> against this <see cref="IRepository{TEntity}"/>, and return the results<see cref="IQueryable{TResult}"/> returned by the lambda expression against this <see cref="IRepository{TEntity}"/>, and return the results list.
        /// </summary>
        /// <typeparam name="TResult">The type of the results.</typeparam>
        /// <param name="query">A lambda expression which should produce the <see cref="IQueryable{TResult}"/> to be executed against this <see cref="IRepository{TEntity}"/>.</param>
        /// <returns>An instance of <see><cref>Task{List{TResult}}</cref></see> which should return the results list.</returns>
        Task<List<TResult>> GetListAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query);

        /// <summary>
        /// Executes asynchronously the provided <see cref="IQueryable{TResult}"/> against this <see cref="IRepository{TEntity}"/>, and return the results array.
        /// </summary>
        /// <typeparam name="TResult">The type of the results.</typeparam>
        /// <param name="query">The <see cref="IQueryable{TResult}"/> to be executed against this <see cref="IRepository{TEntity}"/>.</param>
        /// <returns>An instance of <see><cref>Task{TResult[]}</cref></see> which should return the results array.</returns>
        Task<TResult[]> GetArrayAsync<TResult>(IQueryable<TResult> query);

        /// <summary>
        /// Executes asynchronously the provided lambda expression, passing an <see cref="IQueryable{TEntity}"/> equivalent to the <see cref="All"/> property, then executes the <see cref="IQueryable{TResult}"/> against this <see cref="IRepository{TEntity}"/>, and return the results<see cref="IQueryable{TResult}"/> returned by the lambda expression against this <see cref="IRepository{TEntity}"/>, and return the results array.
        /// </summary>
        /// <typeparam name="TResult">The type of the results.</typeparam>
        /// <param name="query">A lambda expression which should produce the <see cref="IQueryable{TResult}"/> to be executed against this <see cref="IRepository{TEntity}"/>.</param>
        /// <returns>An instance of <see><cref>Task{TResult[]}</cref></see> which should return the results array.</returns>
        Task<TResult[]> GetArrayAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query);
    }

    /// <summary>
    /// Provides methods for querying an <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntity"/>.</typeparam>
    public interface IReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity, long>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Specifies the names of the navigation properties which should be eagerly loaded only in the subsequent query results.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties to be eagerly loaded in the query results.</param>
        /// <returns>An <see cref="IReadOnlyRepository{TEntity}"/> containing the navigation properties specified in ths method and in the Includes property.</returns>
        new IReadOnlyRepository<TEntity> Include(params string[] navigationProperties);
    }
}