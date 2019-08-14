using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp.Core.Repositories;

namespace TestApp.Core.SharedKernel.Service
{
    public class GenericService<T, K> where T : Entity<K>
    {
        protected IRepository<T, K> Repository;

        public GenericService(IRepository<T, K> repository)
        {
            Repository = repository;
        }

        public virtual async Task<PagedResult<T>> Get(PagedFilter<T> pagedFilter,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             params Expression<Func<T, object>>[] include)
        {
            return await Repository.Get(pagedFilter, include, orderBy);
        }

        public virtual async Task<PagedResult<T>> Get(PagedFilter<T> pagedFilter,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await Repository.Get(pagedFilter, orderBy);
        }

        public virtual async Task<T> GetByIdAsync(K id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(K id, IEnumerable<Expression<Func<T, object>>> includes)
        {
            return await Repository.GetByIdAsync(id, includes);
        }

        public virtual async Task<T> Add(T element)
        {
            T savedEntity = Repository.Add(element);
            await Repository.UnitOfWork.SaveEntitiesAsync();

            return savedEntity;
        }

        public virtual async Task<T> Update(T element)
        {
            Repository.Update(element);
            await Repository.UnitOfWork.SaveEntitiesAsync();

            return element;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> result = await Repository.GetAllAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            IEnumerable<T> result = await Repository.GetAllAsync(filter);
            return result;
        }
    }
}
