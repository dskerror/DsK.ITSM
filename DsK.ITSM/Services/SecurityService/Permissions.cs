//using DsK.ITSM.Security.EntityFramework.Models;
//using DsK.ITSM.Security.Shared;
//using Microsoft.EntityFrameworkCore;
//using System.Data;

//namespace DsK.ITSM.Services
//{
//    public partial class SecurityService
//    {
//        public async Task<ServiceResult<string>> PermissionCreate(PermissionCreateDto model)
//        {
//            ServiceResult<string> result = new ServiceResult<string>();
//            int recordsCreated = 0;

//            var record = new Permission();
//            Mapper.Map(model, record);

//            db.Permissions.Add(record);

//            try
//            {
//                recordsCreated = await db.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                result.HasError = true;
//                result.Message = ex.InnerException.Message;
//            }

//            if (recordsCreated == 1)
//            {
//                result.Result = recordsCreated.ToString();
//                result.Message = "Record Created";
//            }

//            return result;
//        }
//        public async Task<ServiceResult<List<PermissionDto>>> PermissionsGet (int id = 0)
//        {
//            ServiceResult<List<PermissionDto>> result = new ServiceResult<List<PermissionDto>>();
//            if (id == 0)
//            {
//                var items = await db.Permissions.ToListAsync();
//                result.Result = Mapper.Map<List<Permission>, List<PermissionDto>>(items);
//            }
//            else
//            {
//                var items = await db.Permissions.Where(x => x.Id == id).ToListAsync();
//                result.Result = Mapper.Map<List<Permission>, List<PermissionDto>>(items);
//            }

//            return result;
//        }
//        public async Task<ServiceResult<string>> PermissionUpdate(PermissionUpdateDto model)
//        {
//            ServiceResult<string> result = new ServiceResult<string>();
//            int recordsUpdated = 0;
//            var record = await db.Permissions.FirstOrDefaultAsync(x => x.Id == model.Id);

//            if (record != null)
//            {
//                Mapper.Map(model, record);                
//            }

//            try
//            {
//                recordsUpdated = await db.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                result.HasError = true;
//                result.Message = ex.InnerException.Message;
//            }

            
//            if (recordsUpdated == 1)
//            {
//                result.Result = recordsUpdated.ToString();
//                result.Message = "Record Updated";
//            }

//            return result;
//        }
//        public async Task<ServiceResult<string>> PermissionDelete(int id)
//        {
//            ServiceResult<string> result = new ServiceResult<string>();
//            int recordsDeleted = 0;
//            var record = db.Permissions.Attach(new Permission { Id = id });
//            record.State = EntityState.Deleted;
//            try
//            {
//                recordsDeleted = await db.SaveChangesAsync();
//                result.Result = recordsDeleted.ToString();
//            }
//            catch (Exception ex)
//            {
//                result.HasError = true;
//                result.Message = ex.Message;
//            }            
            
//            return result;
//        }
//    }
//}
