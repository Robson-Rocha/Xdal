namespace Xdal.EntityFrameworkCore.Sqlite
{
    /// <summary>
    /// Represents a generic repository based on Entity Framework Core which can be used to query and execute commands against an <see cref="EfSqliteUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntity{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Type of the primary key of the TEntity</typeparam>
    public class EfSqliteRepository<TEntity, TKey> : EfRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <inheritdoc />
        protected internal EfSqliteRepository(EfSqliteUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// Represents a generic repository based on Entity Framework Core which can be used to query and execute commands against an <see cref="EfSqliteUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">A class that implements <see cref="IEntity"/> and is registered as an entity in an <see cref="EfUnitOfWork"/>.</typeparam>
    public class EfSqliteRepository<TEntity> : EfRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <inheritdoc />
        protected internal EfSqliteRepository(EfSqliteUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

}
