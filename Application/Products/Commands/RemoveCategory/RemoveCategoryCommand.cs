

using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entity.Database;
using Domain.Entity.Repositories;

namespace Application.Products.Commands.RemoveCategory;

public sealed record RemoveCategoryCommand(int ProductId, int CategoryId) : IRequestWrapper;

public class RemoveCategoryCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : BaseHandler, IRequestHandlerWrapper<RemoveCategoryCommand>
{
    public async Task<ServiceResult> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithCategories(request.ProductId, cancellationToken);
        if (product is null)
            return NotFound<ProductEntity>("Product", request.ProductId, null);

        product.RemoveCategory(request.CategoryId);
        await productRepository.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success("");
    }
}
