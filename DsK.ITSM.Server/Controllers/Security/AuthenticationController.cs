using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Infrastructure;
using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DsK.ITSM.Server.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SecurityService SecurityService;
        public AuthenticationController(SecurityService securityService)
        {
            SecurityService = securityService;
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(UserLoginDto model)
        {
            //todo : implement captcha

            APIResult<TokenModel> result = new APIResult<TokenModel>();
            var IsUserAuthenticated = await SecurityService.AuthenticateUser(model.Username, model.Password, model.AuthenticationProviderId);

            if (IsUserAuthenticated)
            {
                var user = SecurityService.GetUserByMappedUsernameAsync(model.Username, model.AuthenticationProviderId);
                var token = await GenerateAuthenticationToken(user);

                db.UserTokens.Add(new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = token.RefreshToken,
                    TokenRefreshedDateTime = DateTime.Now,
                    TokenCreatedDateTime = DateTime.Now
                });

                await db.SaveChangesAsync();

                result.Result = token;
                return result;

            }
            else
            {
                result.HasError = true;
                return Ok(result);
            }


            //var tokenModel = await SecurityService.UserLogin(model);

            //if (tokenModel == null)
            //    return NotFound();

            //return Ok(tokenModel);
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel refreshToken)
        {
            var resultTokenModel = await SecurityService.RefreshToken(refreshToken);
            if (resultTokenModel == null)
            {
                return NotFound();
            }
            return Ok(resultTokenModel);
        }
    }
}

