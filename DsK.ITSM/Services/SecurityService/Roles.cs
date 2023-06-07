//using DsK.ITSM.Security.EntityFramework.Models;
//using DsK.ITSM.Security.Shared;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Dynamic.Core;
//using System.Data;

//namespace DsK.ITSM.Services;
//public partial class SecurityService
//{
//    public async Task<ServiceResult<RoleDto>> RoleCreate(RoleCreateDto model)
//    {
//        ServiceResult<RoleDto> result = new ServiceResult<RoleDto>();
//        int recordsCreated = 0;

//        var record = new Role();
//        Mapper.Map(model, record);

//        db.Roles.Add(record);

//        try
//        {
//            recordsCreated = await db.SaveChangesAsync();
//        }
//        catch (Exception ex)
//        {
//            result.HasError = true;
//            result.Message = ex.InnerException.Message;
//        }

//        if (recordsCreated == 1)
//        {
//            result.Result = Mapper.Map(record, result.Result);
//            result.Message = "Record Created";
//        }

//        return result;
//    }
//    public async Task<ServiceResult<List<RoleDto>>> RolesGet(int id, int pageNumber, int pageSize, string searchString, string orderBy)
//    {
//        var result = new ServiceResult<List<RoleDto>>();

//        string ordering = "Id";
//        if (!string.IsNullOrWhiteSpace(orderBy))
//        {
//            string[] OrderBy = orderBy.Split(',');
//            ordering = string.Join(",", OrderBy);
//        }
//        result.Paging.CurrentPage = pageNumber;
//        pageNumber = pageNumber == 0 ? 1 : pageNumber;
//        pageSize = pageSize == 0 ? 10 : pageSize;
//        int count = 0;
//        List<Role> items;
//        if (!string.IsNullOrWhiteSpace(searchString))
//        {
//            count = await db.Roles
//                .Where(m => m.RoleName.Contains(searchString) || m.RoleDescription.Contains(searchString))
//                .CountAsync();

//            items = await db.Roles.OrderBy(ordering)
//                .Where(m => m.RoleName.Contains(searchString) || m.RoleDescription.Contains(searchString))
//                .Skip((pageNumber - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();
//        }
//        else if (id != 0)
//        {
//            count = await db.Roles
//                .Where(u => u.Id == id)
//                .CountAsync();

//            items = await db.Roles.OrderBy(ordering)
//                .Where(u => u.Id == id)
//                .ToListAsync();
//        }
//        else
//        {
//            count = await db.Roles.CountAsync();

//            items = await db.Roles.OrderBy(ordering)
//                .Skip((pageNumber - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();
//        }
//        result.Paging.TotalItems = count;
//        result.Result = Mapper.Map<List<Role>, List<RoleDto>>(items);
//        return result;
//    }
//    public async Task<ServiceResult<string>> RoleUpdate(RoleUpdateDto model)
//    {
//        ServiceResult<string> result = new ServiceResult<string>();
//        int recordsUpdated = 0;
//        var record = await db.Roles.FirstOrDefaultAsync(x => x.Id == model.Id);

//        if (record != null)
//            Mapper.Map(model, record);

//        try
//        {
//            recordsUpdated = await db.SaveChangesAsync();
//        }
//        catch (Exception ex)
//        {
//            result.HasError = true;
//            result.Message = ex.InnerException.Message;
//        }

//        if (recordsUpdated == 1)
//            result.Message = "Record Updated";

//        return result;
//    }
//    public async Task<ServiceResult<string>> RoleDelete(int id)
//    {
//        ServiceResult<string> result = new ServiceResult<string>();
//        int recordsDeleted = 0;
//        var record = db.Roles.Attach(new Role { Id = id });
//        record.State = EntityState.Deleted;
//        try
//        {
//            recordsDeleted = await db.SaveChangesAsync();            
//        }
//        catch (Exception ex)
//        {
//            result.HasError = true;
//            result.Message = ex.Message;
//        }
        
//        result.Result = recordsDeleted.ToString();

//        return result;
//    }
//}
