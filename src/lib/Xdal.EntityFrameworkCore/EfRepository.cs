using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xdal.EntityFrameworkCore
{
    /// <summary>
    /// Represents a generic repository based on Entity Framework Core which can be used to query and execute commands against an <see cref="EfUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntity{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Type of the primary key of the TEntity</typeparam>
    public abstract class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly EfUnitOfWork _unitOfWork;
        private readonly DbSet<TEntity> _set;

        /// <inheritdoc />
        protected EfRepository(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _set = DbContext.Set<TEntity>();
            Includes = new List<string>();
        }

        /// <summary>
        /// Gets the <see cref="DbContext"/> of the <see cref="EfUnitOfWork"/>.
        /// </summary>
        protected DbContext DbContext => _unitOfWork;

        private IQueryable<TEntity> SetWithIncludes()
        {
            IQueryable<TEntity> query = _set;
            foreach (var include in Includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        /// <inheritdoc />
        public virtual IUnitOfWork UnitOfWork => _unitOfWork;

        /// <inheritdoc />
        public virtual IQueryable<TEntity> All => SetWithIncludes();

        /// <inheritdoc />
        public virtual IList<string> Includes { get; }

        /// <inheritdoc />
        IReadOnlyRepository<TEntity, TKey> IReadOnlyRepository<TEntity, TKey>.Include(params string[] navigationProperties)
        {
            foreach (string navigationProperty in navigationProperties)
            {
                Includes.Add(navigationProperty);
            }
            return this;
        }

        /// <inheritdoc />
        public virtual TEntity GetById(TKey id)
            => All.FirstOrDefault(e => e.Id.Equals(id));

        /// <inheritdoc />
        public virtual TResult Get<TResult>(IQueryable<TResult> query)
            => query.FirstOrDefault();

        /// <inheritdoc />
        public virtual TResult Get<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query)
            => Get(query(All));

        /// <inheritdoc />
        public virtual async Task<TEntity> GetByIdAsync(TKey id)
            => await All.FirstOrDefaultAsync(e => e.Id.Equals(id));

        /// <inheritdoc />
        public virtual async Task<TResult> GetAsync<TResult>(IQueryable<TResult> query)
            => await query.FirstOrDefaultAsync();

        /// <inheritdoc />
        public virtual async Task<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query)
            => await GetAsync(query(All));

        /// <inheritdoc />
        public virtual async Task<List<TResult>> GetListAsync<TResult>(IQueryable<TResult> query)
            => await query.ToListAsync();

        /// <inheritdoc />
        public virtual async Task<List<TResult>> GetListAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query)
            => await GetListAsync(query(All));

        /// <inheritdoc />
        public virtual async Task<TResult[]> GetArrayAsync<TResult>(IQueryable<TResult> query)
            => await query.ToArrayAsync();

        /// <inheritdoc />
        public virtual async Task<TResult[]> GetArrayAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query)
            => await GetArrayAsync(query(All));

        /// <inheritdoc />
        public virtual void Delete(TEntity entity)
            => _set.Remove(entity);

        /// <inheritdoc />
        public virtual void Delete(TKey id)
        {
            TEntity entity =_set.Find(id);
            if (entity != null)
                Delete(entity);
        }

        /// <inheritdoc />
        public virtual void Delete(IEnumerable<TEntity> entities)
            => _set.RemoveRange(entities);

        /// <inheritdoc />
        public virtual void Delete(IEnumerable<TKey> ids)
        {
            foreach (var id in ids)
                Delete(id);
        }

        /// <inheritdoc />
        public virtual void Add(TEntity entity) => _set.Add(entity);

        /// <inheritdoc />
        public virtual void Add(IEnumerable<TEntity> entities) => _set.AddRange(entities);

        /// <inheritdoc />
        public virtual void Update(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                _set.Update(entity);
        }

        /// <inheritdoc />
        public virtual void Update(IEnumerable<TEntity> entities)
            => _set.UpdateRange(entities.Where(e => DbContext.Entry(e).State == EntityState.Detached));

    }

    /// <summary>
    /// Represents a generic repository based on Entity Framework Core which can be used to query and execute commands against an <see cref="EfUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TEntity">A class that implements <see cref="IEntity"/> and is registered as an entity in an <see cref="EfUnitOfWork"/>.</typeparam>
    public abstract class EfRepository<TEntity> : EfRepository<TEntity, long>, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <inheritdoc />
        protected EfRepository(EfUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <inheritdoc />
        public IReadOnlyRepository<TEntity> Include(params string[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }

}
