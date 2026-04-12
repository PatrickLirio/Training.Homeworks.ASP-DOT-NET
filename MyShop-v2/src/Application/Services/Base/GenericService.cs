

using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces.Base;
using MyShop_v2.Domain.Entities.Base.Interface;

namespace MyShop_v2.Application.Services.Base
{
    public class GenericService<T, TId> where T : class, IEntity<TId>
    {
        protected readonly IRepository<T, TId> repository;
    protected readonly FilterService filterService;

    public GenericService(IRepository<T, TId> repository, FilterService filterService)
    {
        this.repository = repository;
        this.filterService = filterService;
    }

    public virtual T GetById(TId id) => repository.GetById(id);
    public virtual TId GetId(FilterGroup filterGroup, bool noTracking = true) => repository.GetId(filterService.BuildPredicate<T>(filterGroup), noTracking);

    public virtual async Task<T> GetByIdAsync(TId id) => await repository.GetByIdAsync(id);

    public virtual async Task<IEnumerable<T>> GetAllAsync(bool noTracking = true) => await repository.GetAllAsync(noTracking);

    public virtual IEnumerable<T> Find(FilterGroup filterGroup, bool noTracking = true) => repository.Find(filterService.BuildPredicate<T>(filterGroup), noTracking);

    public virtual async Task<IEnumerable<T>> FindAsync(FilterGroup filterGroup, bool noTracking = true) => await repository.FindAsync(filterService.BuildPredicate<T>(filterGroup), noTracking);

    public virtual T GetItem(FilterGroup filterGroup, bool noTracking = true) => repository.GetItem(filterService.BuildPredicate<T>(filterGroup), noTracking);

    public virtual T Add(T entity)
    {
        repository.Add(entity);
        repository.SaveChanges();

        return entity;
    }

    public virtual T Update(T entity)
    {
        repository.Update(entity);
        repository.SaveChanges();
        return entity;
    }

    public virtual T Delete(T entity)
    {
        repository.Delete(entity);
        repository.SaveChanges();
        return entity;
    }

    public virtual int GetCount(FilterGroup filterGroup, bool noTracking = true) => repository.Count(filterService.BuildPredicate<T>(filterGroup), noTracking);
    public virtual async Task<int> GetCountAsync(FilterGroup filterGroup, bool noTracking = true) => await repository.CountAsync(filterService.BuildPredicate<T>(filterGroup), noTracking);

    public void SaveChanges() => repository.SaveChanges();
    public async Task SaveChangesAsync() => await repository.SaveChangesAsync();

    public virtual TId CreateOrUpdate(T entity, int stage, int studentId, bool confirmed = false)
    {
        var itemId = repository.CreateOrUpdate(entity);

        repository.SaveChanges();

        return itemId;
    }

    // public virtual async Task<PaginationResponse<T>> GetListByPageAsync(PaginationFilterRequest request) => await repository.GetListByPageAsync(filterService.BuildPredicate<T>(request.Filter), request.Page, request.PageSize);

    }
}