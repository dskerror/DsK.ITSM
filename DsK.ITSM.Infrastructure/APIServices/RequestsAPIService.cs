using AutoMapper;
using DsK.ITSM.Shared.APIService;
using DsK.ITSM.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using DsK.ITSM.Shared.DTOs;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DsK.ITSM.Infrastructure.APIServices;

public class RequestsAPIService : GenericAPIService<Request, RequestDto>
{
    private readonly DskitsmContext _context;
    private IMapper _mapper;
    private readonly DbSet<Request> _dbSet;
    private readonly IDbConnection _connection;

    public RequestsAPIService(DskitsmContext context, IMapper mapper, IDbConnection connection) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<Request>();
        _connection = connection;
    }

    public async Task<APIResult<List<RequestGridDto>>> Get(PagingRequest p)
    {
        var result = new APIResult<List<RequestGridDto>>();
        result.Paging.CurrentPage = p.PageNumber;
        p.PageNumber = p.PageNumber == 0 ? 1 : p.PageNumber;
        p.PageSize = p.PageSize == 0 ? 10 : p.PageSize;

        try
        {
            var offset = (p.PageNumber - 1) * p.PageSize;
            var sql = @$"
                    SELECT a.*, c.StatusName FROM
                    (
	                    SELECT a.[Id],[Summary],[Description],[RequestDateTime],[RequestType],b.[Name] AS UserName
	                    ,c.SystemName,d.PriorityName,e.CategoryName,MAX(f.Id) AS RequestStatusHistoryMaxId
	                    FROM Requests a
	                    LEFT JOIN Users b ON a.UserId = b.Id
	                    LEFT JOIN ITSystems c ON a.ITSystemId= c.Id
	                    LEFT JOIN [Priority] d ON a.PriorityId= d.Id
	                    LEFT JOIN Categories e ON a.CategoryId= e.Id
	                    LEFT JOIN RequestStatusHistory f ON a.id = f.RequestId
  
	                    GROUP BY 
	                    a.[Id],[Summary],[Description],[RequestDateTime],[RequestType],b.[Name]
	                    ,c.SystemName,d.PriorityName,e.CategoryName
                    ) a
                    LEFT JOIN RequestStatusHistory b ON a.RequestStatusHistoryMaxId = b.id
                    LEFT JOIN Status c ON b.StatusId = c.Id

                    ORDER BY a.Id DESC

                    OFFSET {offset} ROWS FETCH NEXT {p.PageSize} ROWS ONLY;

                ";
            using (_connection)
            {
                result.Result = _connection.Query<RequestGridDto>(sql).ToList();
                result.Paging.TotalItems = await _dbSet.CountAsync();
            }
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Exception = ex;
            result.Message = "Error getting records. " + ex.Message;
        }

        return result;


        
    }

    public async Task<APIResult<RequestDto>> Create(RequestCreateDto dto)
    {
        RequestDto mappedDto = _mapper.Map<RequestDto>(dto);
        return await base.Create(mappedDto);
    }
}