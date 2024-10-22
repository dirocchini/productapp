using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entity.Database;
using Domain.Entity.Repositories;
using System.Text.Json.Serialization;

namespace Application.Products.Commands.AddCategory;

public class AddCategoryCommand : IRequestWrapper
{
    [JsonIgnore]
    public int ProductId { get; set; }
    public string CategoryName { get; set; }
}
public class AddCategoryCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : BaseHandler, IRequestHandlerWrapper<AddCategoryCommand>
{
    public async Task<ServiceResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return NotFound<ProductEntity>("Product", request.ProductId, null);

        var category = await categoryRepository.GetByName(request.CategoryName, cancellationToken);
        if (category is null)
            return NotFound("Category", $"Category not found with name {request.CategoryName}");

        product.AddCategory(category);
        productRepository.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success("");
    }
}