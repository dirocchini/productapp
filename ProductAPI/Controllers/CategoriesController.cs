using Application.Categories.Queries.GetAllCategories;
using Application.Common.Models;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("")]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResult<List<CategoryViewModel>>>> GetAllCategories(CancellationToken cancellation)
    {
        return await _sender.Send(new GetAllCategoriesQuery());
    }
}
