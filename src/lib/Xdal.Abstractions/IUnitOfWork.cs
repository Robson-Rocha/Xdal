using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Xdal
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a group of query or commands performed against a Data Store, that can be committed or discarded as a whole.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits the unit of work in the data store synchronously.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commits the unit of work in the data store asynchronously.
        /// </summary>
        /// <returns><see cref="Task"/> which represents the commit async workload.</returns>
        Task CommitAsync();

        /// <summary>
        /// Occurs when the unit of work is about to be committed.
        /// </summary>
        event EventHandler<OnCommittingEventArgs> OnCommitting;

        /// <summary>
        /// Occurs when the unit of work is successfully commited.
        /// </summary>
        event EventHandler<OnCommittedEventArgs> OnCommitted;

        /// <summary>
        /// Gets a new instance of an <see cref="IRepository{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">A class which implements <see cref="IEntity"/> and is registered with the <see cref="IUnitOfWork"/>.</typeparam>
        /// <returns>A new instance of <see cref="IRepository{TEntity}"/>, already initialized with this <see cref="IUnitOfWork"/>.</returns>
        IRepository<TEntity> GetRepository<TEntity>() 
            where TEntity : class, IEntity;

        /// <summary>
        /// Gets a new instance of an <see cref="IRepository{TEntity, TKey}"/>.
        /// </summary>
        /// <typeparam name="TEntity">A class which implements <see cref="IEntity{TKey}"/> and is registered with the <see cref="IUnitOfWork"/>.</typeparam>
        /// <typeparam name="TKey">Type of the primary key of the TEntity</typeparam>
        /// <returns>A new instance of <see cref="IRepository{TEntity, TKey}"/>, already initialized with this <see cref="IUnitOfWork"/>.</returns>
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="IUnitOfWork"/> should not be disposed.
        /// </summary>
        /// <remarks>Use with caution! This property is useful in scenarios when this <see cref="IUnitOfWork"/> must be used asynchonously in a dependency injected scenario, like a web request that can finish before the unit of work itself, or must be used in a background task that would finish after the request. In those scenarios, you must manually dispose of the <see cref="IUnitOfWork"/> after the work is finished.</remarks>
        bool PreventDisposal { get; set; }

        /// <summary>
        /// Gets a set of not persisted information associated to this <see cref="IUnitOfWork"/> instance which should be available to all their consumers.
        /// </summary>
        IDictionary<string, object> ContextData { get; }
    }
}
