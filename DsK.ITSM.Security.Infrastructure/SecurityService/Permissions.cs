﻿using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DsK.ITSM.Security.Infrastructure
{
    public partial class SecurityService
    {
        public async Task<APIResult<string>> PermissionCreate(PermissionCreateDto model)
        {
            APIResult<string> result = new APIResult<string>();
            int recordsCreated = 0;

            var record = new Permission();
            Mapper.Map(model, record);

            db.Permissions.Add(record);

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
                result.Result = recordsCreated.ToString();
                result.Message = "Record Created";
            }

            return result;
        }
        public async Task<APIResult<List<PermissionDto>>> PermissionsGet (int id = 0)
        {
            APIResult<List<PermissionDto>> result = new APIResult<List<PermissionDto>>();
            if (id == 0)
            {
                var items = await db.Permissions.ToListAsync();
                result.Result = Mapper.Map<List<Permission>, List<PermissionDto>>(items);
            }
            else
            {
                var items = await db.Permissions.Where(x => x.Id == id).ToListAsync();
                result.Result = Mapper.Map<List<Permission>, List<PermissionDto>>(items);
            }

            return result;
        }
        public async Task<APIResult<string>> PermissionUpdate(PermissionUpdateDto model)
        {
            APIResult<string> result = new APIResult<string>();
            int recordsUpdated = 0;
            var record = await db.Permissions.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (record != null)
            {
                Mapper.Map(model, record);                
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
            {
                result.Result = recordsUpdated.ToString();
                result.Message = "Record Updated";
            }

            return result;
        }
        public async Task<APIResult<string>> PermissionDelete(int id)
        {
            APIResult<string> result = new APIResult<string>();
            int recordsDeleted = 0;
            var record = db.Permissions.Attach(new Permission { Id = id });
            record.State = EntityState.Deleted;
            try
            {
                recordsDeleted = await db.SaveChangesAsync();
                result.Result = recordsDeleted.ToString();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }            
            
            return result;
        }
    }
}
