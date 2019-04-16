using Microsoft.EntityFrameworkCore;
using System;

namespace Xdal.EntityFrameworkCore.InMemory
{
    /// <summary>
    /// Represents an Entity Framework Core based <see cref="IUnitOfWork"/> which uses the In-Memory Database Provider.
    /// </summary>
    public class EfInMemoryUnitOfWork : EfUnitOfWork
    {
        private static DbContextOptions<EfUnitOfWork> BuildOptions(string databaseName, Action<DbContextOptionsBuilder<EfUnitOfWork>> dbContextOptionsBuilderAction)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfUnitOfWork>();
            optionsBuilder.UseInMemoryDatabase(databaseName);
            dbContextOptionsBuilderAction?.Invoke(optionsBuilder);
            return optionsBuilder.Options;
        }

        /// <inheritdoc />
        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            CheckDisposed();
            return new EfInMemoryRepository<TEntity>(this);
        }

        public override IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        {
            CheckDisposed();
            return new EfInMemoryRepository<TEntity, TKey>(this);
        }

        /// <inheritdoc />
        public EfInMemoryUnitOfWork(string databaseName, Action<ModelBuilder> modelBuilderAction, Action<DbContextOptionsBuilder<EfUnitOfWork>> dbContextOptionsBuilderAction = null)
            : base(BuildOptions(databaseName, dbContextOptionsBuilderAction), modelBuilderAction)
        {
        }
    }
}
