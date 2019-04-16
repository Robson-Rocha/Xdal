using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Xdal.EntityFrameworkCore
{
    /// <summary>
    /// Represents an Entity Framewofk Core based <see cref="IUnitOfWork"/>.
    /// </summary>
    public abstract class EfUnitOfWork : DbContext, IUnitOfWork
    {
        private bool _isDisposed;
        private readonly Action<ModelBuilder> _modelBuilderAction;

        /// <inheritdoc />
        public event EventHandler<OnCommittingEventArgs> OnCommitting;

        /// <inheritdoc />
        public event EventHandler<OnCommittedEventArgs> OnCommitted;

        /// <inheritdoc />
        protected EfUnitOfWork(DbContextOptions<EfUnitOfWork> options, Action<ModelBuilder> modelBuilderAction) : base(options) => _modelBuilderAction = modelBuilderAction;

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder) => _modelBuilderAction?.Invoke(modelBuilder);

        /// <summary>
        /// Prevents this Unit of Work to be used when the disposal has already been signaled, throwing an <see cref="ObjectDisposedException"/>. 
        /// </summary>
        /// <exception cref="ObjectDisposedException">Throwed when the object was already signaled as disposed.</exception>
        protected virtual void CheckDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(EfUnitOfWork));
        }

        /// <inheritdoc />
        public virtual void Commit() => CommitAsync().Wait();

        /// <inheritdoc />
        public virtual async Task CommitAsync()
        {
            bool successful = false;
            Exception exception = null;
            CheckDisposed();
            try
            {
                OnCommitting?.Invoke(this, new OnCommittingEventArgs(this));
                await SaveChangesAsync();
                successful = true;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                OnCommitted?.Invoke(this, new OnCommittedEventArgs(this, successful, exception));
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            if (PreventDisposal)
                return;

            _isDisposed = true;
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements the disposal logic.
        /// </summary>
        /// <param name="disposing">Indicates that this instance has to be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                OnCommitting = null;
                OnCommitted = null;
                base.Dispose();
            }
        }

        /// <inheritdoc />
        public abstract IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntity;

        /// <inheritdoc />
        public abstract IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>;

        /// <inheritdoc />
        public virtual bool PreventDisposal { get; set; }

        /// <inheritdoc />
        public virtual IDictionary<string, object> ContextData { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Searches for <see cref="IEntity"/> implementations in the assembly that contains TEntityType, and map them as entities on the <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TEntityType">The type whose <see cref="IEntityBase"/> implementations will be searched for. This type itself does not need to be an implementation of <see cref="IEntity"/>, but only an "handle" type used to get easily to the assembly and ensure that it is already loaded into the application domain.</typeparam>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure the entities on the <see cref="DbContext"/>.</param>
        public static void MapEntityTypesFrom<TEntityType>(ModelBuilder modelBuilder)
        {
            Type ientityType = typeof(IEntityBase);
            MethodInfo entityMethod = typeof(ModelBuilder).GetMethod("Entity", new[] { typeof(Type) });
            if (entityMethod == null) return; //Nunca vai acontecer... Mas... 🤷
            foreach (Type type in typeof(TEntityType).Assembly.GetTypes().Where(t => ((TypeInfo)t).ImplementedInterfaces.Contains(ientityType) && t.IsClass))
                entityMethod.Invoke(modelBuilder, new object[] {type});
        }
    }
}
