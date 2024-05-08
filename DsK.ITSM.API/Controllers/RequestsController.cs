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
public class RequestsController : ControllerBase
{
    private readonly RequestsAPIService _service;

    public RequestsController(RequestsAPIService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = $"{Access.Admin}, {Access.Request.Create}")]
    public async Task<IActionResult> Create(RequestCreateDto model)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            try
            {
                int userId = int.Parse(identity.FindFirst("UserId").Value);
                await _service.Create(model, userId);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = $"{Access.Admin}, {Access.Request.View}")]
    public async Task<IActionResult> Get([FromQuery] PagingRequest pagingRequest)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            // or
            //identity.FindFirst("Id").Value;

        }

        var result = await _service.Get(pagingRequest);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{Access.Admin}, {Access.Request.View}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.Get(id);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = $"{Access.Admin}, {Access.Request.Edit}")]
    public async Task<IActionResult> Update(RequestDto model)
    {
        var result = await _service.Update(model);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Access.Admin}, {Access.Request.Delete}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}