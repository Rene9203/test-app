using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp.Core.Repositories;
using TestApp.Core.SharedKernel;
using TestApp.Infraestructure.DbConfig;

namespace TestApp.Infraestructure.Repositories
{
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey>, IDisposable where TEntity : Entity<TKey>
    {
        protected AppDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public async Task<PagedResult<TEntity>> Get(PagedFilter<TEntity> pagedFilter,
            IEnumerable<Expression<Func<TEntity, object>>> includes, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var elementsCountTask = CountAsync(pagedFilter.Filter);

            IQueryable<TEntity> query = includes != null
                ? includes.Aggregate(_context.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include))
                : _context.Set<TEntity>();

            query = query.Where(pagedFilter.Filter);

            if (orderBy != null)
                query = orderBy(query);

            var elementsTask = query.Skip(pagedFilter.GetOmittedPages())
                .Take(pagedFilter.Limit)
                .AsNoTracking()
                .ToListAsync();


            await Task.WhenAll(elementsTask, elementsCountTask);
            return new PagedResult<TEntity>(elementsTask.Result, pagedFilter.Page, pagedFilter.Limit,
                elementsCountTask.Result);
        }

        public async Task<PagedResult<TEntity>> Get(PagedFilter<TEntity> pagedFilter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            var elementsCountTask = CountAsync(pagedFilter.Filter);

            IQueryable<TEntity> query = _context.Set<TEntity>().Where(pagedFilter.Filter);
            if (orderBy != null)
                query = orderBy(query);

            var elementsTask = query.Skip(pagedFilter.GetOmittedPages())
                .Take(pagedFilter.Limit)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(elementsTask, elementsCountTask);
            return new PagedResult<TEntity>(elementsTask.Result, pagedFilter.Page, pagedFilter.Limit,
                elementsCountTask.Result);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(filter).AsQueryable();
            if (orderBy == null)
                return await query.ToListAsync();

            query = orderBy(query);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            if (orderBy == null)
                return await _context.Set<TEntity>().ToListAsync();

            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            query = orderBy(query);
            return await query.ToListAsync();
        }

        public TEntity GetById(TKey id)
        {
            return _context.Set<TEntity>().SingleOrDefault(e => e.Id.Equals(id));
        }

        public TEntity GetById(TKey id, IEnumerable<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = includes != null
                ? includes.Aggregate(_context.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include))
                : _context.Set<TEntity>();

            return query.SingleOrDefault(e => e.Id.Equals(id));
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<TEntity> GetByIdAsync(TKey id, IEnumerable<Expression<Func<TEntity, object>>> includes)
        {
            IQueryable<TEntity> query = includes != null
               ? includes.Aggregate(_context.Set<TEntity>().AsQueryable(),
                   (current, include) => current.Include(include))
               : _context.Set<TEntity>();

            return await query.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> query)
        {
            return _context.Set<TEntity>().Where(query).CountAsync();
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return _context.Set<TEntity>();
        }
    }
}
