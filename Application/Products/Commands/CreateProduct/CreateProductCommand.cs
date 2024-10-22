using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Events;
using Domain.Entity.Database;
using Domain.Entity.Repositories;
using System.Net;

namespace Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Value, List<string>? Categories) : IRequestWrapper;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    ProductEventPublisher productEventPublisher) : BaseHandler, IRequestHandlerWrapper<CreateProductCommand>
{
    public async Task<ServiceResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
       var product = await productRepository.GetByName(request.Name, cancellationToken);
        if (product is not null)
            return BadRequest("Product already exists with same name");

        var newProduct = new ProductEntity(request.Name, request.Value);

        if (request.Categories != null)
        {
            var addCategoriesReturnObject = await AddCategories(request, newProduct, cancellationToken);
            if (addCategoriesReturnObject is ServiceResult)
                return addCategoriesReturnObject;
        }

        var validations = newProduct.Validate();

        if (!validations.IsValid)
            return BadRequest(validations);

        await productRepository.AddAsync(newProduct);
        await productRepository.SaveChangesAsync(cancellationToken);

        await productEventPublisher.PublishEvent();

        var result = ServiceResult.Success("", (int)HttpStatusCode.Created);
        return result;
    }

    private async Task<dynamic> AddCategories(CreateProductCommand request, ProductEntity product, CancellationToken cancellationToken)
    {
        var notFoundCategories = new List<string>();
        var categories = new List<CategoryEntity>();

        foreach (var category in request.Categories)
        {
            var cat = await categoryRepository.GetByName(category, cancellationToken);
            if (cat == null) notFoundCategories.Add(category);
            else categories.Add(cat);
        }

        if (notFoundCategories.Any())
            return NotFound("Categories", string.Join("; ", notFoundCategories.Select(c => c)));

        product.AddCategories(categories);

        return true;
    }
}
