using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces.Base;
using MyShop_v2.Domain.Entities.Base.Interface;
using AutoMapper;
using MyShop_v2.Application.DTOs.Common;

namespace MyShop_v2.Application.Services.Base
{
    public class GenericService<T, TId, TRequest, TResponse> 
        where T : class, IEntity<TId>
        where TResponse : class
    {
        protected readonly IRepository<T, TId> repository;
        protected readonly FilterService filterService;
        protected readonly IMapper mapper;

        public GenericService(IRepository<T, TId> repository, FilterService filterService, IMapper mapper)
        {
            this.repository = repository;
            this.filterService = filterService;
            this.mapper = mapper;
        }

        public virtual TResponse GetById(TId id) => mapper.Map<TResponse>(repository.GetById(id));
        
        public virtual TId GetId(FilterGroup filterGroup, bool noTracking = true) 
            => repository.GetId(filterService.BuildPredicate<T>(filterGroup), noTracking);

        public virtual async Task<TResponse> GetByIdAsync(TId id) 
            => mapper.Map<TResponse>(await repository.GetByIdAsync(id));

        public virtual async Task<IEnumerable<TResponse>> GetAllAsync(bool noTracking = true) 
            => mapper.Map<IEnumerable<TResponse>>(await repository.GetAllAsync(noTracking));

        public virtual IEnumerable<TResponse> Find(FilterGroup filterGroup, bool noTracking = true) 
            => mapper.Map<IEnumerable<TResponse>>(repository.Find(filterService.BuildPredicate<T>(filterGroup), noTracking));

        public virtual async Task<IEnumerable<TResponse>> FindAsync(FilterGroup filterGroup, bool noTracking = true) 
            => mapper.Map<IEnumerable<TResponse>>(await repository.FindAsync(filterService.BuildPredicate<T>(filterGroup), noTracking));

        public virtual TResponse GetItem(FilterGroup filterGroup, bool noTracking = true) 
            => mapper.Map<TResponse>(repository.GetItem(filterService.BuildPredicate<T>(filterGroup), noTracking));

        public virtual TResponse Add(TRequest request)
        {
            var entity = mapper.Map<T>(request);
            repository.Add(entity);
            repository.SaveChanges();
            return mapper.Map<TResponse>(entity);
        }

        public virtual TResponse Update(TId id, TRequest request)
        {
            var entity = repository.GetById(id);
            if (entity == null) return null;

            mapper.Map(request, entity);
            repository.Update(entity);
            repository.SaveChanges();
            return mapper.Map<TResponse>(entity);
        }

        public virtual bool Delete(TId id)
        {
            var entity = repository.GetById(id);
            if (entity == null) return false;
            repository.Delete(entity);
            repository.SaveChanges();
            return true;
        }

        public virtual int GetCount(FilterGroup filterGroup, bool noTracking = true) => repository.Count(filterService.BuildPredicate<T>(filterGroup), noTracking);
        public virtual async Task<int> GetCountAsync(FilterGroup filterGroup, bool noTracking = true) => await repository.CountAsync(filterService.BuildPredicate<T>(filterGroup), noTracking);

        public void SaveChanges() => repository.SaveChanges();
        public async Task SaveChangesAsync() => await repository.SaveChangesAsync();

        // public virtual async Task<PagedResult<TResponse>> GetPagedAsync(FilterGroup filterGroup, int pageNumber, int pageSize)
        // {
        //     var predicate = filterService.BuildPredicate<T>(filterGroup);
        //     // Set includeRelation to true so AutoMapper can find the related names (e.g., CategoryName)
        //     var items = await repository.FindAsync(predicate, includeRelation: true);
        //     var totalCount = await repository.CountAsync(predicate);

        //     return new PagedResult<TResponse>
        //     {
        //         Items = mapper.Map<IEnumerable<TResponse>>(items.Skip((pageNumber - 1) * pageSize).Take(pageSize)),
        //         TotalCount = totalCount,
        //         PageNumber = pageNumber,
        //         PageSize = pageSize
        //     };
        // }
    }
}