using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace DsK.ITSM.Security.Infrastructure;
public partial class SecurityService
{
    public async Task<APIResult<List<RequestTypeDto>>> RequestTypesGet()
    {
        var result = new APIResult<List<RequestTypeDto>>();
        List<RequestType>  items = await db.RequestTypes.OrderBy(x=>x.RequestTypeName).ToListAsync();
        result.Result = Mapper.Map<List<RequestType>, List<RequestTypeDto>>(items);
        return result;
    }
}

