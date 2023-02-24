using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace DsK.ITSM.Security.Infrastructure;
public partial class SecurityService
{
    public async Task<APIResult<List<CategoryDto>>> CategoriesGet()
    {
        var result = new APIResult<List<CategoryDto>>();
        List<Category>  items = await db.Categories.OrderBy(x=>x.CategoryName).ToListAsync();
        result.Result = Mapper.Map<List<Category>, List<CategoryDto>>(items);
        return result;
    }
}

