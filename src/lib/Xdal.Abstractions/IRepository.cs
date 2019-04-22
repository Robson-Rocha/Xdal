using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Xdal
{
    /// <summary>
    /// Provides methods to execute commands against an <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntity{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Type of the primary key of the TEntity</typeparam>
    public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Adds the provided instance of <c>TEntity</c> to the <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="entity">The <c>TEntity</c> to be added.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds the provided set of instances of <c>TEntity</c> to the <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="entity">The set containing the instances of <c>TEntity</c> to be added.</param>
        void Add(IEnumerable<TEntity> entity);

        /// <summary>
        /// Updates the provided instance of <c>TEntity</c> in the <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="entity">The <c>TEntity</c> to be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the provided set of instances of <c>TEntity</c> in the <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="entities">The set containing the instances of <c>TEntity</c> to be updated.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the provided instance of <c>TEntity</c> from the <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="entity">The <c>TEntity</c> to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Finds and deletes the entity identified with the provided Id from the <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="id">The id of the entity to be deleted.</param>
        void Delete(TKey id);

        /// <summary>
        /// Delete the provided set of instances of <c>TEntity</c> from the <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="entities">The set containing the instances of <c>TEntity</c> to be deleted.</param>
        void Delete(IEnumerable<TEntity> entities);


        /// <summary>
        /// Finds and deletes entities identified by the provided set of Ids of <c>TEntity</c> from the <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="ids">The set containing the ids of the entities to be deleted.</param>
        void Delete(IEnumerable<TKey> ids);
    }

    /// <summary>
    /// Provides methods to execute commands against an <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntity"/>.</typeparam>
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {

    }
}
