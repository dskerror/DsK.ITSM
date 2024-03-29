﻿using DsK.ITSM.Security.Infrastructure;
using DsK.ITSM.Security.Shared;
using DsK.ITSM.Security.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DsK.ITSM.Server.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly SecurityService SecurityService;
        public PermissionsController(SecurityService securityService)
        {
            SecurityService = securityService;
        }

        [HttpGet]
        [Authorize(Roles = $"{Access.Admin}, {Access.Permissions.View}")]
        public async Task<IActionResult> PermissionsGet(int id = 0)
        {
            var result = await SecurityService.PermissionsGet(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = $"{Access.Admin}, {Access.Permissions.Create}")]
        public async Task<IActionResult> PermissionCreate(PermissionCreateDto model)
        {
            var result = await SecurityService.PermissionCreate(model);
            return Ok(result);
        }
        [HttpPut]
        [Authorize(Roles = $"{Access.Admin}, {Access.Permissions.Edit}")]
        public async Task<IActionResult> PermissionUpdate(PermissionUpdateDto model)
        {
            var result = await SecurityService.PermissionUpdate(model);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = $"{Access.Admin}, {Access.Permissions.Delete}")]
        public async Task<IActionResult> PermissionDelete(int id)
        {
            var result = await SecurityService.PermissionDelete(id);
            return Ok(result);
        }
    }
}

