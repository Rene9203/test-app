using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp.Core.SharedKernel;

namespace TestApp.Core.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IUnitOfWork UnitOfWork { get; }

        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        TEntity GetById(TKey id);
        TEntity GetById(TKey id, IEnumerable<Expression<Func<TEntity, object>>> includes = null);

        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByIdAsync(TKey id, IEnumerable<Expression<Func<TEntity, object>>> includes);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<PagedResult<TEntity>> Get(PagedFilter<TEntity> pagedFilter,
                                       IEnumerable<Expression<Func<TEntity, object>>> includes,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        Task<PagedResult<TEntity>> Get(PagedFilter<TEntity> pagedFilter,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
    }
}
