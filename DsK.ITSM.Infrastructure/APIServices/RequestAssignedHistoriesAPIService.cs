using AutoMapper;
using DsK.ITSM.Shared.APIService;
using DsK.ITSM.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using DsK.ITSM.Shared.DTOs;

namespace DsK.ITSM.Infrastructure.APIServices;

public class RequestAssignedHistoriesAPIService : GenericAPIService<RequestAssignedHistory, RequestAssignedHistoryDto>
{
    private readonly DskitsmContext _context;
    private IMapper _mapper;
    private readonly DbSet<RequestAssignedHistory> _dbSet;

    public RequestAssignedHistoriesAPIService(DskitsmContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<RequestAssignedHistory>();
    }
}