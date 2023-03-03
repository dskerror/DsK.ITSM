using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace DsK.ITSM.Security.Infrastructure;
public partial class SecurityService
{
    public async Task<APIResult<List<PriorityDto>>> PrioritiesGet()
    {
        try
        {
            var result = new APIResult<List<PriorityDto>>();
            var items = await db.Priorities.ToListAsync();
            result.Result = Mapper.Map<List<Priority>, List<PriorityDto>>(items);
            return result;

        }
        catch (Exception ex)
        {

            throw;
        }
    }
}

