using AutoMapper;
using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Infrastructure;
using DsK.ITSM.Security.Shared;
using DsK.ITSM.Security.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DsK.ITSM.Server.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly SecurityService SecurityService;
        private IMapper Mapper;

        public DashboardController(SecurityService securityService, IMapper Mapper)
        {
            SecurityService = securityService;
            this.Mapper = Mapper;
        }

        [HttpGet]
        //[Authorize(Roles = $"{Access.Admin}, {Access.Users.View}")]
        public async Task<IActionResult> MyRequestsGet(int id, int pageNumber, int pageSize, string searchString = null, string orderBy = null)
        {
            var result = await SecurityService.MyRequestsGet(id, pageNumber, pageSize, searchString, orderBy);
            return Ok(result);
        }
    }
}

