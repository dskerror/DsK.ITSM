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
    public class RequestsController : ControllerBase
    {
        private readonly SecurityService SecurityService;

        public RequestsController(SecurityService securityService)
        {
            SecurityService = securityService;
        }

        [HttpGet]
        //[Authorize(Roles = $"{Access.Admin}, {Access.Users.View}")]
        public async Task<IActionResult> RequestsGet(int id, int pageNumber, int pageSize, string searchString = null, string orderBy = null)
        {
            var result = await SecurityService.RequestsGet(id, pageNumber, pageSize, searchString, orderBy);
            return Ok(result);
        }

        [HttpPost]
        //[Authorize(Roles = $"{Access.Admin}, {Access.Roles.Create}")]
        public async Task<IActionResult> RequestsCreate(RequestCreateDto model)
        {
            var result = await SecurityService.RequestCreate(model);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Roles = $"{Access.Admin}, {Access.Users.View}")]
        [Route("RequestedByUserListGet")]
        public async Task<IActionResult> RequestedByUserListGet()
        {
            var result = await SecurityService.RequestedByUserListGet();
            return Ok(result);
        }
    }
}