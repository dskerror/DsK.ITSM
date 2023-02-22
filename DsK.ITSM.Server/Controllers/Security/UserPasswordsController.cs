using DsK.ITSM.Security.Infrastructure;
using DsK.ITSM.Security.Shared;
using DsK.ITSM.Security.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DsK.ITSM.Server.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class UserPasswordsController : ControllerBase
    {
        private readonly SecurityService SecurityService;
        public UserPasswordsController(SecurityService securityService)
        {
            SecurityService = securityService;
        }

        [HttpPost]
        [Authorize(Roles = $"{Access.Admin}, {Access.UserPasswords.Create}")]        
        public async Task<IActionResult> UserCreateLocalPassword(UserCreateLocalPasswordDto model)
        {
            //TODO : Create another method for user to change their own passwords
            var result = await SecurityService.UserCreateLocalPassword(model);
            return Ok(result);
        }     
    }
}

