using AutoMapper;
using DsK.ITSM.Shared.APIService;
using DsK.ITSM.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using DsK.ITSM.Shared.DTOs;

namespace DsK.ITSM.Infrastructure.APIServices;

public class UserAPIService : GenericAPIService<User, UserDto>
{
    private readonly DskitsmContext _context;
    private IMapper _mapper;
    private readonly DbSet<User> _dbSet;

    public UserAPIService(DskitsmContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<User>();
    }

    public async Task<APIResult<List<UserDto>>> Get(PagingRequest p)
    {
        if (!string.IsNullOrWhiteSpace(p.SearchString))
        {
            var query = _dbSet.Where(m => m.Email.Contains(p.SearchString) || m.Name.Contains(p.SearchString));
            return await base.Get(p, query);
        }
        else
        {
            return await base.Get(p, null);
        }
    }
}