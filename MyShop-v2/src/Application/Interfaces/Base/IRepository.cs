/*
*   T = the type of entity (e.g., User, Product, Order)
*   TId = the type of the entity’s ID (e.g., int, Guid, string) 
*   IEntity<TId> = reusing the Interface where creating/adding an ID
*
*/


using System.Linq.Expressions;
using MyShop_v2.Domain.Entities.Base.Interface;

namespace MyShop_v2.Application.Interfaces.Base
{
    public interface IRepository<T, TId> where T : class, IEntity<TId> 
    {
    T GetById(TId id);
    TId GetId(Expression<Func<T, bool>> predicate, bool noTracking = true);
    Task<T> GetByIdAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync(bool noTracking = true, bool includeRelation = false);
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate, bool noTracking = true, bool includeRelation = false);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, bool includeRelation = false);
    T GetItem(Expression<Func<T, bool>> predicate, bool noTracking = true);
    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);
    int Count(Expression<Func<T, bool>> predicate, bool noTracking = true);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, bool noTracking = true);
    void SaveChanges();
    Task SaveChangesAsync();

    TId CreateOrUpdate(T entity);

    // Task<PaginationResponse<T>> GetListByPageAsync(Expression<Func<T, bool>> predicate, int pageIndex, int perPageNo, bool noTracking = true);
    }
}