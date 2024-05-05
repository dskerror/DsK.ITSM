using AutoMapper;
using DsK.ITSM.Shared.APIService;
using DsK.ITSM.Shared.Token;
using DsK.ITSM.EntityFramework.Models;
using DsK.Services.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Dynamic.Core;

namespace DsK.ITSM.Infrastructure.APIServices;
public class GenericAPIService<TEntity, TDto> : IGenericAPIService<TEntity, TDto> where TEntity : class
    where TDto : class
{
    private readonly DskitsmContext _context;
    private IMapper _mapper;
    private readonly DbSet<TEntity> _dbSet;

    public GenericAPIService(DskitsmContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        _mapper = mapper;
    }
    public async Task<APIResult<TDto>> Create(TDto dto)
    {
        var result = new APIResult<TDto>();

        try
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            result.Result = _mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Exception = ex;
            result.Message = "Error creating record. " + ex.Message;
        }
        return result;
    }
    public async Task<APIResult<List<TDto>>> Get(PagingRequest p, IQueryable<TEntity> queryable)
    {
        var result = new APIResult<List<TDto>>();

        try
        {
            IQueryable<TEntity> query = _dbSet;
            if (queryable != null)
                query = queryable;


            result.Paging.CurrentPage = p.PageNumber;
            p.PageNumber = p.PageNumber == 0 ? 1 : p.PageNumber;
            p.PageSize = p.PageSize == 0 ? 10 : p.PageSize;

            int count = 0;
            List<TEntity> items;

            count = await query.CountAsync();

            items = await query.OrderBy(p.OrderBy).Skip((p.PageNumber - 1) * p.PageSize)
                .Take(p.PageSize)
                .ToListAsync();

            result.Paging.TotalItems = count;
            result.Result = _mapper.Map<List<TEntity>, List<TDto>>(items);

        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Exception = ex;
            result.Message = "Error getting records. " + ex.Message;
        }

        return result;
    }
    public async Task<APIResult<TDto>> Get(int id)
    {
        var result = new APIResult<TDto>();

        try
        {
            var entity = await _dbSet.FindAsync(id);
            result.Result = _mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Exception = ex;
            result.Message = "Error getting record. " + ex.Message;
        }

        return result;
    }
    public async Task<APIResult<TDto>> Update(TDto dto)
    {
        var result = new APIResult<TDto>();
        try
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            result.Result = _mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Exception = ex;
            result.Message = "Error updating record. " + ex.Message;
        }

        return result;
    }
    public async Task<APIResult<string>> Delete(int id)
    {
        APIResult<string> result = new APIResult<string>();

        TEntity entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                result.Message = result.Result = "Record deleted";
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
        }
        else
        {
            result.HasError = true;
            result.Message = "Record not found to delete";
        }

        return result;
    }
}