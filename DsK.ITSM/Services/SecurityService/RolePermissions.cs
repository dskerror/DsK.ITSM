//using DsK.ITSM.Security.EntityFramework.Models;
//using DsK.ITSM.Security.Shared;
//using Microsoft.EntityFrameworkCore;
//using System.Data;

//namespace DsK.ITSM.Services
//{
//    public partial class SecurityService
//    {
//        public async Task<ServiceResult<string>> RolePermissionChange(RolePermissionChangeDto model)
//        {
//            ServiceResult<string> result = new ServiceResult<string>();
//            int recordsModifiedCount = 0;
//            var record = new RolePermission();
//            Mapper.Map(model, record);

//            if (model.PermissionEnabled)
//            {
//                db.RolePermissions.Add(record);
//                try
//                {
//                    recordsModifiedCount = await db.SaveChangesAsync();
//                }
//                catch (Exception ex)
//                {
//                    result.HasError = true;
//                    result.Message = ex.InnerException.Message;
//                }
//                if (recordsModifiedCount == 1)
//                {
//                    result.Result = recordsModifiedCount.ToString();
//                    result.Message = "Record Created";
//                }
//            }
//            else
//            {
//                var recordToDelete = db.RolePermissions.Attach(record);
//                recordToDelete.State = EntityState.Deleted;

//                try
//                {
//                    recordsModifiedCount = await db.SaveChangesAsync();
//                    result.Result = recordsModifiedCount.ToString();
//                }
//                catch (Exception ex)
//                {
//                    result.HasError = true;
//                    result.Message = ex.Message;
//                }
//            }
//            return result;
//        }


//        public async Task<ServiceResult<List<RolePermissionGridDto>>> RolePermissionsGet(int roleId)
//        {

//            ServiceResult<List<RolePermissionGridDto>> result = new ServiceResult<List<RolePermissionGridDto>>();
//            var permissionList = await db.Permissions.ToListAsync();


//            var RolePermissionList = await (from r in db.Roles
//                                            join rp in db.RolePermissions on r.Id equals rp.RoleId
//                                            join p in db.Permissions on rp.PermissionId equals p.Id
//                                            where r.Id == roleId
//                                            select p.PermissionName).ToListAsync();

//            var permissionGrid = Mapper.Map<List<Permission>, List<RolePermissionGridDto>>(permissionList);

//            foreach (var permission in RolePermissionList)
//            {
//                var value = permissionGrid.First(x => x.PermissionName == permission);
//                value.Allow = true;
//            }

//            result.Result = permissionGrid;
//            return result;

//        }
//    }
//}
