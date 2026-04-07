
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyShop_v2.Application.Interfaces.Base;
using MyShop_v2.Domain.Entities.Base.Interface;
using MyShop_v2.Infrastructure.Data;

namespace MyShop_v2.Infrastructure.Repositories.Base
{
    public class GenericRepository<T, TId> : IRepository<T, TId> where T : class, IEntity<TId>
    {
        protected readonly AppDbContext context;

        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            _dbSet = context.Set<T>();
        }

        public virtual T GetById(TId id) => _dbSet.Find(id);

        public virtual TId GetId(Expression<Func<T, bool>> predicate, bool noTracking = true )
        {
            var query = _dbSet.AsQueryable();
            if (noTracking) query.AsNoTracking();
            var entity = query.SingleOrDefault(predicate);
            return entity == null ? default : entity.Id;
        }

        public async Task<T> GetByIdAsync(TId id) => await _dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync(bool noTracking = true, bool includeRelation = false)
        {
            var query = _dbSet.AsQueryable();
            if (noTracking)
                query = query.AsNoTracking();
            if (includeRelation)
                query = AddRelations(query);
            return await query.ToListAsync();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate, bool noTracking = true, bool includeRelation = false)
        {
            var query = _dbSet.Where(predicate);
            if (noTracking) query = query.AsNoTracking();
            if (includeRelation)
                query = AddRelations(query);
            return query.ToList();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, bool includeRelation = false)
        {
            var query = _dbSet.Where(predicate);
            if (noTracking) query = query.AsNoTracking();
            if (includeRelation)
                query = AddRelations(query);
            return await query.ToListAsync();
        }

        public virtual T GetItem(Expression<Func<T, bool>> predicate, bool noTracking)
        {
            //var query = _dbSet;
            var query = _dbSet.AsQueryable();
            if (noTracking) query.AsNoTracking();
            return query.SingleOrDefault(predicate);
        }

        public virtual T Add(T entity)
        {
            var result = _dbSet.Add(entity);
            return result.Entity;
        }

        public virtual T Update(T entity)
        {
            var result = _dbSet.Update(entity);
            return result.Entity;
        }

        public virtual T Delete(T entity)
        {
            var result = _dbSet.Remove(entity);
            return result.Entity;
        }

        public virtual int Count(Expression<Func<T, bool>> predicate, bool noTracking = true)
        {
            var query = _dbSet.Where(predicate);
            if (noTracking) query = query.AsNoTracking();
            return query.Count();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate, bool noTracking = true)
        {
            var query = _dbSet.Where(predicate);
            if (noTracking) query = query.AsNoTracking();
            return await query.CountAsync();
        }

        public void SaveChanges() => context.SaveChanges();
   
        public Task SaveChangesAsync() => context.SaveChangesAsync();

        public virtual TId CreateOrUpdate(T entity)
        {
            // Check if the entity is new by checking for the default ID value
            if (EqualityComparer<TId>.Default.Equals(entity.Id, default))
            {
                // This is a new entity, so we add it to the database
                _dbSet.Add(entity);
            }
            else
            {
                // This is an existing entity, so we attach and update it
                // Note: SetValues can be used, but this is a cleaner, more common EF pattern.
                _dbSet.Update(entity);
            }

            // Call SaveChanges only once for both create and update operations
            context.SaveChanges();

            // The ID will be populated on the entity object after SaveChanges,
            // whether it's a new one or an existing one.
            return entity.Id;
        }

        // public virtual async Task<PaginationResponse<T>> GetListByPageAsync(Expression<Func<T, bool>>? predicate, int pageIndex, int perPageNo, bool noTracking = true)
        // {
        //     PaginationResponse<T> response = new();

        //     var query = predicate is null ? _dbSet.AsQueryable() : _dbSet.Where(predicate);

        //     query = AddRelations(query);

        //     if (noTracking) query = query.AsNoTracking();

        //     //response.Page = pageIndex;
        //     response.TotalRows = query.Count();

        //     if (pageIndex > 0)
        //     {
        //         query = query.Skip(pageIndex * perPageNo);
        //     }

        //     query = query.Take(perPageNo);

        //     response.Result = await query.ToListAsync();

        //     return response;
        // }


        protected virtual IQueryable<T> AddRelations(IQueryable<T> query) => query;
    }
}