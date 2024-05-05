using AutoMapper;
using DsK.ITSM.Shared.APIService;
using DsK.ITSM.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using DsK.ITSM.Shared.DTOs;

namespace DsK.ITSM.Infrastructure.APIServices;

public class RequestsAPIService : GenericAPIService<Request, RequestDto>
{
    private readonly DskitsmContext _context;
    private IMapper _mapper;
    private readonly DbSet<Request> _dbSet;

    public RequestsAPIService(DskitsmContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<Request>();
    }

    public async Task<APIResult<List<RequestDto>>> Get(PagingRequest p)
    {
        if (!string.IsNullOrWhiteSpace(p.SearchString))
        {
            var query = _dbSet.Where(m => m.Description.Contains(p.SearchString));
            return await base.Get(p, query);
        }
        else
        {
            return await base.Get(p, null);
        }
    }

    public async Task<APIResult<RequestDto>> Create(RequestCreateDto dto)
    {
        RequestDto mappedDto = _mapper.Map<RequestDto>(dto);
        return await base.Create(mappedDto);
    }
}