using Microsoft.EntityFrameworkCore;
using System;

namespace Xdal.EntityFrameworkCore.SqlServer
{
    /// <summary>
    /// Represents an Entity Framework Core based <see cref="IUnitOfWork"/> which uses the SQL Server Database Provider.
    /// </summary>
    public class EfSqlUnitOfWork : EfUnitOfWork
    {
        private static DbContextOptions<EfUnitOfWork> BuildOptions(string connectionString, Action<DbContextOptionsBuilder<EfUnitOfWork>> dbContextOptionsBuilderAction)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfUnitOfWork>();
            optionsBuilder.UseSqlServer(connectionString);
            dbContextOptionsBuilderAction?.Invoke(optionsBuilder);
            return optionsBuilder.Options;
        }

        /// <inheritdoc />
        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            CheckDisposed();
            return new EfSqlRepository<TEntity>(this);
        }

        /// <inheritdoc />
        public override IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        {
            CheckDisposed();
            return new EfSqlRepository<TEntity, TKey>(this);
        }

        /// <inheritdoc />
        public EfSqlUnitOfWork(string connectionString, Action<ModelBuilder> modelBuilderAction, Action<DbContextOptionsBuilder<EfUnitOfWork>> dbContextOptionsBuilderAction = null)
            : base(BuildOptions(connectionString, dbContextOptionsBuilderAction), modelBuilderAction)
        {
        }
    }
}
