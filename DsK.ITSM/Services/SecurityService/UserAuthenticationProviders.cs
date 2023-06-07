using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Data;
using DsK.ITSM.Dto;
using DsK.ITSM.Models;

namespace DsK.ITSM.Services;
public partial class SecurityService
{
    public async Task<ServiceResult<UserAuthenticationProviderDto>> UserAuthenticationProviderCreate(UserAuthenticationProviderCreateDto model)
    {
        ServiceResult<UserAuthenticationProviderDto> result = new ServiceResult<UserAuthenticationProviderDto>();

        int recordsCreated = 0;

        var record = new UserAuthenticationProvider();
        Mapper.Map(model, record);

        var checkDuplicateUsername = await db.UserAuthenticationProviders.FirstOrDefaultAsync(x => x.Username == model.Username && x.AuthenticationProviderId == model.AuthenticationProviderId);

        if (checkDuplicateUsername != null)
        {
            result.HasError = true;
            result.Message = "This username is already in use";
            return result;
        }

        db.UserAuthenticationProviders.Add(record);

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
            result.Message = "Record Created";
        }

        return result;
    }
    public async Task<ServiceResult<List<UserAuthenticationProvidersGridDto>>> UserAuthenticationProvidersGet(int userId)
    {

        var result = new ServiceResult<List<UserAuthenticationProvidersGridDto>>();
        var authenticationProviderList = await db.AuthenticationProviders.ToListAsync();


        var userAuthenticationProviderList = await (from uap in db.UserAuthenticationProviders
                                                    join ap in db.AuthenticationProviders on uap.AuthenticationProviderId equals ap.Id
                                                    where uap.UserId == userId
                                                    select new { uap.Id, uap.Username, AuthenticationProviderId = ap.Id }).ToListAsync();

        List<UserAuthenticationProvidersGridDto> userAuthenticationProvidersGridDtoList = new List<UserAuthenticationProvidersGridDto>();

        foreach (var item in authenticationProviderList)
        {
            userAuthenticationProvidersGridDtoList.Add(new UserAuthenticationProvidersGridDto
            {
                AuthenticationProviderId = item.Id,
                AuthenticationProviderName = item.AuthenticationProviderName,
                AuthenticationProviderType = item.AuthenticationProviderType
            });
        }

        foreach (var item in userAuthenticationProviderList)
        {
            var value = userAuthenticationProvidersGridDtoList.First(x => x.AuthenticationProviderId == item.AuthenticationProviderId);
            value.Username = item.Username;
            value.Id = item.Id;
        }

        result.Result = userAuthenticationProvidersGridDtoList;
        return result;
    }
    public async Task<ServiceResult<string>> UserAuthenticationProviderUpdate(UserAuthenticationProviderUpdateDto model)
    {
        ServiceResult<string> result = new ServiceResult<string>();
        int recordsUpdated = 0;
        var record = await db.UserAuthenticationProviders.FirstOrDefaultAsync(x => x.Id == model.Id);

        if (record != null)
            Mapper.Map(model, record);

        var checkDuplicateUsername = await db.UserAuthenticationProviders.FirstOrDefaultAsync(x => x.Username == model.Username && x.AuthenticationProviderId == record.AuthenticationProviderId);

        if (checkDuplicateUsername != null)
        {
            result.HasError = true;
            result.Message = "This username is already in use";
            return result;
        }

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
    public async Task<ServiceResult<string>> UserAuthenticationProviderDelete(int id)
    {
        ServiceResult<string> result = new ServiceResult<string>();
        int recordsDeleted = 0;
        var record = db.UserAuthenticationProviders.Attach(new UserAuthenticationProvider { Id = id });
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
}
