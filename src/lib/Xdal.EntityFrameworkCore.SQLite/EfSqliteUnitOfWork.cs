using Microsoft.EntityFrameworkCore;
using System;

namespace Xdal.EntityFrameworkCore.Sqlite
{
    /// <summary>
    /// Represents an Entity Framework Core based <see cref="IUnitOfWork"/> which uses the SQLite Database Provider.
    /// </summary>
    public class EfSqliteUnitOfWork : EfUnitOfWork
    {
        private static DbContextOptions<EfUnitOfWork> BuildOptions(string connectionString, Action<DbContextOptionsBuilder<EfUnitOfWork>> dbContextOptionsBuilderAction)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfUnitOfWork>();
            optionsBuilder.UseSqlite(connectionString);
            dbContextOptionsBuilderAction?.Invoke(optionsBuilder);
            return optionsBuilder.Options;
        }

        /// <inheritdoc />
        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            CheckDisposed();
            return new EfSqliteRepository<TEntity>(this);
        }

        /// <inheritdoc />
        public override IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        {
            CheckDisposed();
            return new EfSqliteRepository<TEntity, TKey>(this);
        }

        /// <inheritdoc />
        public EfSqliteUnitOfWork(string connectionString, Action<ModelBuilder> modelBuilderAction, Action<DbContextOptionsBuilder<EfUnitOfWork>> dbContextOptionsBuilderAction = null)
            : base(BuildOptions(connectionString, dbContextOptionsBuilderAction), modelBuilderAction)
        {
        }
    }
}
