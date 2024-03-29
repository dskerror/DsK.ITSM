﻿using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace DsK.ITSM.Security.Infrastructure;
public partial class SecurityService
{
    public async Task<APIResult<UserDto>> UserCreate(UserCreateDto model)
    {
        APIResult<UserDto> result = new APIResult<UserDto>();
        int recordsCreated = 0;

        var record = new User();
        Mapper.Map(model, record);

        await db.Users.AddAsync(record);

        try
        {
            recordsCreated = await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Message = ex.InnerException.Message;
        }

        var userRole = new UserRole()
        {
            RoleId = 2,
            UserId = record.Id
        };
        await db.UserRoles.AddAsync(userRole);

        var userAuthenticationProvider = new UserAuthenticationProvider()
        {
            AuthenticationProviderId = 1,
            UserId = record.Id,
            Username = record.Username,
        };

        await db.UserAuthenticationProviders.AddAsync(userAuthenticationProvider);

        try
        {
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Message = ex.InnerException.Message;
        }

        if (recordsCreated == 1)
        {
            result.Result = Mapper.Map(record, result.Result);
            result.Message = "Record Created";
        }

        return result;
    }
    public async Task<APIResult<List<UserDto>>> UsersGet(int id, int pageNumber, int pageSize, string searchString, string orderBy)
    {
        var result = new APIResult<List<UserDto>>();

        string ordering = "Id";
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            string[] OrderBy = orderBy.Split(',');
            ordering = string.Join(",", OrderBy);
        }
        result.Paging.CurrentPage = pageNumber;
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        pageSize = pageSize == 0 ? 10 : pageSize;
        int count = 0;
        List<User> items;
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            count = await db.Users
                .Where(m => m.Username.Contains(searchString) || m.Name.Contains(searchString) || m.Email.Contains(searchString))
                .CountAsync();

            items = await db.Users.OrderBy(ordering)
                .Where(m => m.Username.Contains(searchString) || m.Name.Contains(searchString) || m.Email.Contains(searchString))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else if (id > 0)
        {
            count = await db.Users
                .Where(u => u.Id == id)
                .CountAsync();

            items = await db.Users.OrderBy(ordering)
                .Where(u => u.Id == id)
                .ToListAsync();
        }
        else if (id == -1)
        {
            count = await db.Users.CountAsync();
            items = await db.Users.OrderBy(x => x.Name).ToListAsync();
        }
        else
        {
            count = await db.Users.CountAsync();

            items = await db.Users.OrderBy(ordering)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        result.Paging.TotalItems = count;
        result.Result = Mapper.Map<List<User>, List<UserDto>>(items);
        return result;
    }
    public async Task<APIResult<string>> UserUpdate(UserDto model)
    {
        APIResult<string> result = new APIResult<string>();
        int recordsUpdated = 0;
        var record = await db.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

        if (record != null)
            Mapper.Map(model, record);

        try
        {
            recordsUpdated = await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Message = ex.InnerException.Message;
        }

        if (recordsUpdated == 1)
            result.Message = "Record Updated";

        return result;
    }
    public async Task<APIResult<string>> UserDelete(int id)
    {
        APIResult<string> result = new APIResult<string>();
        int recordsDeleted = 0;
        var record = db.Users.Attach(new User { Id = id });
        record.State = EntityState.Deleted;
        try
        {
            recordsDeleted = await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.Message = ex.Message;
        }

        result.Result = recordsDeleted.ToString();

        return result;
    }
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await db.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
    }
    public async Task<UserDto> GetUserByMappedUsernameAsync(string username, int AuthenticationProviderId)
    {
        var user = await (from u in db.Users
                          join uap in db.UserAuthenticationProviders on u.Id equals uap.UserId
                          where uap.Username == username && uap.AuthenticationProviderId == AuthenticationProviderId
                          select u).FirstOrDefaultAsync();

        var userDto = new UserDto();
        Mapper.Map(user, userDto);

        return userDto;
    }
}

