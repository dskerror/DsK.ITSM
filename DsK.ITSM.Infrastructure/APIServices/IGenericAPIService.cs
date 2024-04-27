using DsK.ITSM.Shared.APIService;

namespace DsK.ITSM.Infrastructure.APIServices;
public interface IGenericAPIService<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    Task<APIResult<TDto>> Create(TDto dto);
    Task<APIResult<string>> Delete(int id);
    Task<APIResult<TDto>> Get(int id);
    Task<APIResult<List<TDto>>> Get(PagingRequest p, IQueryable<TEntity> queryable);
    Task<APIResult<TDto>> Update(TDto dto);
}