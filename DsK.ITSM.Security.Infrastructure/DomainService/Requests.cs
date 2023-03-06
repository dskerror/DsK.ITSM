using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace DsK.ITSM.Security.Infrastructure;
public partial class SecurityService
{
    public async Task<APIResult<List<RequestDto>>> RequestsGet(int id, int pageNumber, int pageSize, string searchString, string orderBy)
    {
        var result = new APIResult<List<RequestDto>>();

        string ordering = "Id Desc";
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            string[] OrderBy = orderBy.Split(',');
            ordering = string.Join(",", OrderBy);
        }
        result.Paging.CurrentPage = pageNumber;
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        pageSize = pageSize == 0 ? 10 : pageSize;
        int count = 0;
        List<Request> items;
        if (!string.IsNullOrWhiteSpace(searchString))
        {

            count = await db.Requests
                .Where(x => x.RequestedByUserId == id)
                .Where(x =>
                        x.Category.CategoryName.Contains(searchString) ||
                        x.Description.Contains(searchString) ||
                        x.Summary.Contains(searchString) ||
                        x.Id.ToString().Contains(searchString) ||
                        x.Itsystem.SystemName.Contains(searchString)
                )
                .CountAsync();



            items = await db.Requests.OrderBy(ordering)
                .Include(x => x.RequestStatusHistories.OrderByDescending(i => i.Id).Take(1))
                .Include(x => x.RequestAssignedHistories.OrderByDescending(i => i.Id).Take(1)).ThenInclude(x => x.AssignedToUser)
                .Include(x => x.Category)
                .Include(x => x.Itsystem)
                .Where(x => x.RequestedByUserId == id && (
                        x.Category.CategoryName.Contains(searchString) ||
                        x.Description.Contains(searchString) ||
                        x.Summary.Contains(searchString) ||
                        x.Id.ToString().Contains(searchString) ||
                        x.Itsystem.SystemName.Contains(searchString) ||
                        x.RequestStatusHistories.OrderByDescending(i => i.Id).FirstOrDefault().Status.Contains(searchString) ||
                        x.RequestAssignedHistories.OrderByDescending(i => i.Id).FirstOrDefault().AssignedToUser.Name.Contains(searchString)
                    )
                )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else
        {
            count = await db.Requests.Where(x => x.RequestedByUserId == id).CountAsync();

            items = await db.Requests.OrderBy(ordering)
                .Include(x => x.RequestStatusHistories.OrderByDescending(i => i.Id).Take(1))
                .Include(x => x.RequestAssignedHistories.OrderByDescending(i => i.Id).Take(1)).ThenInclude(x => x.AssignedToUser)
                .Include(x => x.Category)
                .Include(x => x.Itsystem)
                .Where(x => x.RequestedByUserId == id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        result.Paging.TotalItems = count;
        result.Result = Mapper.Map<List<Request>, List<RequestDto>>(items);
        return result;
    }
    public async Task<APIResult<List<UserDto>>> RequestedByUserListGet()
    {
        var result = new APIResult<List<UserDto>>();
        List<User> items = await db.Users.OrderBy(x => x.Name).ToListAsync();
        result.Result = Mapper.Map<List<User>, List<UserDto>>(items);
        return result;
    }

    public async Task<APIResult<RequestDto>> RequestCreate(RequestCreateDto model)
    {
        APIResult<RequestDto> result = new APIResult<RequestDto>();
        int recordsCreated = 0;

        var record = new Request();
        Mapper.Map(model, record);
        
        record.RequestDateTime = DateTime.Now;
        await db.Requests.AddAsync(record);

        try
        {
            recordsCreated = await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Message = ex.InnerException.Message;
        }

        if (recordsCreated == 1)
        {
            result.Result = Mapper.Map(record, result.Result);
            result.Message = "Request Created";
        }

        return result;
    }

    //public async Task<APIResult<string>> UserUpdate(UserDto model)
    //{
    //    APIResult<string> result = new APIResult<string>();
    //    int recordsUpdated = 0;
    //    var record = await db.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

    //    if (record != null)
    //        Mapper.Map(model, record);

    //    try
    //    {
    //        recordsUpdated = await db.SaveChangesAsync();
    //    }
    //    catch (Exception ex)
    //    {
    //        result.HasError = true;
    //        result.Message = ex.InnerException.Message;
    //    }

    //    if (recordsUpdated == 1)        
    //        result.Message = "Record Updated";        

    //    return result;
    //}
    //public async Task<APIResult<string>> UserDelete(int id)
    //{
    //    APIResult<string> result = new APIResult<string>();
    //    int recordsDeleted = 0;
    //    var record = db.Users.Attach(new User { Id = id });
    //    record.State = EntityState.Deleted;
    //    try
    //    {
    //        recordsDeleted = await db.SaveChangesAsync();
    //    }
    //    catch (Exception ex)
    //    {
    //        result.HasError = true;
    //        result.Message = ex.Message;
    //    }

    //    result.Result = recordsDeleted.ToString();

    //    return result;
    //}

    //private async Task<User> GetUserByUsernameAsync(string username)
    //{
    //    return await db.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
    //}

    //private async Task<User> GetUserByMappedUsernameAsync(string username, int AuthenticationProviderId)
    //{
    //    var user = await (from u in db.Users
    //               join uap in db.UserAuthenticationProviders on u.Id equals uap.UserId
    //               where uap.Username == username && uap.AuthenticationProviderId == AuthenticationProviderId
    //               select u).FirstOrDefaultAsync();
    //    return user;
    //}
}

