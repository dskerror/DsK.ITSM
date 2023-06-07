//using DsK.ITSM.Security.EntityFramework.Models;
//using DsK.ITSM.Security.Shared;

//namespace DsK.ITSM.Services
//{
//    public partial class SecurityService
//    {
//        public async Task<ServiceResult<string>> UserCreateLocalPassword(UserCreateLocalPasswordDto model)
//        {
//            //TODO : Implement Password Complexity Rules
//            //TODO : Implement Previously Used Password Constraint

//            ServiceResult<string> result = new ServiceResult<string>();
//            int recordsCreated = 0;

//            var ramdomSalt = SecurityHelpers.RandomizeSalt;

//            var userPassword = new UserPassword()
//            {
//                UserId = model.UserId,
//                HashedPassword = SecurityHelpers.HashPasword(model.Password, ramdomSalt),
//                Salt = Convert.ToHexString(ramdomSalt),
//                DateCreated = DateTime.Now
//            };

//            db.UserPasswords.Add(userPassword);

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
//    }
//}
