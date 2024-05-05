using AutoMapper;
using DsK.ITSM.Shared.APIService;
using DsK.ITSM.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using DsK.ITSM.Shared.DTOs;

namespace DsK.ITSM.Infrastructure.APIServices;

public class ITSystemsAPIService : GenericAPIService<Itsystem, ItsystemDto>
{
    private readonly DskitsmContext _context;
    private IMapper _mapper;
    private readonly DbSet<Itsystem> _dbSet;

    public ITSystemsAPIService(DskitsmContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<Itsystem>();
    }

    public async Task<APIResult<List<ItsystemDto>>> Get(PagingRequest p)
    {
        if (!string.IsNullOrWhiteSpace(p.SearchString))
        {
            var query = _dbSet.Where(m => m.SystemName.Contains(p.SearchString));
            return await base.Get(p, query);
        }
        else
        {
            return await base.Get(p, null);
        }
    }
}