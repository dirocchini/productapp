using Application.Common.Models;
using Application.Products.Commands.AddCategory;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.RemoveCategory;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetAllProducts;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("")]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResult<List<ProductViewModel>>>> GetAllProducts(CancellationToken cancellation)
    {
        return await _sender.Send(new GetAllProductsQuery());
    }

    [HttpPost("")]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResult>> CreateProduct(CreateProductCommand request, CancellationToken cancellation)
    {
        var result = await _sender.Send(request);

        if (result.Error is not null)
            return StatusCode(result.Error.Code, result);

        return result;
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResult>> UpdateProduct(int id, [FromBody] UpdateProductCommand request, CancellationToken cancellation)
    {
        request.ProductId = id;
        var result = await _sender.Send(request);

        if (result.Error is not null)
            return StatusCode(result.Error.Code, result);

        return result;

    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResult>> DeleteProduct(int id, CancellationToken cancellation)
    {
        var result = await _sender.Send(new DeleteProductCommand(id));

        if (result.Error is not null)
            return StatusCode(result.Error.Code, result);

        return result;
    }

    [HttpPost("{id:int}/categories/")]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResult>> AddCategory(int id, [FromBody] AddCategoryCommand request, CancellationToken cancellation)
    {
        request.ProductId = id;
        var result = await _sender.Send(request);

        if (result.Error is not null)
            return StatusCode(result.Error.Code, result);

        return result;
    }

    [HttpDelete("{id:int}/categories/{categoryId:int}")]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ServiceResult>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResult>> RemoveCategory(int id, int categoryId, CancellationToken cancellation)
    {
        var result = await _sender.Send(new RemoveCategoryCommand(id, categoryId));

        if (result.Error is not null)
            return StatusCode(result.Error.Code, result);

        return result;
    }
}
