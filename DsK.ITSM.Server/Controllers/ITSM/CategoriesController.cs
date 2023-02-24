using AutoMapper;
using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Infrastructure;
using DsK.ITSM.Security.Shared;
using DsK.ITSM.Security.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DsK.ITSM.Server.Controllers.ITSM
{
    [Route("api/ITSM/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SecurityService SecurityService;

        public CategoriesController(SecurityService securityService)
        {
            SecurityService = securityService;
        }

        [HttpGet]
        //[Authorize(Roles = $"{Access.Admin}, {Access.Users.View}")]
        public async Task<IActionResult> CategoriesGet()
        {
            var result = await SecurityService.CategoriesGet();
            return Ok(result);
        }
    }
}

