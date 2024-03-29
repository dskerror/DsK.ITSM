﻿using DsK.ITSM.Security.Infrastructure;
using DsK.ITSM.Security.Shared;
using DsK.ITSM.Security.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DsK.ITSM.Server.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly SecurityService SecurityService;
        public RolesController(SecurityService securityService)
        {
            SecurityService = securityService;
        }

        [HttpPost]
        [Authorize(Roles = $"{Access.Admin}, {Access.Roles.Create}")]
        public async Task<IActionResult> RoleCreate(RoleCreateDto model)
        {
            var result = await SecurityService.RoleCreate(model);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = $"{Access.Admin}, {Access.Roles.View}")]
        public async Task<IActionResult> RolesGet(int id, int pageNumber, int pageSize, string searchString = null, string orderBy = null)
        {
            var result = await SecurityService.RolesGet(id, pageNumber, pageSize, searchString, orderBy);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = $"{Access.Admin}, {Access.Roles.Edit}")]
        public async Task<IActionResult> RoleUpdate(RoleUpdateDto model)
        {
            var result = await SecurityService.RoleUpdate(model);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = $"{Access.Admin}, {Access.Roles.Delete}")]
        public async Task<IActionResult> RoleDelete(int id)
        {
            var result = await SecurityService.RoleDelete(id);
            return Ok(result);
        }
    }
}

