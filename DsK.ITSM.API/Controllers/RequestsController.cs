using DsK.ITSM.Shared.APIService;
using DsK.ITSM.Shared.DTOs.Requests;
using DsK.ITSM.EntityFramework.Models;
using DsK.ITSM.Infrastructure.APIServices;
using Microsoft.AspNetCore.Mvc;
using DsK.ITSM.Shared.Token;
using Microsoft.AspNetCore.Authorization;

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
        return Ok(await _service.Create(model));
    }

    [HttpGet]
    [Authorize(Roles = $"{Access.Admin}, {Access.Request.View}")]
    public async Task<IActionResult> Get([FromQuery] PagingRequest pagingRequest)
    {
        var result = await _service.Get(pagingRequest, null);
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