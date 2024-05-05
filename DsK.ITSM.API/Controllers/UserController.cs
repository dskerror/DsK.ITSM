using DsK.ITSM.Shared.APIService;
using DsK.ITSM.Infrastructure.APIServices;
using Microsoft.AspNetCore.Mvc;
using DsK.ITSM.Shared.Token;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DsK.ITSM.Shared.DTOs;

namespace DsK.ITSM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserAPIService _service;

    public UserController(UserAPIService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = $"{Access.Admin}, {Access.User.Create}")]
    public async Task<IActionResult> Create(UserDto model)
    {
        return Ok(await _service.Create(model));
    }

    [HttpGet]
    [Authorize(Roles = $"{Access.Admin}, {Access.User.View}")]
    public async Task<IActionResult> Get([FromQuery] PagingRequest pagingRequest)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            // or
            //identity.FindFirst("ClaimName").Value;

        }

        var result = await _service.Get(pagingRequest, null);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{Access.Admin}, {Access.User.View}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.Get(id);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = $"{Access.Admin}, {Access.User.Edit}")]
    public async Task<IActionResult> Update(UserDto model)
    {
        var result = await _service.Update(model);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Access.Admin}, {Access.User.Delete}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}