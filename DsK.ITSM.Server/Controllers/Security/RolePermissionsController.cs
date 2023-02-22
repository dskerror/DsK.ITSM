using DsK.ITSM.Security.Infrastructure;
using DsK.ITSM.Security.Shared;
using DsK.ITSM.Security.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DsK.ITSM.Server.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class RolePermissionsController : ControllerBase
    {
        private readonly SecurityService SecurityService;
        public RolePermissionsController(SecurityService securityService)
        {
            SecurityService = securityService;
        }

        [HttpGet]
        [Authorize(Roles = $"{Access.Admin}, {Access.RolesPermissions.View}")]
        public async Task<IActionResult> RolePermissionsGet(int roleid)
        {
            if(roleid == 0)            
                return BadRequest();

            var result = await SecurityService.RolePermissionsGet(roleid);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = $"{Access.Admin}, {Access.RolesPermissions.Edit}")]
        public async Task<IActionResult> RolePermissionChange(RolePermissionChangeDto model)
        {
            var result = await SecurityService.RolePermissionChange(model);
            return Ok(result);
        }
    }
}

