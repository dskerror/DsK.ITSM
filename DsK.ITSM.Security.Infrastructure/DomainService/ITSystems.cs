using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace DsK.ITSM.Security.Infrastructure;
public partial class SecurityService
{
    public async Task<APIResult<List<ItsystemDto>>> ITSystemsGet()
    {
        var result = new APIResult<List<ItsystemDto>>();
        var items = await db.Itsystems.OrderBy(x => x.SystemName).ToListAsync();
        result.Result = Mapper.Map<List<Itsystem>, List<ItsystemDto>>(items);
        return result;
    }
}

