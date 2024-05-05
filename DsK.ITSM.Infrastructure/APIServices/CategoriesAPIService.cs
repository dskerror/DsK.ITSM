using AutoMapper;
using DsK.ITSM.Shared.APIService;
using DsK.ITSM.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using DsK.ITSM.Shared.DTOs;

namespace DsK.ITSM.Infrastructure.APIServices;

public class CategoriesAPIService : GenericAPIService<Category, CategoryDto>
{
    private readonly DskitsmContext _context;
    private IMapper _mapper;
    private readonly DbSet<Category> _dbSet;

    public CategoriesAPIService(DskitsmContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<Category>();
    }

    public async Task<APIResult<List<CategoryDto>>> Get(PagingRequest p)
    {
        if (!string.IsNullOrWhiteSpace(p.SearchString))
        {
            var query = _dbSet.Where(m => m.CategoryName.Contains(p.SearchString));
            return await base.Get(p, query);
        }
        else
        {
            return await base.Get(p, null);
        }
    }
}