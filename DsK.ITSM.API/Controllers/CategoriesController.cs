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
public class CategoriesController : ControllerBase
{
    private readonly CategoriesAPIService _service;

    public CategoriesController(CategoriesAPIService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = $"{Access.Admin}, {Access.Category.Create}")]
    public async Task<IActionResult> Create(CategoryDto model)
    {
        return Ok(await _service.Create(model));
    }

    [HttpGet]
    [Authorize(Roles = $"{Access.Admin}, {Access.Category.View}")]
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
    [Authorize(Roles = $"{Access.Admin}, {Access.Category.View}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.Get(id);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = $"{Access.Admin}, {Access.Category.Edit}")]
    public async Task<IActionResult> Update(CategoryDto model)
    {
        var result = await _service.Update(model);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Access.Admin}, {Access.Category.Delete}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}