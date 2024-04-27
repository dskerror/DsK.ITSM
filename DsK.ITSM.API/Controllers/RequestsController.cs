using DsK.ITSM.Shared.APIService;
using DsK.ITSM.Shared.DTOs.Requests;
using DsK.ITSM.EntityFramework.Models;
using DsK.ITSM.Infrastructure.APIServices;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Create(RequestCreateDto model)
    {
        return Ok(await _service.Create(model));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PagingRequest pagingRequest)
    {
        var result = await _service.Get(pagingRequest, null);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.Get(id);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(RequestDto model)
    {
        var result = await _service.Update(model);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}