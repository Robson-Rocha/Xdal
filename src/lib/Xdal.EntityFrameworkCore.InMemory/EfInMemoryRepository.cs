namespace Xdal.EntityFrameworkCore.InMemory
{
    /// <summary>
    /// Represents a generic, in-memory repository based on Entity Framework Core which can be used to query and execute commands against an <see cref="EfInMemoryUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EfInMemoryRepository<TEntity> : EfRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <inheritdoc />
        protected internal EfInMemoryRepository(EfInMemoryUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// Represents a generic, in-memory repository based on Entity Framework Core which can be used to query and execute commands against an <see cref="EfInMemoryUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntity{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Type of the primary key of the TEntity</typeparam>
    public class EfInMemoryRepository<TEntity, TKey> : EfRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <inheritdoc />
        protected internal EfInMemoryRepository(EfInMemoryUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
